using Assets.Scripts.Architecture;
using R3;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Shop.ResearchTree
{
    public class NextPopupButton : MonoBehaviour
    {

        [SerializeField] private VehicleData _data;
        [SerializeField] private Button _next;

        private void Start()
        {
            _next.onClick.AddListener(NextPopup);
        }

        private void NextPopup()
        {
            EventBus.Instance._nextPopupEvent.OnNext(_data);
            EventBus.Instance._showResearchP.OnNext(_data);
        }

        private void OnDestroy()
        {
            _next.onClick.RemoveListener(NextPopup);
        }
    }
}