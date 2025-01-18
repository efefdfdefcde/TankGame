using Assets.Scripts.Shop.ResearchTree;
using R3;
using UnityEngine;
using Zenject;

public class PlayerDataManager : MonoBehaviour
{
    public Subject<VehicleData> _setVenicleEvent = new();

    [SerializeField] private VehicleData _playerVenicle;


    private void Start()
    {
        _setVenicleEvent?.OnNext(_playerVenicle);
    }
}
