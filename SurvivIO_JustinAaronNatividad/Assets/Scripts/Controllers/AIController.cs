using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    [Header("Roaming Parameters")]
    public Vector2 roamCenter;
    public float roamRadius;
    public float roamInterval;

    private MovementComponent movementComponent;
    private AimComponent aimComponent;

    private void Start()
    {
        movementComponent = GetComponent<MovementComponent>();
        aimComponent = GetComponent<AimComponent>();
        StartCoroutine(MoveRandom());
    }

    // Temporary coroutine to test roaming with nav mesh
    private IEnumerator MoveRandom()
    {
        Vector2 movePos = Vector2.zero;
        yield return new WaitForSeconds(roamInterval);

        while (true)
        {
            movePos.x = Random.Range(-roamRadius, roamRadius);
            movePos.y = Random.Range(-roamRadius, roamRadius);
            movementComponent.MoveAgent(roamCenter + movePos);
            yield return new WaitForSeconds(roamInterval);
        }
    }
}
