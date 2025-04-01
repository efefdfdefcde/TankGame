using Assets.Scripts.New.Shop.UI.NationSelect;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.New.Shop
{
    [CreateAssetMenu(fileName = "NationStorage", menuName = "ScriptableObjects/NationStorage", order = 5)]
    public class NationStorage : ScriptableObject
    {
        public NationName _name;
        public int _researchPoints;

    }
}