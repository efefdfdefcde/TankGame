using System.Collections;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Gameplay.UI
{
    public class BankUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _money;
        [SerializeField] private TextMeshProUGUI _reserchPoints;

        public void PointsUpdate(int money,int researchP)
        {
            _money.text = money.ToString();
            _reserchPoints.text = researchP.ToString();
        }
    }
}