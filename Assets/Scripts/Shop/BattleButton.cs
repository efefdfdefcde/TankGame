using R3;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.Scripts.Shop
{
    public class BattleButton : MonoBehaviour
    {
        public Subject<Unit> _saveData = new();
        public Subject<Unit> _toBattleEvent = new();

        [SerializeField] private Button _toBattle;

        [Inject]
        private void Construct()
        {
            _toBattle.onClick.AddListener(ToBattle);
        }

        private void ToBattle()
        {
            _saveData.OnNext(Unit.Default);
            _toBattleEvent.OnNext(Unit.Default);
        }

        private void OnDestroy()
        {
            _toBattle.onClick.RemoveAllListeners();
        }
    }
}