using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MovementComponent : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    private NavMeshAgent agent;

    [SerializeField] private float moveSpeed;

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

    public void MoveAgent(Vector2 movePosition)
    {
        agent.SetDestination(new Vector3(movePosition.x, movePosition.y, transform.position.z));
    }

    public void MoveUnit(Vector2 moveDirection)
    {
        rigidBody.velocity = moveDirection * moveSpeed;
    }
}
