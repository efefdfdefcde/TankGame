using System.Collections;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.New.Shop.PartsSO.Shells
{
    public class ShellManagerView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _storageCapasity;
        [SerializeField] private TextMeshProUGUI _curentCount;
        [SerializeField] private TextMeshProUGUI _menuStorageCapasity;
        [SerializeField] private TextMeshProUGUI _menuCurentCount;

        public void CountUpdate(int count)
        {
            _curentCount.text = count.ToString();
            _menuCurentCount.text = count.ToString();
        }

        public void CapasityUpdate(int capasity)
        {
            _storageCapasity.text = capasity.ToString();
            _menuStorageCapasity.text = capasity.ToString();
        }
    }
}