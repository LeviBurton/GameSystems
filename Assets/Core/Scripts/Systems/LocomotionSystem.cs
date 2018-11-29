using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// TODO: for now, anything that can move in the world will do it with a 
// CharacterController, since it takes care of a lot of issues 
// with moving in the world for us (think sliding along walls, etc.)
[RequireComponent(typeof(CharacterController))]
public class LocomotionSystem : MonoBehaviour
{
    public float maxForwardSpeed;
    public float maxSideSpeed;
    public bool alignToCamera;

    [Header("Events")]
    public UnityEvent_Vector3 onVelocityUpdated;

    CharacterController unityCharacterController = null;
    Vector3 velocity;
    float forwardSpeed;
    float sideSpeed;

    public void OnEnable()
    {
        unityCharacterController = GetComponent<CharacterController>();
        SetSprinting();
    }

    void Update()
    {
        RotateTowardsCurrentVelocity();

        velocity = unityCharacterController.velocity;

        // broadcast our velocity to anyone interested.
        // currently the HumanoidAnimator component listens for this.
        onVelocityUpdated.Invoke(velocity);
    }

    public void Move(Vector3 movementDirection)
    {
        if (unityCharacterController == null)
            return;

        if (alignToCamera)
        {
            var move = new Vector3();
            var camForward = Camera.main.transform.TransformDirection(Vector3.forward);
            camForward.y = 0;
            camForward.Normalize();
            Vector3 right = new Vector3(camForward.z, 0, -camForward.x);
            move = movementDirection.z * camForward + movementDirection.x * right;
            move = move * forwardSpeed * Time.deltaTime;

            unityCharacterController.Move(move);
        }
        else
        {
            movementDirection = movementDirection.normalized;
            movementDirection = movementDirection * forwardSpeed * Time.deltaTime;

            unityCharacterController.Move(movementDirection);
        }
    }

    void RotateTowardsCurrentVelocity()
    {
        var lookRotation = new Vector3(velocity.x, 0, velocity.z);
        if (lookRotation.magnitude > 0.4f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookRotation), Time.deltaTime * 720);
        }
    }

    // TODO: consider moving this somewhere else or renaming them or just making them a single function that takes a percent.
    public bool SetWalking()
    {
        forwardSpeed = maxForwardSpeed * 0.2f;
        sideSpeed = maxSideSpeed * 0.2f;
        return true;
    }

    public bool SetRunning()
    {
        forwardSpeed = maxForwardSpeed * 0.8f;
        sideSpeed = maxSideSpeed * 0.8f;
        return true;
    }

    public bool SetSprinting()
    {
        forwardSpeed = maxForwardSpeed;
        sideSpeed = maxSideSpeed;
        return true;
    }
}
