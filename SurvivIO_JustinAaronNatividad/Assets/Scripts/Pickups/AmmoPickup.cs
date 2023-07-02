using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : Pickup
{
    [SerializeField] private AmmoType type;
    [SerializeField] private int minAmmoAmount;
    [SerializeField] private int maxAmmoAmount;

    protected override void DoPickup()
    {
        if (unitInventory.IsAmmoFull(type))
        {
            return;
        }

        int ammoAmount = Random.Range(minAmmoAmount,maxAmmoAmount + 1);
        unitInventory.AddAmmo(type, ammoAmount);
        Destroy(gameObject);
    }
}
