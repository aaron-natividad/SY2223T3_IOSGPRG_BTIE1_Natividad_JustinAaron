using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public enum ArrowType { Normal, Opposite, Spinning }

public class Arrow : MonoBehaviour
{
    [SerializeField] private GameObject arrowParent;
    [SerializeField] private SpriteRenderer arrowSprite;
    [SerializeField] private SpriteRenderer boxSprite;
    [Space(10)]
    [SerializeField] private ArrowType type;
    [SerializeField] private float spinInterval;
    [Space(10)]
    [SerializeField] private Color inactiveColor;
    [SerializeField] private Color activeColor;

    [HideInInspector] public bool isActive = false;
    [HideInInspector] public int arrowValue;
    private float internalTimer;

    public void SetValue(int initialArrowValue)
    {
        arrowValue = initialArrowValue;
        DirectArrow(arrowValue);
        StartCoroutine(CO_InactiveBehavior());
    }

    IEnumerator CO_InactiveBehavior()
    {
        isActive = false;
        boxSprite.enabled = false;
        arrowSprite.color = inactiveColor;

        while (!isActive)
        {
            if (type == ArrowType.Spinning && CheckTimer())
            {
                arrowValue = (arrowValue + 1) % 4;
                arrowSprite.transform.Rotate(new Vector3(0, 0, 90));
            }
            yield return new WaitForFixedUpdate();
        }

        StartCoroutine(CO_ActiveBehavior());
    }

    IEnumerator CO_ActiveBehavior()
    {
        isActive = true;
        boxSprite.enabled = true;
        arrowSprite.color = activeColor;

        float popScale = 1.2f;

        while (popScale > 1)
        {
            arrowParent.transform.localScale = Vector3.one * popScale;
            popScale -= Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }

        arrowParent.transform.localScale = Vector3.one;
    }

    private bool CheckTimer()
    {
        if (internalTimer > 0)
        {
            internalTimer -= Time.deltaTime;
            return false;
        }
        else
        {
            internalTimer = spinInterval;
            return true;
        }
    }

    public void DirectArrow(int faceValue)
    {
        arrowSprite.transform.Rotate(new Vector3(0, 0, 90 * faceValue));
        if (type == ArrowType.Opposite)
        {
            arrowSprite.transform.Rotate(new Vector3(0, 0, 180));
        }
    }

    public int SwipeDirectionToInt(SwipeDirection direction)
    {
        switch (direction)
        {
            case SwipeDirection.Up: return 0;
            case SwipeDirection.Left: return 1;
            case SwipeDirection.Down: return 2;
            case SwipeDirection.Right: return 3;
            default: return 0;
        }
    }
}
