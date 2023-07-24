using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wander : EnemyState
{
    private float roamRadius;
    private float roamInterval;
    private Vector2 roamCenter;

    private float wanderTimer;

    public Wander(AIController controller, float roamRadius, float roamInterval) : base(controller)
    {
        this.roamRadius = roamRadius;
        this.roamInterval = roamInterval;
    }

    public override void OnEnter()
    {
        roamCenter = new Vector2(aiController.transform.position.x, aiController.transform.position.y);
        wanderTimer = roamInterval;
    }

    public override void OnFixedUpdate()
    {
        if (aiController.seekRange.HasUnitsInRange())
        {
            aiController.SetEnemyState(aiController.seekState);
            return;
        }

        if (wanderTimer > 0)
        {
            wanderTimer -= Time.deltaTime;
        }
        else
        {
            MoveToRandomPosition();
            wanderTimer = roamInterval;
        }
    }

    private void MoveToRandomPosition()
    {
        Vector2 movePos = Vector2.zero;
        movePos.x = Random.Range(-roamRadius, roamRadius);
        movePos.y = Random.Range(-roamRadius, roamRadius);
        aiController.movement.MoveAgent(roamCenter + movePos);
    }
}
