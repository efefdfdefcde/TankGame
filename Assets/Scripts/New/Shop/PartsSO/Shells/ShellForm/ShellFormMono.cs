using Assets.Scripts.New.Shop.PartsSO.Shells.ShellForm;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.New.Shop.PartsSO.Shells
{
    public class ShellFormMono : MonoBehaviour
    {
        [SerializeField] private ShellFormView _view;
        [SerializeField] private Slider _slider;
        [SerializeField] private Button _plus;
        [SerializeField] private Button _minus;


        public ShellFormModel _model { get; private set;}
        private ShellFormController _controller;
        public ShellMenuInfo _menuInfo{get;private set;}

        public void Init(Shell shell, CannonShell shellCharacteristics,int count,int maxCount,ShellType type,ShellMenuInfo menuInfo)
        {
            _controller = new(_slider,_plus,_minus, count, maxCount);
            _view.Init(shell, shellCharacteristics, count);
            menuInfo.Init(maxCount,shell._icon,shell._name,count);
            _menuInfo = menuInfo;
            _model = new(_controller,_view,count,type,menuInfo);
        }

        private void OnDestroy()
        {
            _model.OnDestroy();
            _controller.OnDestroy();
        }
    }
}