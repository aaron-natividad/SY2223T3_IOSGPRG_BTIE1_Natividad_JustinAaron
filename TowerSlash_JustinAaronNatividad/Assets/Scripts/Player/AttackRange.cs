using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRange : MonoBehaviour
{
    public Player player;
    [HideInInspector] public List<GameObject> enemies = new List<GameObject>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        if (enemy)
        {
            enemy.arrow.isActive = true;
            enemies.Add(enemy.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        if (enemy)
        {
            enemies.Remove(collision.gameObject);
            if (!enemy.isBeingDestroyed)
            {
                player.health.TakeDamage();
                enemy.KillEnemy(false);
            }
        }
    }
}
