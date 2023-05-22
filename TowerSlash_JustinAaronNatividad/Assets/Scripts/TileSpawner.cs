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
}
