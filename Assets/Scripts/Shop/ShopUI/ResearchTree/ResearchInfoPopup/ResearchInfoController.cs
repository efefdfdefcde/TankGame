using R3;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.Scripts.Shop.ResearchTree
{
    public class ResearchInfoController : MonoBehaviour
    {
        public Subject<Unit> _demonstrateEvent = new();
        public Subject<Unit> _researchEvent = new();
        public Subject<Unit> _closeEvent = new();
        public Subject<Unit> _changeVehicle = new();

        [SerializeField] private Button _lookButton;
        [SerializeField] private Button _researchButton;
        [SerializeField] private Button _changeeButton;
        [SerializeField] private Button _close;

        [Inject]
        private void Construct()
        {
            _researchButton.onClick.AddListener(ResearchSignal);
            _close.onClick.AddListener(ClosePanel);
            _lookButton.onClick.AddListener(DemonstrateSignal);
            _changeeButton.onClick.AddListener(ChangeVehicleSignal);
        }

        private void DemonstrateSignal()
        {
            _demonstrateEvent?.OnNext(Unit.Default);
        }

        private void ResearchSignal()
        {
            _researchEvent?.OnNext(Unit.Default);
        }

        private void ClosePanel()
        {
            _closeEvent?.OnNext(Unit.Default);
        }

        private void ChangeVehicleSignal()
        {
            _changeVehicle.OnNext(Unit.Default);
        }

        private void OnDestroy()
        {
            _researchButton.onClick.RemoveListener(ResearchSignal);
            _close.onClick.RemoveListener(ClosePanel);
            _lookButton.onClick.RemoveListener(DemonstrateSignal);
            _changeeButton.onClick.RemoveListener(ChangeVehicleSignal);
        }

    }
}