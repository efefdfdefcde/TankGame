using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.Scripts.New.Shop.UI.NationSelect
{
    public class SelectNationButtonMono : MonoBehaviour
    {

        [SerializeField] private Button _selectButton;
        [SerializeField] private NationName _name;
        [SerializeField] private SelectNationButtonView _view;
        

        private SelectNationButtonController _controller;
        private SelectNationButtonModel _model;

        [Inject]
        private void Construct()
        {
            _controller = new(_selectButton);
            _model = new(_controller,_name,_view);
        }

        private void OnDestroy()
        {
            _controller.OnDestroy();
            _model.OnDestroy();
        }
    }
}