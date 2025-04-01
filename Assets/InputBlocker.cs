using UnityEngine;
using UnityEngine.EventSystems;

public class InputBlocker : MonoBehaviour
{
    [SerializeField] private GameObject cameraController; // Ссылка на компонент камеры (например, FreeLookCam)

    private void Update()
    {
        // Проверка для тачей
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0); // Берём первый тач
            if (EventSystem.current.IsPointerOverGameObject(touch.fingerId))
            {
                DisableCameraControl(); // Отключаем управление камерой
                return;
            }
        }

        // Проверка для мыши
        if (EventSystem.current.IsPointerOverGameObject())
        {
            DisableCameraControl(); // Отключаем управление камерой
        }
        else
        {
            EnableCameraControl(); // Включаем управление камерой
        }
    }

    private void DisableCameraControl()
    {
        if (cameraController != null)
        {
            cameraController.SetActive(false); // Отключаем управление камерой
        }
    }

    private void EnableCameraControl()
    {
        if (cameraController != null)
        {
            cameraController.SetActive(true); // Включаем управление камерой
        }
    }
}
