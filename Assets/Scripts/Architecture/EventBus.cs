using Assets.Scripts.Shop.ResearchTree;
using Assets.Scripts.Shop.Shells;
using R3;
using System;
using System.Collections;
using UnityEngine;

namespace Assets
{
    public class EventBus 
    {

        private static EventBus _instance;

        public static EventBus Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new EventBus();
                }
                return _instance;
            }
        }


        public Action<EnemiesList> _enemyDeathAction;
        public Action<Transform> _returnPersuitPoint;


        //UIShop
        public Subject<Unit> _panelCloseEvent = new();
        public Subject<Unit> _hidePopup = new();
        public Subject<Unit> _showPopup = new();
        public Subject<VehicleData> _showResearchP = new();    
        public Subject<Unit> _hideResearchP = new();
        public Subject<Unit> _researchPUpdate = new();

        //Upgrade
        public Subject<Unit> _spawnPopup = new();

        //Bank
        public Subject<int> _spendMoney = new();

        //Level
        public Subject<int> _getExpirience = new();

        //ShellStorage
        public Subject<Unit> _shellUpdate = new();
        //ShellPopup
        public Subject<(int, ShellType)> _shellCountChanged = new();
    }
}