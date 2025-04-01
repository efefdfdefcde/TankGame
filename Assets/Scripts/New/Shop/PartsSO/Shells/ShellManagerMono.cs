using Assets.Scripts.New.Shop.PartsSO.Shells.ShellForm;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.New.Shop.PartsSO.Shells
{
    public class ShellManagerMono : MonoBehaviour
    {
        [SerializeField] private ShellManagerView _view;
        [SerializeField] private ShellFormMono _form;
        [SerializeField] private ShellMenuInfo _menuInfo;
        [SerializeField] private Transform _parent;
        [SerializeField] private Transform _menuParent;
        [SerializeField] private ShellCatalog _catalog;

        private ShellManagerModel _model;
        private ShellManagerPresenter _presenter;

        [Inject]
        private void Construct()
        {
            _model = new(_form,_menuInfo,_parent,_menuParent,_catalog);
            _presenter = new(_model, _view);
            _model.Init(_presenter);
        }

        private void OnDestroy()
        {
            _model.OnDestroy();
        }

    }
}