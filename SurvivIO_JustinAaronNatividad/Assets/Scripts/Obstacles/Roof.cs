using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roof : MonoBehaviour
{
    [SerializeField] private float fadeTime;

    private List<GameObject> indoorUnits  = new List<GameObject>();
    private bool isVisible = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<MovementComponent>())
        {
            indoorUnits.Add(collision.gameObject);
        }
        CheckVisibility();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<MovementComponent>())
        {
            indoorUnits.Remove(collision.gameObject);
        }
        CheckVisibility();
    }

    private void CheckVisibility()
    {
        if (isVisible && indoorUnits.Count > 0)
        {
            LeanTween.color(gameObject, Color.clear, fadeTime);
            isVisible = false;
        }
        else if (!isVisible && indoorUnits.Count <= 0)
        {
            LeanTween.color(gameObject, Color.white, fadeTime);
            isVisible = true;
        }
    }
}
