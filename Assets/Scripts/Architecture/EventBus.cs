using System;
using System.Collections;
using UnityEngine;

namespace Assets
{
    public class EventBus 
    {

        public static EventBus _instance;

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
    }
}