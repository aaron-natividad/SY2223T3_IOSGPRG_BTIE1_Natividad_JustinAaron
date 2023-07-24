using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float bulletSpeed = 10;
    [SerializeField] private float maxBulletLife;

    private Rigidbody2D rigidBody;
    private int bulletDamage;

    public void Initialize(int bulletDamage)
    {
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.velocity = transform.up.normalized * bulletSpeed;

        this.bulletDamage = bulletDamage;
        Destroy(gameObject, maxBulletLife);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
        else if (collision.GetComponent<HealthComponent>())
        {
            collision.GetComponent<HealthComponent>().TakeDamage(bulletDamage);
            Destroy(gameObject);
        }
    }
}
