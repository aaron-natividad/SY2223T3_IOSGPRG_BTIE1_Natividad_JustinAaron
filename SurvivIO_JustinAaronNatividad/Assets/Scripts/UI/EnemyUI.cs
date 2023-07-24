using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyUI : MonoBehaviour
{
    [SerializeField] private GameObject healthBarParent;
    [SerializeField] private Image healthbar;
    [SerializeField] private float showTime;

    private HealthComponent health;
    private float healthShowTimer;

    private void Start()
    {
        health = GetComponentInParent<HealthComponent>();
        healthbar.fillAmount = health.GetHealthPercentage();
        health.OnHealthChange += UpdateHealth;
        healthBarParent.SetActive(false);
    }

    private void FixedUpdate()
    {
        transform.up = Vector2.up;
        healthShowTimer = Mathf.Max(healthShowTimer - Time.deltaTime, 0);

        if (healthShowTimer <= 0 && healthBarParent.activeSelf)
        {
            healthBarParent.SetActive(false);
        }
    }

    public void UpdateHealth(float healthPercentage)
    {
        healthbar.fillAmount = healthPercentage;
        healthBarParent.SetActive(true);
        healthShowTimer = showTime;
    }
}
