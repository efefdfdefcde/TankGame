using UnityEngine;
using UnityEngine.EventSystems;

public class InputBlocker : MonoBehaviour
{
    [SerializeField] private GameObject cameraController; // ������ �� ��������� ������ (��������, FreeLookCam)

    private void Update()
    {
        // �������� ��� �����
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0); // ���� ������ ���
            if (EventSystem.current.IsPointerOverGameObject(touch.fingerId))
            {
                DisableCameraControl(); // ��������� ���������� �������
                return;
            }
        }

        // �������� ��� ����
        if (EventSystem.current.IsPointerOverGameObject())
        {
            DisableCameraControl(); // ��������� ���������� �������
        }
        else
        {
            EnableCameraControl(); // �������� ���������� �������
        }
    }

    private void DisableCameraControl()
    {
        if (cameraController != null)
        {
            cameraController.SetActive(false); // ��������� ���������� �������
        }
    }

    private void EnableCameraControl()
    {
        if (cameraController != null)
        {
            cameraController.SetActive(true); // �������� ���������� �������
        }
    }
}
