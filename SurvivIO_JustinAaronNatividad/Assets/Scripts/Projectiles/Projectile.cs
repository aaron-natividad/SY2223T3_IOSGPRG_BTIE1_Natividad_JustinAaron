using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Projectile Parameters")]
    [SerializeField] protected float bulletSpeed = 10;
    [SerializeField] protected float maxBulletLife;

    protected Rigidbody2D rigidBody;
    protected int bulletDamage;

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
            OnObstacleHit();
            Destroy(gameObject);
        }
        else if (collision.GetComponent<HealthComponent>())
        {
            OnUnitHit(collision.GetComponent<HealthComponent>());
            Destroy(gameObject);
        }
    }

    protected virtual void OnObstacleHit()
    {

    }

    protected virtual void OnUnitHit(HealthComponent health)
    {

    }
}
