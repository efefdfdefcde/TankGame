using Assets.Scripts.New.Shop.UI.NationSelect;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.Scripts.New.Shop.UI.BuildPopup
{
    public class BuildPopupMono : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private BuildPopupView _view;
        [SerializeField, Range(1, 7)] private int _cellNumber;
        [SerializeField] private NationName _nation;

        private BuildPopupController _controller;
        private BuildPopupModel _model;
        [Inject]
        private DiContainer _container;

        private void Start()
        {
            _controller = new(_button);
            _model = new(_controller, _view,_cellNumber,_nation);   
            _container.Inject(_model);
        }

        private void OnDestroy()
        {
            _controller.OnDestroy();
            _model.OnDestroy();
        }
    }
}