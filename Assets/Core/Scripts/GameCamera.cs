using Rewired;
using Sirenix.OdinInspector;
using UnityEngine;
using Cinemachine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteInEditMode]
public class GameCamera : MonoBehaviour
{
    public BezierCurve curve;

    public Transform targetLookAtOffset;
    public CinemachineVirtualCamera virtualCamera;

    public Transform target;

    public float followingSpeed = 0.3f;
    public float followingMaxSpeed = 30.0f;
    public float rotationSpeed = 10.0f;
    public float zoomSpeed = 10.0f;

    Vector3 positionDampVelocity;
    Vector3 zoomDampVelocity;

    float inputZoom;
    public float inputZoomSensitivity = 0.1f;
    public float inputRotateSensitivty = 0.1f;

    [OnValueChanged("CurrentZoomChanged")]
    [Range(0,1)]
    public float currentZoom;

#if UNITY_EDITOR
    void CurrentZoomChanged()
    {
        EditorUtility.SetDirty(this);
    }
#endif

    float inputRotate;
    float distanceToTarget;
    float zoomPercent;

    Vector3 directionToTarget;
    Transform cameraTarget;
    Vector3 startPosition;
    Quaternion startRotation;
    public SplineComponent spline;

    Player rewiredPlayer = null;

    float totalCameraCurveLength;

    private void Start()
    {
        rewiredPlayer = ReInput.players.GetPlayer(0);
    //    LookAtTarget();
    }


    void LateUpdate()
    {
        if (target != null)
        {
            ZoomCamera();
            FollowTarget();
            RotateAroundTarget();
            LookAtTarget();
        }
    }

    void FollowTarget()
    {
        // TODO: this causes a little bit of aim lag on the camera, need to figure out another way to control movement.
        transform.position = Vector3.SmoothDamp(transform.position, target.position, ref positionDampVelocity, followingSpeed, followingMaxSpeed, Time.unscaledDeltaTime);
        
        // TODO: if we directly set the position without damping, we remove the camera aim lag. 
        //transform.position = target.position;
    }

    void RotateAroundTarget()
    {
        if (Mathf.Abs(inputRotate) < inputRotateSensitivty)
            return;

        float rotation = inputRotate * rotationSpeed * Time.unscaledDeltaTime;
        transform.Rotate(rotation * Vector3.up);
    }

    void LookAtTarget()
    {
      //  virtualCamera.transform.LookAt(targetLookAtOffset.transform.position);
    }

    private void ZoomCamera()
    {
        var dolly = virtualCamera.GetCinemachineComponent<CinemachineTrackedDolly>();
        dolly.m_PathPosition = currentZoom;
    }

    public void SetCameraZoom(float zoom)
    {
        if (Mathf.Abs(zoom) < inputZoomSensitivity)
            return;

        inputZoom = zoom;
        currentZoom += inputZoom * Time.unscaledDeltaTime * zoomSpeed;
        currentZoom = Mathf.Clamp01(currentZoom);
    }

    public void SetCameraRotation(float rotate)
    {
        inputRotate = rotate;
    }
}
