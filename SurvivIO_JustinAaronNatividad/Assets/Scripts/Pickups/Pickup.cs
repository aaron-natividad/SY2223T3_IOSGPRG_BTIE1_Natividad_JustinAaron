using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pickup : MonoBehaviour
{
    protected InventoryComponent unitInventory;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<InventoryComponent>())
        {
            unitInventory = collision.GetComponent<InventoryComponent>();
            if (unitInventory.canLoot)
            {
                DoPickup();
            }
        }
    }

    protected abstract void DoPickup();
}
