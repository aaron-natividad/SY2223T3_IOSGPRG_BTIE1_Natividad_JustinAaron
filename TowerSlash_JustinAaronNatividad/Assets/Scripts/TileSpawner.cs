using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSpawner : MonoBehaviour
{
    public List<Tile> towerTiles;
    private Tile oldestTile;

    private void OnEnable()
    {
        Tile.OnTilePass += MoveTile;
    }

    private void OnDisable()
    {
        Tile.OnTilePass -= MoveTile;
    }

    public void MoveTile()
    {
        oldestTile = towerTiles[0];
        towerTiles.RemoveAt(0);
        oldestTile.transform.position = towerTiles[towerTiles.Count - 1].spawnNode.position;
        towerTiles.Add(oldestTile);
    }

    public IEnumerator AnimateTiles(float moveX)
    {
        float timer = 0.5f;

        while(timer > 0)
        {
            foreach(Tile t in towerTiles)
            {
                t.transform.position = new Vector3(Mathf.Lerp(t.transform.position.x, moveX, 0.2f), t.transform.position.y, t.transform.position.z);
            }
            timer -= Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }

        foreach (Tile t in towerTiles)
        {
            t.transform.position = new Vector3(moveX, t.transform.position.y, t.transform.position.z);
        }
    }
}
