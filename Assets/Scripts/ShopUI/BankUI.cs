﻿using R3;
using System.Collections;
using TMPro;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.ShopUI
{
    public class BankUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _money;
        [SerializeField] private TextMeshProUGUI _gold;
        [SerializeField] private Bank _bank;

        [Inject]
        private void Construct()
        {
            _bank._moneyChangedEvent.Subscribe(money => UpdateMoney(money)).AddTo(this);
            _bank._goldChangedEvent.Subscribe(gold => UpdateGold(gold)).AddTo(this);
        }

        private void UpdateMoney(int money) => _money.text = money.ToString();

        private void UpdateGold(int gold) => _gold.text = gold.ToString();

    }
}