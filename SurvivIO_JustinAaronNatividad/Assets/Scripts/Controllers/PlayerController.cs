using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Joysticks")]
    [SerializeField] private Joystick movementJoystick;
    [SerializeField] private Joystick aimJoystick;

    private MovementComponent movement;
    private AimComponent aiming;

    private void Start()
    {
        movement = GetComponent<MovementComponent>();
        aiming = GetComponent<AimComponent>();
    }

    private void FixedUpdate()
    {
        if(movementJoystick.Direction != Vector2.zero)
        {
            movement.MoveUnit(movementJoystick.Direction);
        }

        if (aimJoystick.Direction != Vector2.zero)
        {
            aiming.AimUnit(aimJoystick.Direction.normalized);
        }
    }
}
