using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailSpawner : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public GameObject trailPrefab;
    public float spawnInterval;
    public Quaternion spawnRotation;

    public IEnumerator CO_SpawnTrail(float spawnTime)
    {
        float timer = spawnTime;
        while (timer > 0)
        {
            Trail currentTrail = Instantiate(trailPrefab, transform.position, spawnRotation).GetComponent<Trail>();
            currentTrail.Initialize(spriteRenderer.sprite);
            timer -= spawnInterval;
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
