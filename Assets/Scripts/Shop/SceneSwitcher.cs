using R3;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Assets.Scripts.Shop
{
    public class SceneSwitcher : MonoBehaviour
    {

        private CompositeDisposable _disposables = new();

        private static SceneSwitcher instance;

        [Inject]
        private void Construct()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }

            DontDestroyOnLoad(gameObject);
            EventBus.Instance._toBattleEvent.Subscribe(_ => StartCoroutine(SceneLoad())).AddTo(_disposables);
            EventBus.Instance._toShopEvent.Subscribe(_ => StartCoroutine(ShopLoad())).AddTo(_disposables);
        }

        private IEnumerator ShopLoad()
        {
            SceneManager.LoadScene("LoadScreen");
            yield return new WaitForSeconds(2);
            SceneManager.LoadScene("Shop");
        }

        private IEnumerator SceneLoad()
        {
            SceneManager.LoadScene("LoadScreen");
            yield return new WaitForSeconds(2);
            SceneManager.LoadScene("Gameplay");

        }

        private void OnDestroy()
        {
            _disposables.Dispose();
        }
    }
}