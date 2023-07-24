using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitDetection : MonoBehaviour
{
    private List<GameObject> unitsInRange = new List<GameObject>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<HealthComponent>())
        {
            unitsInRange.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<HealthComponent>()) 
        {
            unitsInRange.Remove(collision.gameObject);
        }
    }

    public bool HasUnitsInRange()
    {
        return unitsInRange.Count > 0;
    }

    public GameObject GetFirstUnit()
    {
        return unitsInRange[0];
    }
}
