using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public bool isGrounded;
    public List<GameObject> groundTiles = new List<GameObject>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Ground")) groundTiles.Add(collision.gameObject);
        CheckGrounded();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground")) groundTiles.Remove(collision.gameObject);
        CheckGrounded();
    }

    public void CheckGrounded()
    {
        if (groundTiles.Count > 0) isGrounded = true;
        else isGrounded = false;
    }
}
