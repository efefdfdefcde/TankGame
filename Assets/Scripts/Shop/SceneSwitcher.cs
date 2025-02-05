using R3;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Assets.Scripts.Shop
{
    public class SceneSwitcher : MonoBehaviour
    {
        [SerializeField] private BattleButton _button;

        private CompositeDisposable _disposables = new();

        [Inject]
        private void Construct()
        {
            DontDestroyOnLoad(gameObject);
            _button._toBattleEvent.Subscribe(_ => SceneLoad()).AddTo(_disposables);
        }

        private void SceneLoad()
        {
            SceneManager.LoadScene("LoadScreen");
            SceneManager.LoadScene("Gameplay");

        }

        private void OnDestroy()
        {
            _disposables.Dispose();
        }
    }
}