using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class HealthRootUI : MonoBehaviour
    {
        [SerializeField] private Image _healthImage;

        private void Start()
        {
            PlayerHealthRoot._instance._healthChangedAction += HealthChanged;
        }

        private void HealthChanged(float maxHealth,float currentHealth)
        {
            float health = currentHealth/maxHealth;
            _healthImage.fillAmount = health;
        }

        private void OnDestroy()
        {
            PlayerHealthRoot._instance._healthChangedAction -= HealthChanged;
        }

    }
}