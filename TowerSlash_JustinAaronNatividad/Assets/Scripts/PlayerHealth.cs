using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public delegate void HealthChangeDelegate(int health);
    public HealthChangeDelegate OnHealthChange;

    public delegate void DeathDelegate();
    public DeathDelegate OnDeath;

    public int health;
    public bool isAlive;

    private void Start()
    {
        OnHealthChange += UIManager.instance.UpdateHealthUI;
        OnDeath += GameManager.instance.GameOver;
        OnHealthChange(health);
    }

    public void Heal(int healAmount)
    {
        health += healAmount;
        OnHealthChange?.Invoke(health);
    }

    public bool TakeDamage(int damageAmount)
    {
        health -= damageAmount;
        OnHealthChange?.Invoke(health);
        if (health <= 0)
        {
            isAlive = false;
            OnDeath?.Invoke();
        }
        return health <= 0;
    }
}
