using Assets.Scripts.Architecture;
using Assets.Scripts.Shop.ResearchTree;
using R3;
using UnityEngine;
using Zenject;

public class VehicleChanger : MonoBehaviour
{
    public Subject<Unit> _initEvent = new();
    public Subject<VehicleData> _setVenicleEvent = new();

    [SerializeField] private DataManagerShop _dataManager;
    [SerializeField] private VehicleData _data;
    //Override reactive property

    [SerializeField] private ResearchInfoPopup _infoPopup;

    private CompositeDisposable _disposable = new();

    [Inject]
    private void Construct()
    {
        _dataManager._setVehicleWay.Subscribe(vehicleWay => Init(vehicleWay)).AddTo(_disposable);
        _infoPopup._changeEvent.Subscribe(data => SetVehicle(data)).AddTo(_disposable);
    }

    private void Init(string way)
    {
        VehicleData data = Resources.Load<VehicleData>($"ScriptableObjects/VehicleDatas/{way}");
        _data = data;
    }

    private void Start()
    {
        _initEvent.OnNext(Unit.Default);
        _initEvent.OnCompleted();
        _setVenicleEvent?.OnNext(_data);
    }

    private void SetVehicle(VehicleData data)
    {
        _data = data;
        _setVenicleEvent.OnNext(_data);
    }

    private void OnDestroy()
    {
        _disposable.Dispose();
    }
}
