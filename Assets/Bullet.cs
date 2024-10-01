using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _damage;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            collision.collider.GetComponent<HealthRoot>().TakeDamage(_damage);
        }
        Destroy(gameObject);
    }
}
