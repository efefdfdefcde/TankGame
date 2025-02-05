using Assets;
using Assets.Scripts.Architecture;
using Assets.Scripts.Architecture.SaveSistem;
using R3;
using UnityEngine;
using Zenject;

public class Bank : MonoBehaviour
{
    public Subject<Unit> _bankInitialize = new();
    public Subject<int> _moneyChangedEvent = new();
    public Subject<int> _goldChangedEvent = new();

    [SerializeField] private DataManagerShop _dataManager;

    public static int _money {  get; private set; }
    public static int _gold {  get; private set; }

    private CompositeDisposable _disposable = new();

    [Inject]
    private void Construct()
    {
        EventBus.Instance._spendMoney.Subscribe(money => SpendMoney(money)).AddTo(_disposable);
        _dataManager._bankInitialize.Subscribe(data => Init(data)).AddTo(_disposable);
    }

    private void Start()
    {
        _bankInitialize.OnNext(Unit.Default);
        _bankInitialize.OnCompleted();
        _moneyChangedEvent?.OnNext(_money);
        _goldChangedEvent?.OnNext(_gold);
    }


    private void Init((int exitParamsMoney, int exitParamsGold, int enterParamsMoney, int enterParamsGold) bankData)
    {
        _money = bankData.exitParamsMoney;
        _gold = bankData.exitParamsGold;
        _money += bankData.enterParamsMoney;
        _gold += bankData.enterParamsGold;
    }

    private void SpendMoney(int money)
    {
        _money -= money;
        _moneyChangedEvent.OnNext(_money);
    }

    private void OnDestroy()
    {
        _disposable.Dispose();
    }
}
