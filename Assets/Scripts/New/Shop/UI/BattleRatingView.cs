using R3;
using System.Collections;
using TMPro;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.New.Shop.UI
{
    public class BattleRatingView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _rating;

        private CompositeDisposable _disposables = new();

        [Inject]
        private void Construct()
        {
            New.Arhitecture.EventBus.Instance._battleRatingUpdate.Subscribe(rating => UpdateRating(rating)).AddTo(_disposables);
        }

        private void UpdateRating(float rating)
        {
            _rating.text = rating.ToString();
        }

        private void OnDestroy()
        {
            _disposables.Dispose();
        }
    }
}