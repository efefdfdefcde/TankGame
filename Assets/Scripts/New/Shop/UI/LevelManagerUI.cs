using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.New.Shop.UI
{
    public class LevelManagerUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _level;
        [SerializeField] private Image _progress;

        public void UpdateLevel(float expirience, float levelExperience, int currentLevel)
        {
            float progress = expirience / levelExperience;
            _progress.fillAmount = progress;
            _level.text = currentLevel.ToString();
        }
    }
}