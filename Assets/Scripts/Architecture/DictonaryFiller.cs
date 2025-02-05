using Assets.Scripts.Shop.ResearchTree;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Architecture
{
    public class DictonaryFiller : MonoBehaviour
    {

        [SerializeField] private VehicleData[] _datas;

        [Inject]
        private void Construct()
        {
            foreach (var data in _datas)
            {
                data.FillDictonary();
            }
        }
    }
}