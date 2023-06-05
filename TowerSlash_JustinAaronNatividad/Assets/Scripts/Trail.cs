using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trail : MonoBehaviour
{
    public Color startColor;
    public Color endColor;
    [Space(10)]
    public float fadeTime;

    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Initialize(Sprite currentSprite)
    {
        spriteRenderer.sprite = currentSprite;
        StartCoroutine(CO_FadeTrail());
    }

    public IEnumerator CO_FadeTrail()
    {
        float timer = 0;
        while(timer < fadeTime)
        {
            timer += Time.deltaTime;
            spriteRenderer.color = Color.Lerp(startColor, endColor, timer/fadeTime);
            yield return new WaitForFixedUpdate();
        }
        Destroy(gameObject);
    }
}
