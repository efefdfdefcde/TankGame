using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.New.Arhitecture.SaveSistem
{
    [Serializable]
    public class ShopEnterParams 
    {
        public int _money;
        public int _gold;
        public int _experience;
     

        public ShopEnterParams(int money, int experience, int gold)
        {
            _experience = experience;
            _money = money;
            _gold = gold;
        }
       
    }
}