using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyUI : MonoBehaviour
{
    [SerializeField] private Image healthbar;

    private HealthComponent health;

    private void Start()
    {
        health = GetComponentInParent<HealthComponent>();
        UpdateHealth(health.GetHealthPercentage());
        health.OnHealthChange += UpdateHealth;
    }

    public void UpdateHealth(float healthPercentage)
    {
        healthbar.fillAmount = healthPercentage;
    }
}
