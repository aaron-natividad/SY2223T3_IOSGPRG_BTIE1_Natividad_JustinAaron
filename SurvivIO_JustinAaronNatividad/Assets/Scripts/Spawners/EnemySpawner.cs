using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : Spawner
{
    public static event Action<int> OnEnemyCountChanged;
    public static event Action OnEnemiesDepleted;

    [SerializeField] private GameObject enemyPrefab;
    private List<GameObject> spawnedEnemies = new List<GameObject>();
    //private int remainingEnemies;

    protected override void OnEnter()
    {
        OnEnemyCountChanged?.Invoke(spawnAmount);
    }

    protected override void SpawnPrefab(Vector3 spawnPosition)
    {
        GameObject spawnedUnit = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        spawnedUnit.GetComponent<HealthComponent>().OnDeath = RemoveEnemyFromCount;
        spawnedEnemies.Add(spawnedUnit);
    }

    public void RemoveEnemyFromCount(GameObject enemy)
    {
        //remainingEnemies--;
        spawnedEnemies.Remove(enemy);
        OnEnemyCountChanged?.Invoke(spawnedEnemies.Count);

        if (spawnedEnemies.Count <= 0)
        {
            OnEnemiesDepleted?.Invoke();
        }
    }
}
