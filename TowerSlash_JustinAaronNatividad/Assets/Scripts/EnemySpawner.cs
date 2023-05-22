using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private List<Transform> spawnNodes;
    [SerializeField] private Transform player;

    [Header("Parameters")]
    [SerializeField] private float minSpawnTime;
    [SerializeField] private float maxSpawnTime;

    public bool isSpawning;
    private Vector3 spawnPosition;
    

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CO_SpawnEnemies());
    }

    IEnumerator CO_SpawnEnemies()
    {
        int spawnIndex;
        float spawnTime;
        isSpawning = true;
        yield return null;

        while (isSpawning)
        {
            spawnIndex = (int)(Random.Range(0,spawnNodes.Count));
            spawnTime = Random.Range(minSpawnTime, maxSpawnTime);
            spawnPosition = new Vector3(spawnNodes[spawnIndex].position.x, spawnNodes[spawnIndex].position.y, 0);
            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            yield return new WaitForSeconds(spawnTime);
        }
    }
}
