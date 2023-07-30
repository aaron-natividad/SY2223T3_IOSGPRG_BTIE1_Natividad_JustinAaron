using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDrop : MonoBehaviour
{
    [SerializeField] private HealthComponent health;

    private void Start()
    {
        health.OnDeath += GameManager.instance.DropGrenadeLauncher;
    }
}
