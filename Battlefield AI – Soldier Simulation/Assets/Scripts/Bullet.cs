using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public float lifetime = 3f;
    public int damage = 10;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        // Move forward relative to the bullet's rotation
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    // Changed from OnCollisionEnter to OnTriggerEnter
    void OnTriggerEnter(Collider other)
    {
        // Check if we hit an object on the Enemy layer or with the Enemy tag
        if (other.gameObject.CompareTag("Enemy") || other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            EnemyHealth enemyHealth = other.gameObject.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
                Debug.Log("Bullet successfully triggered hit on enemy!");
            }
        }

        // Destroy the bullet on impact
        Destroy(gameObject);
    }
}