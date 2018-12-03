using Rewired;
using Sirenix.OdinInspector;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteInEditMode]
public class GameCamera : MonoBehaviour
{
    public Transform targetLookAtOffset;
    public Camera mainCamera;
    public Transform target;
    public Transform maxZoomCameraTransform;

    public float followingSpeed = 0.3f;
    public float followingMaxSpeed = 30.0f;
    public float rotationSpeed = 10.0f;
    public float zoomSpeed = 10.0f;

    public float minZoom = 2.0f;
    public float maxZoom = 50.0f;

    public float zoomControlSensitivity = 0.25f;

    public AnimationCurve curve;

    Vector3 positionDampVelocity;
    Vector3 zoomDampVelocity;

    float inputZoom;

    [OnValueChanged("CurrentZoomChanged")]
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

    void OnEnable()
    {
        rewiredPlayer = ReInput.players.GetPlayer(0);

        startPosition = mainCamera.transform.position;
        startRotation = mainCamera.transform.rotation;

        LookAtTarget();
        currentZoom = maxZoom;
    }

    void LateUpdate()
    {
        distanceToTarget = Vector3.Distance(mainCamera.transform.position, targetLookAtOffset.position);
        directionToTarget = targetLookAtOffset.position - mainCamera.transform.position;
        zoomPercent = distanceToTarget.Map(minZoom, maxZoom, 0, 1);
   
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
        mainCamera.transform.LookAt(targetLookAtOffset.position);
    }

    private void ZoomCamera()
    {
        currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);

        var percent = currentZoom.Map(minZoom, maxZoom, 0, 1);

        var position = transform.TransformPoint(spline.GetPoint(percent));
        mainCamera.transform.position = position;
    }

    public void SetCameraZoom(float zoom)
    {

        inputZoom = zoom;
        currentZoom -= zoom;

    }

    public void SetCameraRotation(float rotate)
    {
        inputRotate = rotate;
    }
}
