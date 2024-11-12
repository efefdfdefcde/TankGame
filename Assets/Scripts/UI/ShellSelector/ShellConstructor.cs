using Assets.Scripts.Architecture;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.UI.ShellSelector
{
    public class ShellConstructor : MonoBehaviour
    {

        [SerializeField] private ShellView _view;
        [SerializeField] private ShellConroller _conroller;

        public void ConstructView(ShellData shellData, int ShellCount) => _view.Construct(shellData, ShellCount);

        public (ShellView, ShellConroller) GetViewController() => (_view, _conroller);
    }
}