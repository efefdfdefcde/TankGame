using Assets;
using Assets.Scripts.Shop.ResearchTree;
using R3;
using TMPro;
using UnityEngine;

public class ResearchPresenter : MonoBehaviour
{
    [SerializeField] private GameObject _researchPPopup;
    [SerializeField] private TextMeshProUGUI _points;

    private VenicleData _data;
    private CompositeDisposable _disposable = new();

    private void Start()
    {
        EventBus.Instance._showResearchP.Subscribe(data => ShowResearchPoint(data)).AddTo(_disposable);
        EventBus.Instance._hideResearchP.Subscribe(_ => { _researchPPopup.SetActive(false); } ).AddTo(_disposable);
        EventBus.Instance._researchPUpdate.Subscribe(_ => { _points.text = _data._researchPoints.ToString(); }).AddTo(_disposable);
    }

    private void ShowResearchPoint(VenicleData data)
    {
        _data = data;
        _researchPPopup.SetActive(true);
        _points.text = data._researchPoints.ToString();
    }



    private void OnDestroy()
    {
        _disposable?.Dispose();
    }
}
