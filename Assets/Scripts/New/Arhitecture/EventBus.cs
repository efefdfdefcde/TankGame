using Assets.Scripts.New.Shop;
using Assets.Scripts.New.Shop.Assembly;
using Assets.Scripts.New.Shop.PartsSO.Shells;
using Assets.Scripts.New.Shop.UI.BuildPopup;
using Assets.Scripts.New.Shop.UI.NationSelect;
using Assets.Scripts.New.Shop.UI.ResearchTree;
using Assets.Scripts.New.Shop.Upgrades;
using R3;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.New.Arhitecture
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

        public Subject<Unit> _panelClose = new();

        public Subject<GameObject> _menuInfo = new();

        public Subject<GameObject> _openPanel = new();
        public Subject<NationName> _selectNation = new();
        public Subject<PartsNames> _selectPart = new();
        //Upgrade
        public Subject<Unit> _spawnPopup = new();
        public ReplaySubject<UpgradeMono> _upgradeBought = new();
        public Subject<(NationName,ShellType)> _shellAwailable = new();

        //Bank
        public Subject<int> _spendMoney = new();

        //Level
        public Subject<int> _getExpirience = new();

        public Subject<int> _researchPUpdate = new();

        //Scenes
        public Subject<Unit> _toShopEvent = new();
        public Subject<Unit> _toBattleEvent = new();

        public Subject<TankPartSO> _setPartEvent = new();

        public Subject<BuildPopupModel> _sendBuild = new();

        public Subject<float> _battleRatingUpdate = new();

        public Subject<(NationName,int)> _cellSelect = new();

        public Subject<Unit> _shellsRespawn = new();
    }
}