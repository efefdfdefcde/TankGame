using System.Collections;
using UnityEngine;

namespace Assets.Scripts.New.Shop.PartsSO.Shells
{
    [CreateAssetMenu(fileName = "Shell", menuName = "ScriptableObjects/Shell")]
    public class Shell : ScriptableObject
    {
        public Color _color;
        public Sprite _icon;
        public string _name;

    }
}