using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private float fadeTime;

    void Start()
    {
        LeanTween.color(gameObject, Color.clear, fadeTime);
        Destroy(gameObject, fadeTime);
    }
}
