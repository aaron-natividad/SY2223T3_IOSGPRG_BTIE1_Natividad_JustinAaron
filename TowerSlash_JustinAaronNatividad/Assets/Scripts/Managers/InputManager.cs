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
    private Vector2 swipeDirection;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    DragStart();
                    break;
                case TouchPhase.Ended:
                    DragRelease();
                    break;
                default:
                    Drag();
                    break;
            }
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

        if (swipeDirection.magnitude < minimumSwipeMagnitude)
        {
            if (temporaryTouchTime < maximumTouchTime)
            {
                OnTap?.Invoke();
            }
            return; 
        }

        if(Mathf.Abs(swipeDirection.x) >= Mathf.Abs(swipeDirection.y))
        {
            OnSwipe?.Invoke(swipeDirection.x > 0 ? SwipeDirection.Right : SwipeDirection.Left);
        }
        else
        {
            OnSwipe?.Invoke(swipeDirection.y > 0 ? SwipeDirection.Up : SwipeDirection.Down);
        }
    }
}
