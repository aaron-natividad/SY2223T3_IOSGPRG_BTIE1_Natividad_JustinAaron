using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject target;
    public float dampValue;

    private Vector3 targetPos;
    private Vector3 camVelocity;

    private void FixedUpdate()
    {
        if (target == null)
        {
            return;
        }

        targetPos = target.transform.position;
        targetPos.z = transform.position.z;
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref camVelocity, dampValue);
    }
}
