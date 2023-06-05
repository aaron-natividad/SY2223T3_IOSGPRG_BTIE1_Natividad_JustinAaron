using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType { Grounded, Flying }

public class Enemy : MonoBehaviour
{
    public delegate void EnemyDestroyDelegate(GameObject self, bool isDestroyedByPlayer);
    public EnemyDestroyDelegate OnEnemyDestroy;

    public delegate void PowerupDelegate();
    public PowerupDelegate OnPowerup;

    public EnemyType type;
    public Arrow arrow;
    public int pointValue;
    public float powerupChance;

    [HideInInspector] public bool isBeingDestroyed;

    private void OnEnable()
    {
        GameManager.instance.player.health.OnDeath += ClearEnemy;
        OnEnemyDestroy += GameManager.instance.ReceivePoints;
        OnPowerup += GameManager.instance.player.health.Heal;
    }

    private void OnDisable()
    {
        GameManager.instance.player.health.OnDeath -= ClearEnemy;
        OnEnemyDestroy -= GameManager.instance.ReceivePoints;
        OnPowerup -= GameManager.instance.player.health.Heal;
    }

    private void Start()
    {
        arrow.SetValue(Random.Range(0, 4));
    }

    public void ClearEnemy()
    {
        Destroy(gameObject);
    }

    public void KillEnemy(bool isDestroyedByPlayer)
    {
        if (isBeingDestroyed) return;
        isBeingDestroyed = true;

        if(isDestroyedByPlayer && CalculatePowerupDrop(powerupChance))
        {
            OnPowerup?.Invoke();
        }

        OnEnemyDestroy?.Invoke(gameObject, isDestroyedByPlayer);
        Destroy(gameObject);
    }

    public bool CalculatePowerupDrop(float dropChance)
    {
        float dropRoll = Random.Range(0f, 1f);
        return dropChance >= dropRoll;
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
