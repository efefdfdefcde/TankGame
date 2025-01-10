using Assets.Scripts.Shop.ResearchTree.Upgrade.Upgraders;
using Assets.Scripts.Shop.ResearchTree.Upgraders;
using R3;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Shop.ResearchTree.Upgrade
{
    public class UpgradeManager : MonoBehaviour
    {

        [SerializeField] private VenicleData _data;
        [SerializeField] private UpgradeManagerView _view;
        [SerializeField] private UpgradeManagerController _controller;
        [SerializeField] private Upgrade[] _upgrades;

        private Dictionary<UpgradeDictonary, IUprade> _upgraders = new() 
        {
            {UpgradeDictonary.HP, new HPUpgrade() },
            {UpgradeDictonary.Armor, new ArmorUpgrade() },
            {UpgradeDictonary.Reload, new ReloadUpgrade() },
            {UpgradeDictonary.Speed, new SpeedUpgrade() },
            {UpgradeDictonary.Engine, new EngineUpgrade() },
            {UpgradeDictonary.Turret, new TurretUpgrade() },
            {UpgradeDictonary.ArmorPiercing, new ArmorPiercingUpgrade() },
            {UpgradeDictonary.HightExplosive, new HightExplosiveUpgrade() },
        };

        private List<StructUpgrade> _upgradeList;
        private Upgrade _currentUpgrade;
        private int _researchPrice;
        private int _price;
        private bool _buttonStatus;
        private bool _isMoney;

        private CompositeDisposable _disposable = new();

        [Inject]
        private void Construct()
        {
            _controller._upgradeEvent.Subscribe(_ => Upgrade()).AddTo(_disposable);
            _view.Construct(_data);
            foreach (var upgrade in _upgrades)
            {
                upgrade._upgradeNotAvailable.Subscribe(upgradeInfo => UpgradeNotAvailable(upgradeInfo)).AddTo(_disposable);
                upgrade._upgradeAvailable.Subscribe(upgradeInfo => UpgradeAvailable(upgradeInfo)).AddTo(_disposable);
                upgrade._upgradeResearched.Subscribe(upgradeInfo => UpgradeResearched(upgradeInfo)).AddTo(_disposable);
                upgrade._upgradeBought.Subscribe(upgradeInfo => UpgradeBought(upgradeInfo)).AddTo(_disposable);
            }
        }

        private void UpgradeNotAvailable((List<StructUpgrade> upgradeList ,string name, Sprite icon,bool isShell) upgradeInfo)
        {
            _controller.TurnStatus(false);
            _view.Init(upgradeInfo.upgradeList, upgradeInfo.name, upgradeInfo.icon, upgradeInfo.isShell);
        }

        private void UpgradeAvailable((List<StructUpgrade> upgradeList, string name, Sprite icon, bool isShell, int price, Upgrade current) upgradeInfo)
        {
            _isMoney = false;
            if(_data._researchPoints >= upgradeInfo.price)_buttonStatus = true;
            else _buttonStatus = false;
            _controller.TurnStatus(_buttonStatus);
            _view.Init((upgradeInfo.upgradeList, upgradeInfo.name, upgradeInfo.icon, upgradeInfo.isShell, upgradeInfo.price), _buttonStatus,_isMoney);
            _upgradeList = upgradeInfo.upgradeList;
            _currentUpgrade = upgradeInfo.current;
            _researchPrice = upgradeInfo.price;
           
        }

        private void UpgradeResearched((List<StructUpgrade> upgradeList,string name, Sprite icon, bool isShell, int price, Upgrade current) upgradeInfo)
        {
            _isMoney = true;
            int money = Bank._money;
            if (money >= upgradeInfo.price)_buttonStatus = true;
            else _buttonStatus = false;
            _controller.TurnStatus(_buttonStatus);
            _view.Init((upgradeInfo.upgradeList, upgradeInfo.name,upgradeInfo.icon, upgradeInfo.isShell, upgradeInfo.price),_buttonStatus,_isMoney);
            _upgradeList = upgradeInfo.upgradeList;
            _currentUpgrade = upgradeInfo.current;
            _price = upgradeInfo.price;
        }

        private void UpgradeBought((string name, Sprite icon, bool isShell) upgradeInfo)
        {
            _controller.TurnStatus(false);
            _view.Init(upgradeInfo.name, upgradeInfo.icon, upgradeInfo.isShell);
        }

        private void Upgrade()
        {
            if (_isMoney)
            {
                _currentUpgrade.UpgradeBought();
                foreach (var upgrade in _upgradeList)
                {
                    var upgrader = _upgraders[upgrade._upgradeType];
                    upgrader.Upgrade(_data, upgrade._upgradeCount);
                }
                EventBus.Instance._spendMoney?.OnNext(_price);
                _view.DataUpdate();
            }
            else
            {
                _currentUpgrade.UpgradeResearched();
                _data._researchPoints -= _researchPrice;
                EventBus.Instance._researchPUpdate.OnNext(Unit.Default);
            }
        }

        private void OnDestroy()
        {
            _disposable.Dispose();
        }
    }
}