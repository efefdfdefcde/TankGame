using Assets.Scripts.Shop.ResearchTree;
using R3;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Shop.Shells
{
    public class ShellStorageModel : MonoBehaviour
    {
        public Subject<VehicleData> _setDataEvent = new();

        [SerializeField] private PlayerDataManager _dataManager;

        private VehicleData _data;

        private CompositeDisposable _disposable = new();

        [Inject]
        private void Construct()
        {
            _dataManager._setVenicleEvent.Subscribe(data => { _data = data; _setDataEvent?.OnNext(_data); }).AddTo(_disposable);
        }



        private void OnDestroy()
        {
            _disposable.Dispose();
        }

    }
}