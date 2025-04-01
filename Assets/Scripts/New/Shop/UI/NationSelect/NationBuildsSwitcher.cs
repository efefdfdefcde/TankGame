using Assets.Scripts.New.Shop.UI.NationSelect;
using R3;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.New.Shop.UI
{
    public class NationBuildsSwitcher : MonoBehaviour
    {

        private Dictionary<NationName, GameObject> _nations = new();

        private GameObject _currentNation;

        [SerializeField] private NationStruct[] _nationsStructs;

        [Serializable]
        public struct NationStruct
        {
            public NationName _name;
            public GameObject _panel;
        }

        private CompositeDisposable _disposables = new();

        [Inject]
        private void Construct()
        {
            foreach(var nation in _nationsStructs)
            {
                _nations.Add(nation._name,nation._panel);
            }
            Arhitecture.EventBus.Instance._selectNation.Subscribe(name => SelectNation(name)).AddTo(_disposables);
        }

        private void SelectNation(NationName name)
        {
            var nation = _nations[name];
            if(_currentNation != nation)
            {
                if(_currentNation != null) _currentNation.SetActive(false);
                _currentNation = nation;
                _currentNation.SetActive(true);
            }
        }


        private void OnDestroy()
        {
            _disposables.Dispose();
        }
    }
}