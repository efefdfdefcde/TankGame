using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _damage;
    [SerializeField] private string _targetTag;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag(_targetTag))
        {
            collision.collider.GetComponent<HealthRoot>().TakeDamage(_damage);
        }
        Destroy(gameObject);
    }
}
