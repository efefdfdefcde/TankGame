using Assets.Scripts.Shop.ResearchTree;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Architecture
{
    public class DictonaryFiller : MonoBehaviour
    {

        [SerializeField] private VehicleData[] _datas;

        private void Awake()
        {
            foreach (var data in _datas)
            {
                data.FillDictonary();
            }
        }
    }
}