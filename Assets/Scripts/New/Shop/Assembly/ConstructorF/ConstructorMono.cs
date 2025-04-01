using System.Collections;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.New.Shop.Assembly.ConstructorF
{
    public class ConstructorMono : MonoBehaviour
    {
        [SerializeField] private Transform _trackPoint;
        [SerializeField] private ConstructorView _view;

        private Constructor _constructor;
        public ConstructorPresenter _presenter { get; private set; }

        [Inject]
        private void Construct()
        {
            _constructor = new(_trackPoint);
            _presenter = new(_constructor,_view);
            _constructor.Init(_presenter);
        }

        private void OnDestroy()
        {
            _constructor.OnDestroy();
            _presenter.OnDestroy();
        }
    }
}