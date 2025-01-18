using Assets.Scripts.Shop.ResearchTree.NewUpgrade.Models;
using Assets.Scripts.Shop.Shells;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Shop.ResearchTree.NewUpgrade.Popups
{
    public class ShellUpgradePopup : UpgradePopup   
    {
        [SerializeField] private ShellUpgradeInfoPopup _prefab;
        [SerializeField] private Transform _parent;


        private List<ShellUpgradeInfoPopup> _shellListPopup = new();
        private Dictionary<ShellType, List<ShellUpgradeStruct>> _shells = new();

        public void Init(Dictionary<ShellType, List<ShellUpgradeStruct>> shells)
        {
            _shells = shells;
            Spawn();
        } 

        private void Spawn()
        {
            foreach(var shellInfo in _shells)
            {
                var shellPopup = Instantiate(_prefab, _parent);
                shellPopup.Init(_data._shellInfo[shellInfo.Key], shellInfo.Value);
                _shellListPopup.Add(shellPopup);
            }
           
        }

        public override void UpgradeBought()
        {
            base.UpgradeBought();
            foreach(var shellInfo in _shellListPopup)shellInfo.Bought();
        }
    }
}