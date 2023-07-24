using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPickup : Pickup
{
    [SerializeField] private GameObject gunPrefab;
    [Space(10)]
    [SerializeField] private int minAmmoAmount;
    [SerializeField] private int maxAmmoAmount;

    protected override void DoPickup()
    {
        Gun newGun = gunPrefab.GetComponent<Gun>();
        Gun oldGun = unitInventory.GetGun(newGun.gunType);
        
        if (oldGun != null)
        {
            if (oldGun.gunName == newGun.gunName)
            {
                int ammoAmount = Random.Range(minAmmoAmount, maxAmmoAmount + 1);
                unitInventory.ModifyAmmo(newGun.ammoType, ammoAmount);
                Destroy(gameObject);
                return;
            }
        }

        unitInventory.AddGun(gunPrefab);
        Destroy(gameObject);
    }
}
