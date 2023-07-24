using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState
{
    protected AIController aiController;

    public EnemyState(AIController controller)
    {
        aiController = controller;
    }

    // State methods virtual instead of abstract since implementation for each method not required
    public virtual void OnEnter()
    {

    }

    public virtual void OnFixedUpdate()
    {

    }

    public virtual void OnExit()
    {

    }
}
