using Assets.Scripts.New.Arhitecture;
using Assets.Scripts.New.Shop.UI;
using R3;
using System;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.New.Shop
{
    public class ShopBank : MonoBehaviour
    {
        public Subject<Unit> _bankInitialize = new();
        public Subject<int> _moneyChanged = new();
        public Subject<int> _goldChanged = new();
        public Subject<int> _researchPChanged = new();

        [SerializeField] private DataManagerShop _dataManager;
        [SerializeField] private BankUI _UI;

        public static int _money { get; private set; } = 600;
        public static int _gold { get; private set; } = 200;
        private NationStorage _currentNation;

        private CompositeDisposable _disposables = new();

        [Inject]
        private void Construct()
        {
            New.Arhitecture.EventBus.Instance._spendMoney.Subscribe(money => SpendMoney(money)).AddTo(_disposables);
            _dataManager._bankInitialize.Subscribe(data => Initialize(data)).AddTo(_disposables);
            _dataManager._changeNation.Subscribe(nation => ChangeNation(nation)).AddTo(_disposables);
            _dataManager._experienceInit.Subscribe(exp => NationInit(exp)).AddTo(_disposables);
            New.Arhitecture.EventBus.Instance._researchPUpdate.Subscribe(points => _researchPChanged.OnNext(points)).AddTo(_disposables);
        }

        private void Start()
        {
            _bankInitialize.OnNext(Unit.Default);
            _bankInitialize.OnCompleted();
            _moneyChanged.OnNext(_money);
            _goldChanged.OnNext(_gold);
        }

        private void Initialize((int money, int gold, int enterMoney, int enterGold) data)
        {
            _gold = data.gold;
            _money = data.money;
            _gold += data.enterGold;
            _money += data.enterMoney;
        }

        private void NationInit((int experience, NationStorage storage) data)
        {
            data.storage._researchPoints += data.experience;
            _currentNation = data.storage;
            _researchPChanged.OnNext(_currentNation._researchPoints);
        }


        private void SpendMoney(int money)
        {
            _money -= money;
            _moneyChanged.OnNext(_money);
        }

        private void ChangeNation(NationStorage storage)
        {
            _currentNation = storage;
            _researchPChanged.OnNext(_currentNation._researchPoints);
        }

        private void OnDestroy()
        {
            _disposables.Dispose();
        }
    }
}