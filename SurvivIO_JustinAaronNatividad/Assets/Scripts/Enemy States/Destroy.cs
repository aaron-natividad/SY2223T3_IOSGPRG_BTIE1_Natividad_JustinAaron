using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : EnemyState
{
    private GameObject target;
    private Gun equippedGun;
    private float semiAutoTimer;

    public Destroy(AIController controller) : base(controller)
    {

    }

    public override void OnEnter()
    {
        aiController.movement.StopAgent();
        equippedGun = aiController.inventory.GetEquippedGun();
    }

    public override void OnFixedUpdate()
    {
        if (!aiController.destroyRange.HasUnitsInRange())
        {
            aiController.SetEnemyState(aiController.seekState);
            return;
        }

        target = aiController.destroyRange.GetFirstUnit();
        aiController.aiming.AimUnit(target.transform.position - aiController.transform.position);

        if (equippedGun.isAutomatic)
        {
            aiController.aiming.SetIsFiring(true);
        }
        else
        {
            if (semiAutoTimer > 0)
            {
                semiAutoTimer -= Time.deltaTime;
                aiController.aiming.SetIsFiring(false);
            }
            else
            {
                semiAutoTimer = equippedGun.fireRate;
                aiController.aiming.SetIsFiring(true);
            }
        }
    }

    public override void OnExit()
    {
        aiController.aiming.SetIsFiring(false);
    }
}
