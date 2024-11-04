using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Convoy
{
    [Serializable]
    public class Convoy 
    {
        public event Action _convoyRetreatAction;
        public event Action _convoyDestroyAction; 

        public bool _isSelected;
        public Transform _spawnPoint;
        public ConvoyPath _path;
        public List<ConvoyPartData> _convoyData;
        private List<ConvoyPart> _convoyParts = new();

        public void ConvoyPartSubscribe(ConvoyPart convoyPart)
        {
            _convoyParts.Add(convoyPart);
            convoyPart._convoyRetreatAction += ConvoyRetreat;
            convoyPart._removeFromConvoyListAction += ConvoyPartRemove;
        }

        private void ConvoyRetreat()
        {
            _convoyRetreatAction?.Invoke();
        }

        private void ConvoyPartRemove(ConvoyPart convoyPart)
        {
            _convoyParts.Remove(convoyPart);
            convoyPart._convoyRetreatAction -= ConvoyRetreat;
            convoyPart._removeFromConvoyListAction -= ConvoyPartRemove;
            if (_convoyParts.Count == 0)
            {
                _convoyDestroyAction?.Invoke();
            }
        }


    }
}