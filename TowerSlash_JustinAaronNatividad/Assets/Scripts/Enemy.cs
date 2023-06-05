using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType { Grounded, Flying }

public class Enemy : MonoBehaviour
{
    public delegate void EnemyDestroyDelegate(GameObject self, bool isDestroyedByPlayer, bool spawnNewEnemy);
    public EnemyDestroyDelegate OnEnemyDestroy;

    public EnemyType type;
    public Arrow arrow;
    public bool isBeingDestroyed;

    private void Start()
    {
        GameManager.instance.player.health.OnDeath += ClearEnemy;
        arrow.SetValue((int)Random.Range(0, 4));
    }

    public void ClearEnemy()
    {
        Destroy(gameObject);
    }

    public void KillEnemy(bool isDestroyedByPlayer, bool spawnNewEnemy)
    {
        if (isBeingDestroyed) return;
        isBeingDestroyed = true;

        OnEnemyDestroy?.Invoke(gameObject, isDestroyedByPlayer, spawnNewEnemy);
        Destroy(gameObject);
    }

    public bool CompareDirection(SwipeDirection direction)
    {
        return arrow.arrowValue == arrow.SwipeDirectionToInt(direction);
    }

    private void OnDestroy()
    {
        GameManager.instance.player.health.OnDeath -= ClearEnemy;
    }
}
