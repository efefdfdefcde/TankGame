using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Convoy
{
    public class ConvoyPart : MonoBehaviour
    {
        public Action _convoyRetreatAction;
        public Action<ConvoyPart> _removeFromConvoyListAction;

        public virtual void Retreat()
        {

        }

        protected virtual void OnDisable()
        {
            _removeFromConvoyListAction?.Invoke(this);
        }

    }
}