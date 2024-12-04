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
        public int _researchPoints;

        public ShopExitParams(int money, int gold, int researchPoints)
        {
            _money = money;
            _gold = gold;
            _researchPoints = researchPoints;
        }
    }
}