using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitDetection : MonoBehaviour
{
    [HideInInspector] public List<GameObject> units = new List<GameObject>();

    [SerializeField] private AIController controller;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<HealthComponent>())
        {
            units.Add(collision.gameObject);
            controller.SetEnemyState(EnemyState.Shooting);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<HealthComponent>()) 
        {
            units.Remove(collision.gameObject);
            if (units.Count <= 0)
            {
                controller.SetEnemyState(EnemyState.Patrol);
            }
        }
    }
}
