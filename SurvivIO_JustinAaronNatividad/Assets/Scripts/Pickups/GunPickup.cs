using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPickup : Pickup
{
    [SerializeField] private GameObject gunPrefab;
    [SerializeField] private string gunName;

    protected override void DoPickup()
    {
        if (unitInventory.IsValidGun(gunName))
        {
            unitInventory.AddGun(gunPrefab);
            Destroy(gameObject);
        }
    }
}
