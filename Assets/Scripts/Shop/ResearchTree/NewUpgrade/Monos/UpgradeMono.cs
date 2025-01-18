using R3;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts.Shop.ResearchTree.NewUpgrade.Monos
{
    public class UpgradeMono : MonoBehaviour
    {
        public Subject<R3.Unit> _upgradeBought = new();

        protected UpgradePresenter _presenter;
        [SerializeField] protected UpgradeMono _previousUpgrade;

        [SerializeField] protected int _researchPrice;
        [SerializeField] protected int _moneyPrice;
        [SerializeField] protected VehicleData _vehicleData;

        [SerializeField] protected UpgradeStatusDictonary _status;

        private CompositeDisposable _disposable = new();
        
        protected virtual void Start()
        {
            _presenter.Init(_previousUpgrade);
            _presenter._upgradeBought.Subscribe(_ => _upgradeBought?.OnNext(R3.Unit.Default)).AddTo(_disposable);
        }

        private void OnDestroy()
        {
            _presenter.OnDestroy();
        }
    }
}