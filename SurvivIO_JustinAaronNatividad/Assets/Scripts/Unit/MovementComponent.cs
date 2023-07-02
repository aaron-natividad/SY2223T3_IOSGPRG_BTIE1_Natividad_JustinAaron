using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MovementComponent : MonoBehaviour
{
    [SerializeField] private float moveSpeed;

    // NavMeshAgent still included within movement component because Unit prefab is generic
    private Rigidbody2D rigidBody;
    private NavMeshAgent agent;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        agent = GetComponent<NavMeshAgent>();
        if (agent)
        {
            agent.updateRotation = false;
            agent.updateUpAxis = false;
            agent.speed = moveSpeed;
        }
    }

    public void MoveUnit(Vector2 moveDirection)
    {
        rigidBody.velocity = moveDirection * moveSpeed;
    }

    public void MoveAgent(Vector2 movePosition)
    {
        agent.SetDestination(new Vector3(movePosition.x, movePosition.y, transform.position.z));
    }
}
