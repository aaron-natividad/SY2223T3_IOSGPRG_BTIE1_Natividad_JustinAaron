using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyPrefabs;
    [Header("Parameters")]
    [SerializeField] private float groundedXPos;
    [SerializeField] private float flyingXPos;
    [SerializeField] private float minSpawnDistance;
    [SerializeField] private float maxSpawnDistance;
    [Space(10)]
    [SerializeField] private List<GameObject> enemyList = new List<GameObject>();

    public void Initialize()
    {
        for(int i = 0; i < 5; i++)
        {
            SpawnNewEnemyRaw();
        }
    }

    public void ClearEnemies()
    {
        enemyList.Clear();
    }

    public void ReplaceEnemy(GameObject destroyedEnemy, bool isDestroyedByPlayer)
    {
        enemyList.Remove(destroyedEnemy);
        if (GameManager.instance.player.health.isAlive)
        {
            SpawnNewEnemyRaw();
        }
    }

    public void SpawnNewEnemyRaw()
    {
        int spawnTypeIndex = Random.Range(0, enemyPrefabs.Length);
        float spawnDistance = Random.Range(minSpawnDistance, maxSpawnDistance);
        float spawnY = enemyList.Count > 0 ? enemyList[enemyList.Count - 1].transform.position.y + spawnDistance : transform.position.y; 

        Enemy spawnedEnemy = Instantiate(enemyPrefabs[spawnTypeIndex], transform.position, Quaternion.identity).GetComponent<Enemy>();
        switch (spawnedEnemy.type)
        {
            case EnemyType.Grounded:
                spawnedEnemy.transform.position = new Vector3(groundedXPos, spawnY, 0);
                break;
            case EnemyType.Flying:
                spawnedEnemy.transform.position = new Vector3(flyingXPos, spawnY, 0);
                break;
        }
        spawnedEnemy.OnEnemyDestroy += ReplaceEnemy;
        enemyList.Add(spawnedEnemy.gameObject);
    }
}
