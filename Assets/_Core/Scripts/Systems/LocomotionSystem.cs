using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: for now, anything that can move in the world will do it with a 
// CharacterController, since it takes care of a lot of issues 
// with moving in the world for us (think sliding along walls, etc.)
[RequireComponent(typeof(CharacterController))]
public class LocomotionSystem : MonoBehaviour
{
    public Vector3 velocity;
    public float maxForwardSpeed;
    public float maxSideSpeed;
    public float tmp1;
    public float tmp2;

    private CharacterController controller = null;

    public void OnEnable()
    {
        controller = GetComponent<CharacterController>();
    }

    public void SetVelocity(Vector3 newVelocity)
    {
        this.velocity = newVelocity;
    }

    public void MoveInDirection(Vector3 movementDirection)
    {
        movementDirection = transform.TransformDirection(movementDirection);
        movementDirection = movementDirection * maxForwardSpeed * Time.deltaTime;

        controller.Move(movementDirection);
    }
}
