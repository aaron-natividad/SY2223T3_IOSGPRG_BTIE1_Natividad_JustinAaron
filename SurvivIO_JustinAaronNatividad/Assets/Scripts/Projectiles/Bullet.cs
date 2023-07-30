using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Projectile
{
    [Header("Bullet Particles")]
    [SerializeField] private GameObject sparkParticle;
    [SerializeField] private GameObject bloodParticle;

    protected override void OnObstacleHit()
    {
        Instantiate(sparkParticle, transform.position, Quaternion.identity);
    }

    protected override void OnUnitHit(HealthComponent health)
    {
        Instantiate(bloodParticle, transform.position, Quaternion.identity);
        health.TakeDamage(bulletDamage);
    }
}
