using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    [Header("Roaming Parameters")]
    [SerializeField] private float roamRadius;
    [SerializeField] private float roamInterval;

    [Header("Aim Parameters")]
    [SerializeField] private GameObject gunPrefab;
    [SerializeField] private float semiAutoFireRate;

    // Components
    private MovementComponent movement;
    private AimComponent aiming;
    private InventoryComponent inventory;
    private UnitDetection unitDetection;

    // Private Variables
    private EnemyState state;
    private Vector2 roamCenter;

    private void Start()
    {
        movement = GetComponent<MovementComponent>();
        aiming = GetComponent<AimComponent>();
        inventory = GetComponent<InventoryComponent>();
        unitDetection = GetComponentInChildren<UnitDetection>();

        inventory.AddGun(gunPrefab);
        roamCenter = new Vector2(transform.position.x, transform.position.y);
        state = EnemyState.Patrol;

        StartCoroutine(CO_Patrol());
    }

    public void SetEnemyState(EnemyState newState)
    {
        state = newState;

        if (state == EnemyState.Patrol)
        {
            StartCoroutine(CO_Patrol());
        }
        else if (state == EnemyState.Shooting)
        {
            StartCoroutine(CO_ShootFirstUnit());
        }
    }

    private IEnumerator CO_Patrol()
    {
        Vector2 movePos = Vector2.zero;
        yield return new WaitForSeconds(roamInterval);

        while (state == EnemyState.Patrol)
        {
            movePos.x = Random.Range(-roamRadius, roamRadius);
            movePos.y = Random.Range(-roamRadius, roamRadius);
            movement.MoveAgent(roamCenter + movePos);
            yield return new WaitForSeconds(roamInterval);
        }
    }

    private IEnumerator CO_ShootFirstUnit()
    {
        float semiAutoTimer = semiAutoFireRate;

        while (state == EnemyState.Shooting)
        {
            GameObject targetedUnit = unitDetection.units[0];
            aiming.AimUnit(targetedUnit.transform.position - transform.position);

            if (inventory.GetEquippedGun().isAutomatic)
            {
                // Held firing
                aiming.SetIsFiring(true);
            }
            else
            {
                // Interruptable semi auto timer to emulate player firing
                if (semiAutoTimer > 0)
                {
                    semiAutoTimer -= Time.deltaTime;
                    aiming.SetIsFiring(false);
                }
                else
                {
                    semiAutoTimer = semiAutoFireRate;
                    aiming.SetIsFiring(true);
                }
            }
            yield return new WaitForFixedUpdate();
        }

        aiming.SetIsFiring(false);
    }
}
