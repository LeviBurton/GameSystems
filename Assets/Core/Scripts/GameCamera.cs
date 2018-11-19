﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCamera : MonoBehaviour
{
    public Transform targetLookAtOffset;
    public Camera mainCamera;
    public Transform target;
    public float followingSpeed = 0.3f;
    public float followingMaxSpeed = 30.0f;
    public float rotationSpeed = 10.0f;
    public float zoomSpeed = 10.0f;
    public float maxZoom = 50.0f;
    public float minZoom = 2.0f;
    public float zoomControlSensitivity = 0.25f;

    Vector3 positionDampVelocity;
    Vector3 zoomDampVelocity;

    float inputZoom;
    float inputRotate;

    void LateUpdate()
    {
        if (target != null)
        {
            FollowTarget();
            RotateAroundTarget();
            LookAtTarget();
            ZoomCamera();
        }
    }

    void FollowTarget()
    {
        if (Vector3.Distance(target.position, transform.position) > 0.01f)
        {
            transform.position = Vector3.SmoothDamp(transform.position, target.position, ref positionDampVelocity, followingSpeed, followingMaxSpeed, Time.unscaledDeltaTime);
        }
    }

    void RotateAroundTarget()
    {
        if (Mathf.Abs(inputRotate) > 0.1f)
        {
            transform.Rotate(inputRotate * Vector3.up * rotationSpeed * Time.unscaledDeltaTime);
        }
    }

    void LookAtTarget()
    {
        mainCamera.transform.LookAt(transform.position + transform.InverseTransformPoint(targetLookAtOffset.position));
    }

    private void ZoomCamera()
    {
        if (Mathf.Abs(inputZoom) < zoomControlSensitivity)
            return;

        var distanceToTargetOffset = Vector3.Distance(mainCamera.transform.position, targetLookAtOffset.position);

        // check to see if we are within the min/max extents, or at the extents.
        // when at minZoom allow zooming out, when at maxZoom allow zooming in, when between, allow zoom in/out.
        bool bCanZoom = distanceToTargetOffset <= minZoom && inputZoom < 0 ||
                        distanceToTargetOffset >= maxZoom && inputZoom > 0 ||
                        distanceToTargetOffset < maxZoom && distanceToTargetOffset > minZoom;

        if (bCanZoom)
        {
            mainCamera.transform.position += mainCamera.transform.forward * inputZoom * zoomSpeed * Time.unscaledDeltaTime;
        }
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
