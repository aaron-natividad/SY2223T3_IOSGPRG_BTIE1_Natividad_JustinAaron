using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    public event Action<float> OnHealthChange;
    public event Action<GameObject> OnDeath;

    [SerializeField] private int maxHealth;
    private int currentHealth;

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
            OnDeath?.Invoke(gameObject);
            Destroy(gameObject);
        }
    }

    public float GetHealthPercentage()
    {
        return (float)currentHealth / maxHealth;
    }
}
