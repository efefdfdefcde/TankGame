using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.New.Shop.Assembly
{
    public class AssemblyPopupMono : MonoBehaviour
    {
        [SerializeField] private AssemblyPopupView _view;
        [SerializeField] private Button _button;

        private AssemblyPopupModel _model;
        private AssemblyPopupController _controller;

        public TankPartSO _part { get; private set; }

        public void Spawn(TankPartSO part)
        {
            _part = part;
            _view.SetPart(part);
            _controller = new(_button);
            _model = new(_controller,_view,part);   
        }

        public void SpawnInit()
        {
            _view.Select();
        }

        public void Unselect()
        {
            _view.Unselect();
        }

        private void OnDestroy()
        {
            _model.OnDestroy();
            _controller.OnDestroy();
        }

    }
}