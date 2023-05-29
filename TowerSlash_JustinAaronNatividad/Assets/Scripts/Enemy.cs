using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType { Grounded, Flying }

public class Enemy : MonoBehaviour
{
    public delegate void EnemyDestroyDelegate();
    public EnemyDestroyDelegate OnEnemyDestroy;

    public EnemyType type;
    public Arrow arrow;

    private void OnEnable()
    {
        OnEnemyDestroy += DestroyEnemy;
    }

    private void OnDisable()
    {
        OnEnemyDestroy -= DestroyEnemy;
    }

    private void Start()
    {
        arrow.SetValue((int)Random.Range(0, 4));
    }

    public void ActivateEnemy()
    {
        arrow.isActive = true;
    }

    public void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    public bool CompareValue(SwipeDirection direction)
    {
        bool isCorrect = arrow.arrowValue == arrow.SwipeDirectionToInt(direction);
        if (isCorrect && OnEnemyDestroy != null) OnEnemyDestroy();
        return isCorrect;
    }
}
