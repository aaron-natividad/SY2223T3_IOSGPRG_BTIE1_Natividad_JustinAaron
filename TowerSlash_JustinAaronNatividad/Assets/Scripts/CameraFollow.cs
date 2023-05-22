using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private float followSpeed;
    [SerializeField] private float yOffset;

    private Vector3 targetPos;
    private Vector3 currentVelocity;

    private void FixedUpdate()
    {
        targetPos = new Vector3(0, target.transform.position.y + yOffset, transform.position.z);
        transform.position = Vector3.SmoothDamp(transform.position, targetPos,ref currentVelocity, followSpeed);
    }
}
