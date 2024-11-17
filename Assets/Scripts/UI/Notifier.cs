using Assets.Scripts.TankParts.Player;
using DG.Tweening;
using R3;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class Notifier : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _notify;
        [SerializeField] private string _text;
        [SerializeField] private ShellManager ShellManager;

        private Tween _tween;
        private CompositeDisposable _disposable = new();

        private void Start()
        {
            ShellManager._shellEndEvent.Subscribe(_ => ShellNotEnoughNotify()).AddTo(_disposable);
        }

        private void ShellNotEnoughNotify()
        {
            _tween.Kill();
            _notify.text = _text;
            Color color = _notify.color;
            color.a = 1;
            _notify.color = color;
            _tween = _notify.DOFade(0, 4f);
        }

        private void OnDestroy()
        {
            _disposable.Dispose();
        }
    }
}