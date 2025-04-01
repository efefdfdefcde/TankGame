using Assets.Scripts.New.Shop.Assembly.Parts;
using Assets.Scripts.New.Shop.PartsSO.Shells;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.New.Shop
{
    [CreateAssetMenu(fileName = "Cannon", menuName = "ScriptableObjects/Cannon", order = 5)]
    public class Cannon : TankPartSO
    {
        public CannonPart _shopPrefab;
        public CannonPart _gameplayPrefab;
        public float _reload;
        public int _shellSize;
        public CannonShell[] _shells;
    }

    [Serializable]
    public struct CannonShell
    {
        public ShellType _type;
        public int _damage;
        public int _piersing;
        public int _fuseSensivity;
    }
}