using Assets.Scripts.Shop.ResearchTree;
using R3;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.Scripts.ShopUI.ResearchTree
{
    public class LocalResearchButton : MonoBehaviour
    {
        public Subject<VehicleData> _researchVenicleEvent = new();

        [SerializeField] private Button _button;
        [SerializeField] private PlayerDataManager _researchManager;

        private VehicleData _playerVenicle;
        private CompositeDisposable _disposable = new();

        [Inject]
        private void Construct()
        {
            _button.onClick.AddListener(OpenResearch);
            _researchManager._setVenicleEvent.Subscribe(playerVenicle => SetData(playerVenicle)).AddTo(_disposable);
        }

        private void OpenResearch()
        {
            _researchVenicleEvent?.OnNext(_playerVenicle);
        }

        private void SetData(VehicleData playerVenicle)
        {
            _playerVenicle = playerVenicle;
        }

        private void OnDestroy()
        {
            _disposable.Dispose();
        }
    }
}