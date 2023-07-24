using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : Spawner
{
    [Header("Enemy Spawner")]
    [SerializeField] private GameObject enemyPrefab;

    private List<GameObject> spawnedEnemies = new List<GameObject>();

    protected override void SpawnPrefab(Vector3 spawnPosition)
    {
        GameObject spawnedUnit = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        spawnedUnit.GetComponent<HealthComponent>().OnDeath = RemoveEnemyFromList;
        spawnedEnemies.Add(spawnedUnit);
    }

    public void RemoveEnemyFromList(GameObject enemy)
    {
        spawnedEnemies.Remove(enemy);
    }
}
