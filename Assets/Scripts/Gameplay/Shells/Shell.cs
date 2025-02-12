using Assets;
using UnityEngine;

public class Shell : MonoBehaviour
{
    [SerializeField] protected float _damage;
    [SerializeField] protected float _shellPenetration;
    [SerializeField] protected float _fuseSensitivity;


    protected virtual void OnCollisionEnter(Collision collision)
    {
       
        Destroy(gameObject);
    }
}
