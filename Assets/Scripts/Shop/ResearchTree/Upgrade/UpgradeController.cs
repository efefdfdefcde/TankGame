using R3;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Shop.ResearchTree.Upgrade
{
    public class UpgradeController : MonoBehaviour
    {
        public Subject<Unit> _upgradeInfoEvent = new();

        [SerializeField] private Button _button;

        private void Start()
        {
            _button.onClick.AddListener(UpgradeInfo);
        }

        private void UpgradeInfo()
        {
            _upgradeInfoEvent.OnNext(Unit.Default);
        }


    }
}