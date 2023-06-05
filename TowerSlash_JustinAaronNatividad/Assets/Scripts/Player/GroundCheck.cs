using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [HideInInspector] public bool isGrounded;
    [HideInInspector] public List<GameObject> groundTiles = new List<GameObject>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            groundTiles.Add(collision.gameObject);
        }
        isGrounded = groundTiles.Count > 0;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            groundTiles.Remove(collision.gameObject);
        }
        isGrounded = groundTiles.Count > 0;
    }
}
