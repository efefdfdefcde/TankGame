using Assets.Scripts.Architecture;
using Assets.Scripts.Shop.ResearchTree;
using R3;
using UnityEngine;
using Zenject;

public class VenicleContainerModel : MonoBehaviour
{
    public static Subject<VehicleData> _openInfoPanel = new();

    [SerializeField] private VenicleContainerView _view;
    [SerializeField] private VenicleContainerController _controller;
    [SerializeField] private VehicleData _data;

    private CompositeDisposable _disposable = new();

    [Inject]
    private void Construct()
    {
        _controller._openInfoEvent.Subscribe(_ => OpenInfo()).AddTo(_disposable);
        _view.Init(_data);
    }

    private void OpenInfo()
    {
        _openInfoPanel?.OnNext(_data);
    }

    private void OnDestroy()
    {
        _disposable.Dispose();
    }
}
