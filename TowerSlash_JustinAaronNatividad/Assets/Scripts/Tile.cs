using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public delegate void TilePassDelegate();
    public static TilePassDelegate OnTilePass;

    public Transform spawnNode;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && OnTilePass != null) OnTilePass();
    }
}
