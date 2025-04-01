using R3;
using System.Collections;
using TMPro;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.New.Shop.UI
{
    public class BankUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _gold;
        [SerializeField] private TextMeshProUGUI _money;
        [SerializeField] private TextMeshProUGUI _experience;
        [SerializeField] private ShopBank _bank;

        private CompositeDisposable _disposables = new();

        [Inject]
        private void Construct()
        {
            _bank._goldChanged.Subscribe(gold => UpdateGold(gold)).AddTo(_disposables);
            _bank._moneyChanged.Subscribe(money => UpdateMoney(money)).AddTo(_disposables);
            _bank._researchPChanged.Subscribe(resP => ResPChange(resP)).AddTo(_disposables);
        }

        private void UpdateMoney(int money) => _money.text = money.ToString();

        private void UpdateGold(int gold) => _gold.text = gold.ToString();

        private void ResPChange(int experience)
        {
            _experience.text = experience.ToString();
        }
       
    }
}