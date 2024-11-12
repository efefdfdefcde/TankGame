using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Architecture
{
    [CreateAssetMenu(fileName = "ShellData", menuName = "ScriptableObjects/ShellData", order = 2)]
    public class ShellData : ScriptableObject
    {
        public Sprite _shellImage;
        public Color _selectColor;
        public GameObject _shellPrefab;
        public float _damage;
        public float _shellPenetration;
        public float _fuseSensitivity;
        
    }
}