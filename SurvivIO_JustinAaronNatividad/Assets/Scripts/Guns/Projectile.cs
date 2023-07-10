using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Temporary code for projectile for gun testing
    [SerializeField] private float bulletSpeed;

    private void Start()
    {
        // Temporarily destroy projectiles after 2 seconds to prevent lag
        Destroy(gameObject, 2f);
    }

    public void Initialize(float bSpeed)
    {
        bulletSpeed = bSpeed;
    }

    private void FixedUpdate()
    {
        transform.position += transform.up * bulletSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle") || collision.GetComponent<MovementComponent>())
        {
            Destroy(gameObject);
        }
    }
}
