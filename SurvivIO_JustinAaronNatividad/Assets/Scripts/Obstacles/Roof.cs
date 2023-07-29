using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roof : MonoBehaviour
{
    [SerializeField] private float fadeTime;
    private List<GameObject> indoorUnits  = new List<GameObject>();
    private bool visible = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<MovementComponent>())
        {
            indoorUnits.Add(collision.gameObject);
        }
        CheckVisible();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<MovementComponent>())
        {
            indoorUnits.Remove(collision.gameObject);
        }
        CheckVisible();
    }

    private void CheckVisible()
    {
        if (visible && indoorUnits.Count > 0)
        {
            LeanTween.color(gameObject, Color.clear, fadeTime);
            visible = false;
        }
        else if (!visible && indoorUnits.Count <= 0)
        {
            LeanTween.color(gameObject, Color.white, fadeTime);
            visible = true;
        }
    }
}
