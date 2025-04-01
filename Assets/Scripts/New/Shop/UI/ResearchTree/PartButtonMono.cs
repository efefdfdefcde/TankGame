using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.New.Shop.UI.ResearchTree
{
    public class PartButtonMono : MonoBehaviour
    {
        [SerializeField] private PartsNames _name;
        [SerializeField] private Button _button;
        [SerializeField] private PartButtonView _view;

        private PartButtonController _controller;
        private PartButtonModel _model;

        private void Start()
        {
            _controller = new(_button);
            _model = new(_controller,_name,_view);
        }

        private void OnDestroy()
        {
            _controller.OnDestroy();
            _model.OnDestroy();
        }
    }
}