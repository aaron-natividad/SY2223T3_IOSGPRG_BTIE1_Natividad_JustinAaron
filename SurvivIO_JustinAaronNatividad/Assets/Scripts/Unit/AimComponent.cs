using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimComponent : MonoBehaviour
{
    public bool isFiring;
    private InventoryComponent inventory;

    private void Start()
    {
        inventory = GetComponent<InventoryComponent>();
    }

    private void FixedUpdate()
    {
        if (inventory.GetEquippedGun() != null)
        {
            inventory.GetEquippedGun().Fire(isFiring);
        }
    }

    public void AimUnit(Vector2 aimDirection)
    {
        transform.up = aimDirection;
    }

    public void SetIsFiring(bool isFiring)
    {
        this.isFiring = isFiring;
    }
}
