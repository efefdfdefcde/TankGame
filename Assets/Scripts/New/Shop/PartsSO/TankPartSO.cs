using Assets.Scripts.New.Shop.UI.NationSelect;
using Assets.Scripts.New.Shop.UI.ResearchTree;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.New.Shop
{
    public class TankPartSO : ScriptableObject
    {
        public float _battleRating;
        public bool _isAwailable;
        public NationName _nation;
        public PartsNames _part;
        public Sprite _icon;
        public string _name;

    }
}