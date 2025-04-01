using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.New.Shop.PartsSO.Shells.ShellForm
{
    public class ShellMenuInfo : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _name;
        [SerializeField] private TextMeshProUGUI _count;
        [SerializeField] private Image _icon;
        [SerializeField] private Image _filable;

        private int _storageCapacity;

        public void Init(int storageCapasity,Sprite icon, string name,int count)
        {
            _storageCapacity = storageCapasity;
            _icon.sprite = icon;
            _name.text = name;
            _filable.fillAmount = (float)count / _storageCapacity;
            _count.text = count.ToString();
        }

        public void CountUpdate(int count)
        {
            _filable.fillAmount = (float)count / _storageCapacity;
            _count.text = count.ToString();
        }
    }
}