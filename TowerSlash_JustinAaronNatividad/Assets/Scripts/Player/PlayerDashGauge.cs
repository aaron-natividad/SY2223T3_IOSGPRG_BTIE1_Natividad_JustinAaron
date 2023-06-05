using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GaugeState { Inactive, Active, Cooldown }

public class PlayerDashGauge : MonoBehaviour
{
    public delegate void PowerActiveDelegate();
    public PowerActiveDelegate OnPowerActivate;
    
    public float gaugeIncrease = 5;
    public float gaugeDecreaseSpeed;
    public float activeTime;

    [HideInInspector] public GaugeState state = GaugeState.Inactive;

    private float maxGaugeValue = 100;
    private float internalGaugeValue = 0;
    private float internalTimer = 0;

    private void FixedUpdate()
    {
        switch (state)
        {
            case GaugeState.Inactive:
                internalGaugeValue = Mathf.Clamp(internalGaugeValue - Time.deltaTime * gaugeDecreaseSpeed, 0, maxGaugeValue);
                if (internalGaugeValue >= maxGaugeValue)
                {
                    state = GaugeState.Active;
                }
                break;
            case GaugeState.Active:
                internalGaugeValue = maxGaugeValue;
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
        OnPowerActivate?.Invoke();
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
