using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seek : EnemyState
{
    private GameObject target;

    public Seek(AIController controller) : base(controller)
    {
        
    }

    public override void OnEnter()
    {
        aiController.movement.StopAgent();
    }

    public override void OnFixedUpdate()
    {
        if (!aiController.seekRange.HasUnitsInRange())
        {
            aiController.SetEnemyState(aiController.wanderState);
            return;
        }

        if (aiController.destroyRange.HasUnitsInRange())
        {
            aiController.SetEnemyState(aiController.destroyState);
            return;
        }

        target = aiController.seekRange.GetFirstUnit();
        aiController.movement.MoveAgent(target.transform.position);
    }
}
