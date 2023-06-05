using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GaugeState { Inactive, Active, Cooldown }

public class PlayerDashGauge : MonoBehaviour
{
    public Player player;
    public GaugeState state = GaugeState.Inactive;

    public float maxGaugeValue = 100;
    public float gaugeIncrease = 5;
    public float gaugeDecreaseSpeed;
    public float activeTime;

    private float internalGaugeValue = 0;
    private float internalTimer = 0;

    private void FixedUpdate()
    {
        switch (state)
        {
            case GaugeState.Inactive:
                internalGaugeValue = Mathf.Clamp(internalGaugeValue - Time.deltaTime * gaugeDecreaseSpeed, 0, 100);
                if (internalGaugeValue >= 100)
                {
                    state = GaugeState.Active;
                }
                break;
            case GaugeState.Active:
                internalGaugeValue = 100;
                break;
            case GaugeState.Cooldown:
                internalGaugeValue = 0;
                internalTimer -= Time.deltaTime;
                if(internalTimer <= 0)
                {
                    internalTimer = 0;
                    state = GaugeState.Inactive;
                }
                break;
        }
    }

    public void IncreaseGauge()
    {
        internalGaugeValue += gaugeIncrease;
    }

    public void Activate()
    {
        internalTimer = activeTime;
        state = GaugeState.Cooldown;
        player.StartCoroutine(player.CO_Autoplay());
    }

    public float GetGaugePercentage()
    {
        return internalGaugeValue / maxGaugeValue;
    }

    public float GetActivePercentage()
    {
        return internalTimer / activeTime;
    }
}
