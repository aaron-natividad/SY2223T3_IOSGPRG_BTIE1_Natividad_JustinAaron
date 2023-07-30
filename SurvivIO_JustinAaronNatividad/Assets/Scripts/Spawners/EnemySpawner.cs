using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : Spawner
{
    public static event Action<int> OnEnemyCountChanged;
    public static event Action OnEnemiesDepleted;

    [Header("Enemy Spawner")]
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject bossPrefab;

    private List<GameObject> spawnedEnemies = new List<GameObject>();

    protected override void OnEnter()
    {
        OnEnemyCountChanged?.Invoke(spawnAmount);
    }

    protected override void SpawnPrefab(Vector3 spawnPosition)
    {
        GameObject prefabToSpawn = spawnedCount == spawnAmount - 1 ? bossPrefab : enemyPrefab;

        GameObject spawnedUnit = Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);
        spawnedUnit.GetComponent<HealthComponent>().OnDeath += RemoveEnemyFromList;
        spawnedEnemies.Add(spawnedUnit);
    }

    public void RemoveEnemyFromList(GameObject enemy)
    {
        spawnedEnemies.Remove(enemy);
        OnEnemyCountChanged?.Invoke(spawnedEnemies.Count);

        if (spawnedEnemies.Count <= 0)
        {
            OnEnemiesDepleted?.Invoke();
        }
    }
}
