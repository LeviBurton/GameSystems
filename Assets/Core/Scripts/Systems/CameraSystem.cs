using Rewired;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSystem : MonoBehaviour
{
    public float followingSpeed = 0.25f;
    public float followingMaxSpeed = 50.0f;
    public float rotationSpeed = 180.0f;
    public float cameraPitch = 40.0f;
    public float cameraYaw = 0;
    public float cameraDistance = 5.0f;

    public GameObject currentTarget = null;

    Player rewiredPlayer = null;

    Vector3 positionDampVelocity;
    float inputZoom;
    float inputRotate;

    void OnEnable()
    {
        rewiredPlayer = ReInput.players.GetPlayer(0);
    }

    void LateUpdate()
    {
        FollowTarget();
    }

    void FollowTarget()
    {
        if (Vector3.Distance(currentTarget.transform.position, transform.position) > 0.01f)
        {
            //transform.position = currentTarget.transform.position + transform.TransformDirection(-transform.forward) * 5.0f + Vector3.up * 2.0f;
            transform.position = Vector3.SmoothDamp(transform.position, currentTarget.transform.position, ref positionDampVelocity, followingSpeed, followingMaxSpeed, Time.unscaledDeltaTime);
        }

        var newRotation = Quaternion.LookRotation(transform.position - currentTarget.transform.position, Vector3.forward);
        
        transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * 8);
  
    }

    public void SetCameraZoom(float zoom)
    {
        inputZoom = zoom;
    }

    public void SetCameraRotation(float rotate)
    {
        inputRotate = rotate;
    }
}
