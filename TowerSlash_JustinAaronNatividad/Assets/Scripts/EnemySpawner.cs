using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyPrefabs;
    [Space(10)]
    [SerializeField] private Transform groundedSpawnNode;
    [SerializeField] private Transform flyingSpawnNode;

    [Header("Parameters")]
    [SerializeField] private float minSpawnTime;
    [SerializeField] private float maxSpawnTime;

    public bool isSpawning;
    
    void Start()
    {
        StartCoroutine(CO_SpawnEnemies());
    }

    IEnumerator CO_SpawnEnemies()
    {
        int spawnTypeIndex;
        float spawnTime;
        isSpawning = true;
        yield return null;

        while (isSpawning)
        {
            spawnTypeIndex = (int)(Random.Range(0, enemyPrefabs.Length));
            spawnTime = Random.Range(minSpawnTime, maxSpawnTime);

            Enemy spawnedEnemy = Instantiate(enemyPrefabs[spawnTypeIndex], transform.position, Quaternion.identity).GetComponent<Enemy>();
            if      (spawnedEnemy.type == EnemyType.Grounded) spawnedEnemy.transform.position = groundedSpawnNode.position;
            else if (spawnedEnemy.type == EnemyType.Flying)   spawnedEnemy.transform.position = flyingSpawnNode.position;

            yield return new WaitForSeconds(spawnTime);
        }
    }
}
