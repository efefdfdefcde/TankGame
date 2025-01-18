using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Shop.ResearchTree
{
    [Serializable]
    public class ShellData
    {
        public bool _isAllowed;
        public string _name;
        public int _damage;
        public int _shellPenetration;
        public int _fuseSensivity;
        public int _count;
        public Sprite _shellImage;
        public Color _selectColor;
        public GameObject _shellPrefab;
    }
}