using Assets.Scripts.TankParts.Player;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class FuelUI : MonoBehaviour
    {
        [SerializeField] private FuelPool _fuelPool;
        [SerializeField] protected Image _fuelImage;

        private void Start()
        {
            _fuelPool._fuelUpdateAction += FuelChange;
        }

        private void FuelChange(float maxFuel, float currentFuel)
        {
            float fuel = currentFuel/maxFuel;
            _fuelImage.fillAmount = fuel;
        }

        private void OnDestroy()
        {
            _fuelPool._fuelUpdateAction -= FuelChange;
        }

    }
}