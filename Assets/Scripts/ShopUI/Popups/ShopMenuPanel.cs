﻿using R3;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Shop
{
    public class ShopMenuPanel : MonoBehaviour
    {
        public static Subject<GameObject> _panelShowEvent = new();


        [SerializeField] private ShopMenuButton _button;

        private GameObject _panel;

        [Inject]
        private void Construct()
        {
            _panel = gameObject;
            _button._buttonClick.Subscribe(_ => SendPanel()).AddTo(this);
        }

        private void SendPanel() => _panelShowEvent?.OnNext(_panel);
    }
}