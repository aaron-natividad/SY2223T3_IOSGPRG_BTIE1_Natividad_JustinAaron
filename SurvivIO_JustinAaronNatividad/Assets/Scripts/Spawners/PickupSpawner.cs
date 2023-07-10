using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] pickups;
    [SerializeField] private Vector2 minBounds;
    [SerializeField] private Vector2 maxBounds;
    [SerializeField] private int spawnAmount;

    void Start()
    {
        for(int i = 0; i < spawnAmount; i++)
        {
            int index = Random.Range(0, pickups.Length);
            float spawnX = Random.Range(minBounds.x, maxBounds.x);
            float spawnY = Random.Range(minBounds.y, maxBounds.y);
            Instantiate(pickups[index], new Vector3(spawnX, spawnY, 0), Quaternion.identity);
        }
    }
}
