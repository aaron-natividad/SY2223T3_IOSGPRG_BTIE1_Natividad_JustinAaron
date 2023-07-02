using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimComponent : MonoBehaviour
{
    public void AimUnit(Vector2 aimDirection)
    {
        transform.up = aimDirection;
    }
}
