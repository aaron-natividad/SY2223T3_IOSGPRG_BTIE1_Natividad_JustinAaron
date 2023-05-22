using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SwipeDirection { Up, Down, Left, Right }

public class InputManager : MonoBehaviour
{
    public static InputManager instance;

    public delegate void SwipeDirectionDelegate(SwipeDirection direction);
    public static SwipeDirectionDelegate OnSwipe;

    public delegate void TapDelegate();
    public static TapDelegate OnTap;

    [SerializeField] private float minimumSwipeMagnitude;
    [SerializeField] private float maximumTouchTime;
    [SerializeField] private float tapTime;

    private Touch touch;
    private float touchTime;
    private Vector2 startPos;
    private Vector2 endPos;
    public Vector2 swipeDirection;

    private void Awake()
    {
        if (instance != null) Destroy(this);
        else instance = this;
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began) DragStart();
            else if (touch.phase == TouchPhase.Ended) DragRelease();
            else Drag();
        }
    }

    void DragStart()
    {
        Vector2 dragPos = Camera.main.ScreenToWorldPoint(touch.position);
        startPos = dragPos;
    }

    void Drag()
    {
        Vector2 dragPos = Camera.main.ScreenToWorldPoint(touch.position);
        endPos = dragPos;
        touchTime += Time.deltaTime;
    }

    void DragRelease()
    {
        float temporaryTouchTime = touchTime;
        touchTime = 0;
        swipeDirection = endPos - startPos;

        // tap
        if (swipeDirection.magnitude < minimumSwipeMagnitude)
        {
            if (temporaryTouchTime < maximumTouchTime && OnTap != null)
            {
                Debug.Log("Tap");
                OnTap();
            }
            return; 
        }

        // if greater horizontal
        if(Mathf.Abs(swipeDirection.x) >= Mathf.Abs(swipeDirection.y))
        {
            if (swipeDirection.x > 0)
            {
                Debug.Log("Right");
                OnSwipe(SwipeDirection.Right);
            }
            else if (swipeDirection.x < 0)
            {
                Debug.Log("Left");
                OnSwipe(SwipeDirection.Left);
            }
        }
        // if greater vertical
        else
        {
            if (swipeDirection.y > 0)
            {
                Debug.Log("Up");
                OnSwipe(SwipeDirection.Up);
            }
            else if (swipeDirection.y < 0)
            {
                Debug.Log("Down");
                OnSwipe(SwipeDirection.Down);
            }
        }
    }
}
