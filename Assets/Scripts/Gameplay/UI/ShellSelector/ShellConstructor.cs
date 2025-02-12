using Assets.Scripts.Architecture;
using Assets.Scripts.Shop.ResearchTree;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.UI.ShellSelector
{
    public class ShellConstructor : MonoBehaviour
    {

        [SerializeField] private ShellView _view;
        [SerializeField] private ShellConroller _conroller;

        public void ConstructView(ShellData shellData) => _view.Construct(shellData);

        public (ShellView, ShellConroller) GetViewController() => (_view, _conroller);
    }
}