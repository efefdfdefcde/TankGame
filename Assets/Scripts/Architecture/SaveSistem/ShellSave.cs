using Assets.Scripts.Shop.ResearchTree;
using System;

namespace Assets.Scripts.Architecture.SaveSistem
{
    [Serializable]
    public class ShellSave 
    {
        public bool _isAllowed;
        public string _name;
        public int _damage;
        public int _shellPenetration;
        public int _fuseSensivity;
        public int _count;

        public ShellSave(ShellData data)
        {
            _isAllowed = data._isAllowed;
            _name = data._name;
            _damage = data._damage;
            _shellPenetration = data._shellPenetration;
            _fuseSensivity = data._fuseSensivity;
            _count = data._count;
        }

        public void SetParams(ShellData data)
        {
            _isAllowed = data._isAllowed;
            _name = data._name;
            _damage = data._damage;
            _shellPenetration = data._shellPenetration;
            _fuseSensivity = data._fuseSensivity;
            _count = data._count;
        }
    }
}