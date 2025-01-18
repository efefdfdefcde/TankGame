using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Architecture.SaveSistem
{
    [Serializable]
    public class ShopExitParams 
    {
        public int _money;
        public int _gold;
 

        public ShopExitParams(int money, int gold)
        {
            _money = money;
            _gold = gold;
          
        }
    }
}