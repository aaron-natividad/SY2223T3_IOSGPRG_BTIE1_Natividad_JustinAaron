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
    [HideInInspector] public bool isAlive = true;

    private void Start()
    {
        OnHealthChange += GameUIManager.instance.UpdateHealthUI;
        OnDeath += GameManager.instance.GameOver;
        OnHealthChange(health);
    }

    public void Heal()
    {
        health++;
        OnHealthChange?.Invoke(health);
    }

    public void TakeDamage()
    {
        health--;
        OnHealthChange?.Invoke(health);

        if (health <= 0)
        {
            isAlive = false;
            OnDeath?.Invoke();
        }
    }
}
