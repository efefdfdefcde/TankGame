using System;
using UnityEngine;

namespace Assets.Scripts.Architecture.SaveSistem
{
    [Serializable]
    public class ShopEnterParams
    {
        public int _money;
        public int _researchPoints;
        public int _gold;

        public ShopEnterParams(int money, int researchPoint, int gold)
        {
            _money = money;
            _researchPoints = researchPoint;
            _gold = gold;
        }
    }
}
