using Assets.Scripts.New.Arhitecture.SaveSistem;
using Assets.Scripts.New.Shop.Assembly;
using Assets.Scripts.New.Shop.UI.NationSelect;
using R3;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;
using Zenject;

namespace Assets.Scripts.New.Shop.UI.BuildPopup
{
    public class BuildPopupModel 
    {
        private int _cellNumber;
        private NationName _nationName;
        private bool _selected;

        private BuildPopupController _controller;
        private BuildPopupView _view;
        public Build _build;
        private BuildSave _save;
        Dictionary<(int, NationName), BuildSave> _buildDictionary;
        Dictionary<NationName, int> _seletedCells;

        private CompositeDisposable _disposables = new();

        public BuildPopupModel(BuildPopupController controller, BuildPopupView view,int cellNumber,NationName nationName)
        {
            _cellNumber = cellNumber;
            _nationName = nationName;
            _controller = controller;
            _controller._selectBuild.Subscribe(_ => SendBuild()).AddTo(_disposables);
            New.Arhitecture.EventBus.Instance._cellSelect.Subscribe(info => Unselect(info)).AddTo(_disposables);
            New.Arhitecture.EventBus.Instance._selectNation.Subscribe(name => NationChange(name)).AddTo(_disposables);
            _view = view;
        }

        [Inject]
        private void Load(Dictionary<(int, NationName), BuildSave> buildDictionary,Dictionary<NationName,int> seletedCells)
        {
            _buildDictionary = buildDictionary;
            if(_buildDictionary.TryGetValue((_cellNumber,_nationName),out _save))
            {
                _build = new();
                _build._corpus = Resources.Load<Corpus>($"ScriptableObjects/Parts/{_save._corpus}");
                _build._turret = Resources.Load<Turret>($"ScriptableObjects/Parts/{_save._turret}");
                _build._cannon = Resources.Load<Cannon>($"ScriptableObjects/Parts/{_save._cannon}");
                _build._tracks = Resources.Load<Tracks>($"ScriptableObjects/Parts/{_save._tracks}");
                _build._name = _save._name;
                Texture2D texture = new Texture2D(2, 2); 
                texture.LoadImage(_save._icon); 
                _build._icon = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
                _build._battleRating = _save._battleRating;
                _build._shellStorageCapasity = _save._shellStorageCapasity;
                _build._shellsStorage = _save._shellsStorage;
                _view.UpdateBuildView(_build._name, _build._icon, _build._battleRating);
            }
            _seletedCells = seletedCells;
            if (_seletedCells[_nationName] == _cellNumber)
            {
                SendBuild();
            }
        }


        private void SendBuild()
        {
            New.Arhitecture.EventBus.Instance._sendBuild.OnNext(this);
            New.Arhitecture.EventBus.Instance._cellSelect.OnNext((_nationName, _cellNumber));
            float rating;
            if (_build != null) rating = _build._battleRating;
            else rating = 0.0f;
            New.Arhitecture.EventBus.Instance._battleRatingUpdate.OnNext(rating);
            _view.Select();
            _selected = true;
            _seletedCells[_nationName] = _cellNumber;
        }

        private void Unselect((NationName nation,int cellNumber) info)
        {
            if(info.nation == _nationName && info.cellNumber != _cellNumber)
            {
                _selected = false;
                _view.Unselect();
            }       
        }

        private void NationChange(NationName name)
        {
            if (name == _nationName & _selected) SendBuild(); 
        }

        public void UpdateBuild(bool updateShells)
        {
            _view.UpdateBuildView(_build._name, _build._icon, _build._battleRating);
            if (updateShells)
            {
                _build.StorageUpdate();
            }   
            New.Arhitecture.EventBus.Instance._battleRatingUpdate.OnNext(_build._battleRating);
            Save();
        }

        private void Save()
        {
            if (_save == null)
            {
                _save = new BuildSave();
                _buildDictionary.Add((_cellNumber, _nationName), _save);
            }
            _save._corpus = _build._corpus.name;
            _save._turret = _build._turret.name;
            _save._cannon = _build._cannon.name;
            _save._tracks = _build._tracks.name;
            _save._name = _build._name;
            _save._icon = _build._icon.texture.EncodeToPNG();
            _save._battleRating = _build._battleRating;
            _save._shellStorageCapasity = _build._shellStorageCapasity;
            _save._shellsStorage = _build._shellsStorage;
            New.Arhitecture.EventBus.Instance._sendBuild.OnNext(this);
        }

        public void OnDestroy()
        {
            _disposables.Dispose();
        }
    }
}