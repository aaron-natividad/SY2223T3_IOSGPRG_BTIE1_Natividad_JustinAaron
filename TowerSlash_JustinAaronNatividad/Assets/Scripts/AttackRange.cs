using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRange : MonoBehaviour
{
    public Player player;
    public List<GameObject> enemies = new List<GameObject>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Enemy>().arrow.isActive = true;
            enemies.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            enemies.Remove(collision.gameObject);
            if (!enemy.isBeingDestroyed)
            {
                player.health.TakeDamage(1);
                enemy.KillEnemy(false, player.health.isAlive);
            }
        }
    }
}
