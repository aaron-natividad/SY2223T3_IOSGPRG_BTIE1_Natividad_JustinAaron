using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    public delegate void HealthChangeDelegate(float healthPercentage);
    public HealthChangeDelegate OnHealthChange;

    public delegate void DeathDelegate();
    public DeathDelegate OnDeath;

    public int maxHealth;

    public int currentHealth
    {
        get;
        private set;
    }

    private void Start()
    {
        currentHealth = maxHealth;
        OnHealthChange?.Invoke(GetHealthPercentage());
    }

    public void Heal(int healAmount)
    {
        currentHealth = Mathf.Clamp(currentHealth + healAmount, 0, maxHealth);
        OnHealthChange?.Invoke(GetHealthPercentage());
    }

    public void TakeDamage(int damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, maxHealth);
        OnHealthChange?.Invoke(GetHealthPercentage());
        if (currentHealth <= 0)
        {
            OnDeath?.Invoke();
        }
    }

    public float GetHealthPercentage()
    {
        return currentHealth / maxHealth;
    }
}
