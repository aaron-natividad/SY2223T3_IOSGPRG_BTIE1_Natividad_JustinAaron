using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [Header("Base Spawner Parameters")]
    [SerializeField] protected Vector2 minBounds;
    [SerializeField] protected Vector2 maxBounds;
    [SerializeField] protected int spawnAmount;
    [Space(10)]
    [SerializeField] protected float spawnCheckRadius;
    [SerializeField] protected LayerMask spawnCheckMask;

    private void Start()
    {
        for (int i = 0; i < spawnAmount; i++)
        {
            SpawnOnValidPoint();
        }
        OnEnter();
    }

    protected void SpawnOnValidPoint()
    {
        bool spawnAvailable = false;

        while (!spawnAvailable)
        {
            float spawnX = Random.Range(minBounds.x, maxBounds.x);
            float spawnY = Random.Range(minBounds.y, maxBounds.y);

            Collider2D[] collisionsOnSpawn = Physics2D.OverlapCircleAll(new Vector2(spawnX, spawnY), spawnCheckRadius, spawnCheckMask);
            if (collisionsOnSpawn.Length <= 0)
            {
                SpawnPrefab(new Vector3(spawnX, spawnY, 0));
                spawnAvailable = true;
            }
        }
    }

    protected virtual void OnEnter()
    {

    }

    protected virtual void SpawnPrefab(Vector3 spawnPosition)
    {

    }
}
