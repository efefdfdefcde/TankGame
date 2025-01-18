using System.Collections;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Shop.ResearchTree.NewUpgrade.Popups
{
    public class ShellUnlockPopup : UpgradePopup
    {
        [SerializeField] private TextMeshProUGUI _damage;
        [SerializeField] private TextMeshProUGUI _shellPiercing;
        [SerializeField] private TextMeshProUGUI _fuseSensivity;
        [SerializeField] private TextMeshProUGUI _status;


        public void Init(ShellData shell)
        {
            _damage.text = shell._damage.ToString();
            _shellPiercing.text = shell._shellPenetration.ToString();
            _fuseSensivity.text = shell._fuseSensivity.ToString();
            _status.text = "No";

        }

        public override void UpgradeBought()
        {
            base.UpgradeBought();
            _status.text = "Yes";
        }

    }
}