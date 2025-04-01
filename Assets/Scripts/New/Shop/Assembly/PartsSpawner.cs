using Assets.Scripts.New.Shop.Assembly.ConstructorF;
using Assets.Scripts.New.Shop.UI.NationSelect;
using Assets.Scripts.New.Shop.UI.ResearchTree;
using R3;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.New.Shop.Assembly
{
    public class PartsSpawner : MonoBehaviour
    {
        [SerializeField] private Transform _spawnParent;
        [SerializeField] private AssemblyPopupMono _popupPrefab;
        [SerializeField] private ConstructorMono _presenter;

        private NationName _currentNation;
        private PartsNames _currentPart;

        private Dictionary<NationName,Dictionary<PartsNames,List<TankPartSO>>> _parts = new();

        private Dictionary<PartsNames,TankPartSO> _currentParts = new() 
        {
            { PartsNames.Corpus, null },
            { PartsNames.Turret, null },
            { PartsNames.Cannon, null },
            { PartsNames.Tracks, null }
        };

        private TankPartSO _selectedPart;

        private CompositeDisposable _disposables = new();
        private List<AssemblyPopupMono> _spawnedParts = new();

        private CompositeDisposable _disposable1;

        [Inject]
        private void Construct()
        {
            New.Arhitecture.EventBus.Instance._selectNation.Subscribe(name => _currentNation = name).AddTo(_disposables);
        }

        private void Start()
        {
            TankPartSO[] parts = Resources.LoadAll<TankPartSO>("ScriptableObjects/Parts");
            foreach (TankPartSO part in parts)
            {
                if (!_parts.ContainsKey(part._nation))
                {
                    _parts.Add(part._nation,new Dictionary<PartsNames, List<TankPartSO>>());
                }
                if (!_parts[part._nation].ContainsKey(part._part))
                {
                    _parts[part._nation].Add(part._part, new List<TankPartSO>());
                }
                _parts[part._nation][part._part].Add(part);
            }
            _presenter._presenter.Corpus.Subscribe(corpus => _currentParts[PartsNames.Corpus] = corpus).AddTo(_disposables);
            _presenter._presenter.Turret.Subscribe(turret => _currentParts[PartsNames.Turret] = turret).AddTo(_disposables);
            _presenter._presenter.Cannon.Subscribe(cannon => _currentParts[PartsNames.Cannon] = cannon).AddTo(_disposables);
            _presenter._presenter.Track.Subscribe(track => _currentParts[PartsNames.Tracks] = track).AddTo(_disposables);
            _presenter._presenter._reselect.Subscribe(_ => Reselect()).AddTo(_disposables);
        }

        private void OnEnable()
        {
            _disposable1 = new CompositeDisposable();
            New.Arhitecture.EventBus.Instance._selectPart.Subscribe(part => SpawnParts(part)).AddTo(_disposable1);
            New.Arhitecture.EventBus.Instance._selectNation.Subscribe(name => ChangeNation()).AddTo(_disposable1);
        }

        private void OnDisable()
        {
            DestroyParts();
            _disposable1.Dispose();
        }

        private void Reselect()
        {
            _selectedPart = _currentParts[_currentPart];
            foreach (var part in _spawnedParts)
            {
                if(part._part == _selectedPart)part.SpawnInit();
                else part.Unselect();
            }
        }

        private void ChangeNation()
        {
            if(_spawnedParts.Count > 0)
            {
                SpawnParts(_currentPart);
            }
        }

        private void SpawnParts(PartsNames name)
        {
            _currentPart = name;
            _selectedPart = _currentParts[_currentPart]; 
            foreach (var part in _spawnedParts)
            {
                Destroy(part.gameObject);
            }
            _spawnedParts.Clear();
            var nationDictonary = _parts[_currentNation];
            var partList = nationDictonary[_currentPart];
            foreach (var part in partList)
            {
                if (part._isAwailable)
                {
                    var AssemblyPopup = Instantiate(_popupPrefab, _spawnParent);
                    AssemblyPopup.Spawn(part);
                    _spawnedParts.Add(AssemblyPopup);
                    if(_selectedPart == part)AssemblyPopup.SpawnInit();
                }              
            }
        }

        private void DestroyParts()
        {
            foreach (var part in _spawnedParts)
            {
                Destroy(part.gameObject);
            }
            _spawnedParts.Clear();
        }

    }

   
}