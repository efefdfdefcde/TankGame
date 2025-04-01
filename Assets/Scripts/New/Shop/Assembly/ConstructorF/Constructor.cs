using Assets.Scripts.New.Shop.Assembly.ConstructorF;
using Assets.Scripts.New.Shop.Assembly.Parts;
using Assets.Scripts.New.Shop.UI.BuildPopup;
using Assets.Scripts.New.Shop.UI.ResearchTree;
using R3;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.Scripts.New.Shop.Assembly
{
    public class Constructor 
    {
        private Transform _tracksPoint;
        private ConstructorPresenter _presenter;

        private Corpus _corpusSO;
        private Turret _turretSO;
        private Cannon _cannonSO;
        private Tracks _tracksSO;

        private bool _isBuild;
        private bool _isCannonChange;

        public ReadOnlyReactiveProperty<bool> Save => _isSave;
        private readonly ReactiveProperty<bool> _isSave = new();

        private readonly Dictionary<PartsNames, TankPartSO> _partsDictionary = new() 
        {
            { PartsNames.Corpus, null },
            { PartsNames.Turret, null },
            { PartsNames.Cannon, null },
            { PartsNames.Tracks, null }
        };

        private CorpusPart _corpus;
        private TurretPart _turret;
        private CannonPart _cannon;
        private TracksPart _tracks;

        private CompositeDisposable _disposables = new();

        public Constructor(Transform trackPoint)
        {
            _tracksPoint = trackPoint;
        }

        public void Init(ConstructorPresenter presenter)
        {
            _presenter = presenter;
            _presenter._build.Subscribe(_ => Build()).AddTo(_disposables);
            _presenter._setBuild.Subscribe(_ => SetBuild()).AddTo(_disposables);
            _presenter._destroy.Subscribe(_ => {_isSave.Value = false; DestroyAll(); }).AddTo(_disposables);
            _presenter.BuildStatus.Subscribe(build => _isBuild = build).AddTo(_disposables);
            _presenter._save.Subscribe(saveInfo => SaveBuild(saveInfo)).AddTo(_disposables);
            _presenter.Corpus.Subscribe(corpus => { _corpusSO = corpus; _partsDictionary[PartsNames.Corpus] = corpus; ChangeCorpus(); }).AddTo(_disposables);
            _presenter.Turret.Subscribe(turret => { _turretSO = turret; _partsDictionary[PartsNames.Turret] = turret; ChangeTurret(); }).AddTo(_disposables);
            _presenter.Cannon.Subscribe(cannon => { _cannonSO = cannon; _partsDictionary[PartsNames.Cannon] = cannon; ChangeCannon(); }).AddTo(_disposables);
            _presenter.Track.Subscribe(track => { _tracksSO = track; _partsDictionary[PartsNames.Tracks] = track; ChangeTracks(); }).AddTo(_disposables);
        }

       

        private void Assemble()
        {
            _tracks = UnityEngine.Object.Instantiate(_tracksSO._shopPrefab, _tracksPoint.position, Quaternion.identity);
            _corpus = UnityEngine.Object.Instantiate(_corpusSO._shopPrefab);
            _corpus.transform.position = _tracks._corpusPoint.transform.position;
            _tracks._rightTrack.transform.position = new Vector3(
                _corpus._rightTrack.transform.position.x,
                _tracks._rightTrack.transform.position.y,
                _tracks._rightTrack.transform.position.z
            );
            _tracks._leftTrack.transform.position = new Vector3(
               _corpus._leftTrack.transform.position.x,
               _tracks._leftTrack.transform.position.y,
               _tracks._leftTrack.transform.position.z
            );
            _turret = UnityEngine.Object.Instantiate(_turretSO._shopPrefab);
            _turret.transform.position = _corpus._turretPoint.transform.position;
            _cannon = UnityEngine.Object.Instantiate(_cannonSO._shopPrefab);
            _cannon.transform.position = _turret._cannonPoint.transform.position;
        }

        private void Build()
        {
            Assemble();
            _isSave.Value = true;
        }

        #region
        private void ChangeCorpus()//Override
        {
            if (_isBuild)
            {
                UnityEngine.Object.Destroy(_corpus.gameObject);
                _corpus = UnityEngine.Object.Instantiate(_corpusSO._shopPrefab);
                Rebuild();
                _isSave.Value = true;
            }
        }

        private void ChangeTurret()
        {
            if (_isBuild)
            {
                UnityEngine.Object.Destroy(_turret.gameObject);
                _turret = UnityEngine.Object.Instantiate( _turretSO._shopPrefab);
                Rebuild();
                _isSave.Value = true;
            }
        }

        private void ChangeTracks()
        {
            if (_isBuild)
            {
                UnityEngine.Object.Destroy(_tracks.gameObject);
                _tracks = UnityEngine.Object.Instantiate(_tracksSO._shopPrefab);
                Rebuild();
                _isSave.Value = true;
            }
        }

        private void ChangeCannon()
        {
            if (_isBuild)
            {
                UnityEngine.Object.Destroy(_cannon.gameObject);
                _cannon = UnityEngine.Object.Instantiate(_cannonSO._shopPrefab);
                Rebuild();
                _isCannonChange = true;
                _isSave.Value = true;
            }
        }

        private void Rebuild()
        {
            _corpus.transform.position = _tracks._corpusPoint.transform.position;
            _tracks._rightTrack.transform.position = new Vector3(
                _corpus._rightTrack.transform.position.x,
                _tracks._rightTrack.transform.position.y,
                _tracks._rightTrack.transform.position.z
            );
            _tracks._leftTrack.transform.position = new Vector3(
               _corpus._leftTrack.transform.position.x,
               _tracks._leftTrack.transform.position.y,
               _tracks._leftTrack.transform.position.z
            );
            _turret.transform.position = _corpus._turretPoint.transform.position;
            _cannon.transform.position = _turret._cannonPoint.transform.position;
        }
        #endregion

        private void SetBuild()
        {
            DestroyAll();
            Assemble();
            _isCannonChange = false;
            _isSave.Value = false;
        }

        private void SaveBuild((string name, Sprite icon, BuildPopupModel buildModel) saveInfo)
        {
            if (saveInfo.buildModel._build == null)
            {
                Build build = new();
                saveInfo.buildModel._build = build;
                _isCannonChange = true;
            }
            saveInfo.buildModel._build._corpus = _corpusSO;
            saveInfo.buildModel._build._turret = _turretSO;
            saveInfo.buildModel._build._cannon = _cannonSO;
            saveInfo.buildModel._build._tracks = _tracksSO;
            saveInfo.buildModel._build._battleRating = CalculateRating();
            saveInfo.buildModel._build._name = saveInfo.name;
            saveInfo.buildModel._build._icon = saveInfo.icon;
            saveInfo.buildModel._build._shellStorageCapasity = _corpusSO._shellCapacity / _cannonSO._shellSize;
            if(_isCannonChange)
            {
                saveInfo.buildModel.UpdateBuild(true);
            }
            else saveInfo.buildModel.UpdateBuild(false);
            _isSave.Value = false ;
        }

        private float CalculateRating()
        {
            float rating = 0f;
            foreach(var part in _partsDictionary)
            {
                if(part.Value != null)
                {
                    if(part.Value._battleRating > rating)rating = part.Value._battleRating;
                }
            }
            return rating;
        }

        private void DestroyAll()
        {
            if (_tracks)
            {
                UnityEngine.Object.Destroy(_turret.gameObject);
                UnityEngine.Object.Destroy(_corpus.gameObject);
                UnityEngine.Object.Destroy(_tracks.gameObject);
                UnityEngine.Object.Destroy(_cannon.gameObject);
            }  
        }

        public void OnDestroy()
        {
            _disposables.Dispose();
        }
    }
}