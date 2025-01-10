using Assets.Scripts.Shop.ResearchTree;
using R3;
using UnityEngine;
using Zenject;

public class PlayerDataManager : MonoBehaviour
{
    public Subject<VenicleData> _setVenicleEvent = new();

    [SerializeField] private VenicleData _playerVenicle;


    private void Start()
    {
        _setVenicleEvent?.OnNext(_playerVenicle);
    }
}
