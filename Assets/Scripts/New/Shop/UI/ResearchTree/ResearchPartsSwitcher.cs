using Assets.Scripts.New.Shop.UI.NationSelect;
using R3;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.New.Shop.UI.ResearchTree
{
    public class ResearchPartsSwitcher : MonoBehaviour
    {

        private Dictionary<NationName, Dictionary<PartsNames, GameObject>> _partsPopups = new() 
        {
            {NationName.USSR, new Dictionary<PartsNames, GameObject> () },
            {NationName.Germany, new Dictionary<PartsNames, GameObject> () },
            {NationName.USA, new Dictionary<PartsNames, GameObject> () },
        };

        [SerializeField] private DictonaryStruct[] _dictonaryStructs;

        [Serializable]
        public struct DictonaryStruct
        {
            public NationName _nation;
            public PartsNames _part;
            public GameObject _popup;
        }

        private NationName _currrentNation;
        private PartsNames _currrentPart;
        private GameObject _currentPopup;

        private CompositeDisposable _disposables = new();
        private CompositeDisposable _disposables1;

        [Inject]
        private void Construct()
        {
            New.Arhitecture.EventBus.Instance._selectNation.Subscribe(name => _currrentNation = name).AddTo(_disposables);
            New.Arhitecture.EventBus.Instance._panelClose.Subscribe(_ => { if (_currentPopup != null) _currentPopup.SetActive(false); }).AddTo(_disposables);
            foreach(var DStruct  in _dictonaryStructs)
            {
                var nationDict = _partsPopups[DStruct._nation];
                nationDict.Add(DStruct._part,DStruct._popup);
            }
        }

        private void OnEnable()
        {
            _disposables1 = new();
            New.Arhitecture.EventBus.Instance._selectNation.Subscribe(_ =>  ChangeNation() ).AddTo(_disposables1);
            New.Arhitecture.EventBus.Instance._selectPart.Subscribe(partName => { _currrentPart = partName; Reactivate(); }).AddTo(_disposables1);
        }

        private void OnDisable()
        {
            _disposables1.Dispose();
        }

        private void ChangeNation()
        {
            if (_currentPopup != null)
            {
               Reactivate();
            }
        }


        private void Reactivate()
        {
            if (_currentPopup != null) _currentPopup.SetActive(false);
            var nationParts = _partsPopups[_currrentNation];
            _currentPopup = nationParts[_currrentPart];
            _currentPopup.SetActive(true);
        }

    }
}