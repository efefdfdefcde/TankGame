using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.UI.ShellSelector
{
    public class ShellConroller : MonoBehaviour
    {
        public event Action<ShellConroller> _selectShellAction;

        public void Select()
        {
            _selectShellAction?.Invoke(this);
        }
        
    }
}