using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PickupSpawner : Spawner
{
    [Header("Pickup Spawner")]
    [SerializeField] private GameObject[] gunPickups;
    [SerializeField] private GameObject[] ammoPickups;
    [Space(10)]
    [SerializeField] private float gunSpawnWeight;

    protected override void SpawnPrefab(Vector3 spawnPosition)
    {
        GameObject[] pickupArray = GetPickupArray();
        int index = Random.Range(0, pickupArray.Length);
        Instantiate(pickupArray[index], spawnPosition, Quaternion.identity);
    }

    private GameObject[] GetPickupArray()
    {
        float weight = Random.Range(0f, 1f);
        return weight <= gunSpawnWeight ? gunPickups : ammoPickups;
    }
}
