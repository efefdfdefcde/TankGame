using Assets.Scripts.New.Shop;
using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.New.Arhitecture.SaveSistem
{
    [Serializable]
    public class NationStorageSave 
    {
        public int _researchPoints;

        public NationStorageSave(NationStorage storage)
        {
            _researchPoints = storage._researchPoints;
        }
    }
}