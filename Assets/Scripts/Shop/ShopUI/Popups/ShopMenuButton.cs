using R3;
using UnityEngine;
using UnityEngine.UI;

public class ShopMenuButton : MonoBehaviour
{
    public Subject<Unit> _buttonClick = new();

    [SerializeField] private Button _button;

    private void Awake() => _button.onClick.AddListener(ButtonClick);
    
    private void ButtonClick() => _buttonClick.OnNext(Unit.Default);

    private void OnDestroy() => _button.onClick.RemoveListener(ButtonClick);
   
}
