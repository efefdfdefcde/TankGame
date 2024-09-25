using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Joystic : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Image _joystickBackground;
    [SerializeField] private Image _joystick;
    [SerializeField] private Image _joystickArea;

    private Vector2 _joystickBackgroundStartPosition;

    private Vector2 _inputVector;

    private void Start()
    {
        _joystickBackgroundStartPosition = _joystickBackground.rectTransform.anchoredPosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 joystickPosition;

        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(_joystickBackground.rectTransform, eventData.position, null, out joystickPosition))
        {
            joystickPosition.x = (joystickPosition.x * 2 / _joystickBackground.rectTransform.sizeDelta.x);
            joystickPosition.y = (joystickPosition.y * 2 / _joystickBackground.rectTransform.sizeDelta.y);

            _inputVector = new Vector2(joystickPosition.x, joystickPosition.y);

            _inputVector = (_inputVector.magnitude > 1f) ? _inputVector.normalized : _inputVector;
            _joystick.rectTransform.anchoredPosition = new Vector2(_inputVector.x *(_joystickBackground.rectTransform.sizeDelta.x /2), _inputVector.y * (_joystickBackground.rectTransform.sizeDelta.y /2));
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Vector2 joysticBackgroundPosition;

        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(_joystickArea.rectTransform, eventData.position, null, out joysticBackgroundPosition))
        {
            _joystickBackground.rectTransform.anchoredPosition = new Vector2(joysticBackgroundPosition.x, joysticBackgroundPosition.y);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _joystickBackground.rectTransform.anchoredPosition = _joystickBackgroundStartPosition;
        _inputVector = Vector2.zero;
        _joystick.rectTransform.anchoredPosition = Vector2.zero;
    }
}
