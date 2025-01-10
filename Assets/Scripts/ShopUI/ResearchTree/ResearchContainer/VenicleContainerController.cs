using R3;
using UnityEngine;
using UnityEngine.UI;

public class VenicleContainerController : MonoBehaviour
{
    public Subject<Unit> _openInfoEvent = new();

    [SerializeField] private Button _open;

    private void Start()
    {
        _open.onClick.AddListener(OpenInfo);
    }

    private void OpenInfo()
    {
        _openInfoEvent?.OnNext(Unit.Default);
    }

    private void OnDestroy()
    {
        _open.onClick.RemoveListener(OpenInfo);
    }
}
