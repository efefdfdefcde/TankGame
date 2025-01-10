using Assets;
using Assets.Scripts.Architecture.SaveSistem;
using R3;
using UnityEngine;
using Zenject;

public class Bank : MonoBehaviour
{
    public Subject<int> _moneyChangedEvent = new();
    public Subject<int> _goldChangedEvent = new();

    public static int _money {  get; private set; }
    public static int _gold {  get; private set; }

    private CompositeDisposable _disposable = new();

    [Inject]
    private void Construct(ShopExitParams exitParams,ShopEnterParams enterParams)
    {
        _money = exitParams._money;
        _gold = exitParams._gold;
        _money += enterParams._money;
        _gold += enterParams._gold;
        EventBus.Instance._spendMoney.Subscribe(money => SpendMoney(money)).AddTo(_disposable);
    }

    private void Awake()
    {
        _moneyChangedEvent?.OnNext(_money);
        _goldChangedEvent?.OnNext(_gold);
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
