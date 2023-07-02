using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Joysticks")]
    public Joystick movementJoystick;
    public Joystick aimJoystick;

    private MovementComponent movementComponent;
    private AimComponent aimComponent;

    private void Start()
    {
        movementComponent = GetComponent<MovementComponent>();
        aimComponent = GetComponent<AimComponent>();
    }

    private void FixedUpdate()
    {
        if(movementJoystick.Direction != Vector2.zero)
        {
            movementComponent.MoveUnit(movementJoystick.Direction);
        }

        if (aimJoystick.Direction != Vector2.zero)
        {
            aimComponent.AimUnit(aimJoystick.Direction.normalized);
        }
    }
}
