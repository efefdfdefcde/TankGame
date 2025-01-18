using Assets.Scripts.Shop.ResearchTree.NewUpgrade.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Shop.ResearchTree.NewUpgrade.Popups
{
    public class ShellUpgradeInfoPopup : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _name;
        [SerializeField] private TextMeshProUGUI _damage;
        [SerializeField] private TextMeshProUGUI _piercing;
        [SerializeField] private TextMeshProUGUI _fuseSensivity;

        private Dictionary<ShellCharacteristic,TextMeshProUGUI> _characteristics = new();

        [Serializable]
        public struct StructForDictonary
        {
            public ShellCharacteristic characteristicName;
            public TextMeshProUGUI characteristicString;
        }

        [SerializeField] private List<StructForDictonary> _structForDictonaries;

   
        private ShellData _data;

        public void Init(ShellData shell, List<ShellUpgradeStruct> upgradeStructs)
        {
            _data = shell;
            _name.text = _data._name;
            _damage.text = _data._damage.ToString();
            _piercing.text = _data._shellPenetration.ToString();
            _fuseSensivity.text = _data._fuseSensivity.ToString();
            foreach(var str in _structForDictonaries)_characteristics.Add(str.characteristicName,str.characteristicString);
            foreach (ShellUpgradeStruct upgrade in upgradeStructs)
            {
                var upgradeString = _characteristics[upgrade._characteristic];
                upgradeString.text = upgrade._upgradeValue.ToString();
            }
        }

        public void Bought()
        {
            _damage.text = _data._damage.ToString();
            _piercing.text = _data._shellPenetration.ToString();
            _fuseSensivity.text = _data._fuseSensivity.ToString();
            foreach(var Char in _characteristics) Char.Value.text = null;
        }

    }
}