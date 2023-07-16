using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Temporary code for projectile for gun testing
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float maxBulletLife;

    private Rigidbody2D rigidBody;

    public void Initialize(float bSpeed)
    {
        bulletSpeed = bSpeed;
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.velocity = transform.up.normalized * bulletSpeed;

        // Temporarily destroy projectiles after 2 seconds to prevent lag
        Destroy(gameObject, maxBulletLife);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle") || collision.GetComponent<MovementComponent>())
        {
            Destroy(gameObject);
        }
    }
}
