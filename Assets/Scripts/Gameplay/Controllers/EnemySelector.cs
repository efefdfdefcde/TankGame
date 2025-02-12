using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Controllers
{
    public class EnemySelector : MonoBehaviour
    {
        public event Action<Collider> _selectedEnemy;

        [SerializeField] private Camera mainCamera; 
        [SerializeField] private LayerMask enemyLayer; 

        void Update()
        {
            HandleTouchInput();
        }

        void HandleTouchInput()
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Ended)
                {
                    Ray ray = mainCamera.ScreenPointToRay(touch.position);
                    RaycastHit hit;

                   
                    if (Physics.Raycast(ray, out hit, 100, enemyLayer))
                    {
                        _selectedEnemy?.Invoke(hit.collider);
                    }
                }
            }
        }
    }
}