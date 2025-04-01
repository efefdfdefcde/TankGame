using Assets.Scripts.New.Shop.Assembly.Parts;
using Assets.Scripts.New.Shop.UI.BuildPopup;
using R3;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.New.Shop.Assembly.ConstructorF
{
    public class ConstructorPresenter 
    {
        public Subject<Unit> _build = new();
        public Subject<Unit> _setBuild = new();
        public Subject<Unit> _destroy = new();
        public Subject<(string, Sprite,BuildPopupModel)> _save = new();
        public Subject<Unit> _reselect = new();

        private Constructor _constructor;
        private ConstructorView _view;

        public ReadOnlyReactiveProperty<Corpus> Corpus => _corpusSO;
        private readonly ReactiveProperty<Corpus> _corpusSO = new();

        public ReadOnlyReactiveProperty<Turret> Turret => _turretSO;
        private readonly ReactiveProperty<Turret> _turretSO = new();

        public ReadOnlyReactiveProperty<Cannon> Cannon => _cannonSO;
        private readonly ReactiveProperty<Cannon> _cannonSO = new();

        public ReadOnlyReactiveProperty<Tracks> Track => _trackSO;
        private readonly ReactiveProperty<Tracks> _trackSO = new();

        public ReadOnlyReactiveProperty<bool> BuildStatus => _isBuild;
        private readonly ReactiveProperty<bool> _isBuild = new();

        private bool _isSave;
        private bool _overweight;
        private bool _notEnoughtAmmo;

        private BuildPopupModel _buildModel;

        private CompositeDisposable _disposables = new();

        private readonly int _minimalShellsCount = 20;

        public ConstructorPresenter(Constructor constructor, ConstructorView view)
        {
            _view = view;
            _view._build.Subscribe(_ => Build()).AddTo(_disposables);
            _view._save.Subscribe(name => Save(name)).AddTo(_disposables);
            _view._delete.Subscribe(_ =>  Delete()).AddTo(_disposables);

            BuildStatus.Subscribe(_ => BuildButton()).AddTo(_disposables);
            New.Arhitecture.EventBus.Instance._sendBuild.Subscribe(build => SetBuild(build)).AddTo(_disposables);
            New.Arhitecture.EventBus.Instance._setPartEvent.Subscribe(part => SetPart(part)).AddTo(_disposables);
            _constructor = constructor;
            _constructor.Save.Subscribe(saveStatus => { _isSave = saveStatus; SaveButton(); }).AddTo(_disposables);
   
        }

        private void SetPart(TankPartSO part)
        {
            switch (part)
            {
                case Corpus corpusPart:
                    _corpusSO.Value = corpusPart;
                    break;

                case Turret turretPart:
                    _turretSO.Value = turretPart;
                    break;

                case Cannon cannonPart:
                    _cannonSO.Value = cannonPart;
                    break;

                case Tracks tracksPart:
                    _trackSO.Value = tracksPart;
                    break;

                default:
                    Debug.LogWarning("Unknown part type");
                    break;
            }
            BuildButton();
            DeleteButton();
            SaveButton();
        }

        private void BuildButton()
        {
            if(_corpusSO.Value && _turretSO.Value && _cannonSO.Value && _trackSO.Value && !_isBuild.Value)
            {
                _view.ActivateBuildButton();
            }
            else _view.DeactivateBuildButton();
        }

        private void DeleteButton()
        {
            if( _corpusSO.Value || _turretSO.Value || _cannonSO.Value || _trackSO.Value)_view.ActivateDeleteButton();
            else _view.DeactivateDeleteButton();
        }

        private void SaveButton()
        {
            if(_isSave)
            {
                CalculateCharacteristic();
                if (!_overweight && !_notEnoughtAmmo)
                _view.ActivateSaveButton();
            }
            else _view.DeactivateSaveButton();
        }

        private void Build()
        {
            _build.OnNext(Unit.Default);
            _isBuild.Value = true;
        }

        private void Save((string name,Sprite icon) saveInfo)
        {
            _save.OnNext((saveInfo.name,saveInfo.icon,_buildModel));
        }

        private void Delete()
        {
            SetBuild(_buildModel);
        }

        private void SetBuild(BuildPopupModel model)
        {
            _isBuild.Value = false;
            _buildModel = model;
            if (model._build != null)
            {
                _corpusSO.Value = model._build._corpus;
                _turretSO.Value = model._build._turret;
                _cannonSO.Value = model._build._cannon;
                _trackSO.Value = model._build._tracks;
                _setBuild.OnNext(Unit.Default);
                _view.ShowName(model._build._name);
                _isBuild.Value = true;
                CalculateCharacteristic();
            }
            else
            {
                _destroy.OnNext(Unit.Default);
                _corpusSO.Value = null;
                _turretSO.Value = null;
                _cannonSO.Value = null;
                _trackSO.Value = null;
                _view.ClearString();
            }
            _reselect.OnNext(Unit.Default);
            BuildButton();
        }

        private void CalculateCharacteristic()
        {
            int health = _corpusSO.Value._health + _turretSO.Value._health;
            int armor = _corpusSO.Value._armor + _turretSO.Value._armor;
            _view.ShowArmorAndHealth(health, armor);
            BuildWeightIsValid();
            HasEnoughAmmo();
            _view.ShowTrack(_trackSO.Value);
            _view.ShowCorpus(_corpusSO.Value);
            _view.ShowTurret(_turretSO.Value);
            _view.ShowCannon(_cannonSO.Value);
        }

        private void BuildWeightIsValid()
        {
            int weight = _corpusSO.Value._weight + _turretSO.Value._weight;
            if (weight <_trackSO.Value._maxWeight)
            { _view.OverweightWarning(false); _overweight = false; }
            else { _view.OverweightWarning(true); _overweight = true; }
            _view.ShowWeight(weight);
        }

        private void HasEnoughAmmo()
        {
            int ammo = _corpusSO.Value._shellCapacity / _cannonSO.Value._shellSize;
            if (ammo >= _minimalShellsCount)
            { _view.ShellWarning(false,_minimalShellsCount); _notEnoughtAmmo = false; }
            else {_view.ShellWarning(true, _minimalShellsCount); _notEnoughtAmmo = true; }
            _view.ShowShellsCount(ammo);
        }

        public void OnDestroy()
        {
            _disposables.Dispose();
        }
    }
}