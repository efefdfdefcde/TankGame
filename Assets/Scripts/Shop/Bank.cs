using Assets.Scripts.Architecture.SaveSistem;
using R3;
using UnityEngine;
using Zenject;

public class Bank : MonoBehaviour
{
    public Subject<int> _moneyChangedEvent = new();
    public Subject<int> _goldChangedEvent = new();

    private int _money;
    private int _gold;


    [Inject]
    private void Construct(ShopExitParams exitParams,ShopEnterParams enterParams)
    {
        _money = exitParams._money;
        _gold = exitParams._gold;
        _money += enterParams._money;
        _gold += enterParams._gold;
    }

    private void Awake()
    {
        _moneyChangedEvent?.OnNext(_money);
        _goldChangedEvent?.OnNext(_gold);
    }
}
