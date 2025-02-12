using R3;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.Scripts.Gameplay
{
    public class Base : MonoBehaviour
    {
        public Subject<Unit> _playerOnBase = new();

        [SerializeField] private Button _testBurron;//Test

        [Inject]
        private void Construct()
        {
            _testBurron.onClick.AddListener(PlayerReturn);
        }

        private void PlayerReturn()
        {
            _playerOnBase.OnNext(Unit.Default);
        }

        private void OnDestroy()
        {
            _testBurron.onClick.RemoveAllListeners();
        }

    }
}