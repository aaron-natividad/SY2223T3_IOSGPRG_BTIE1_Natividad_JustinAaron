using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : Projectile
{
    [Header("Rocket Parameters")]
    [SerializeField] private LayerMask unitMask;
    [SerializeField] private GameObject explosionPrefab;
    [Space(10)]
    [SerializeField] private float explosionRadius;

    protected override void OnObstacleHit()
    {
        Explode();
    }

    protected override void OnUnitHit(HealthComponent health)
    {
        Explode();
    }

    private void Explode()
    {
        GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        explosion.transform.localScale = Vector3.one * explosionRadius * 2;

        Collider2D[] hitUnits = Physics2D.OverlapCircleAll(transform.position, explosionRadius, unitMask);
        foreach(Collider2D unit in hitUnits)
        {
            unit.GetComponent<HealthComponent>().TakeDamage(bulletDamage);
        }
    }
}
