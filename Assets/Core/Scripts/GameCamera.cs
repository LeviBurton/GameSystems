using Rewired;
using Sirenix.OdinInspector;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteInEditMode]
public class GameCamera : MonoBehaviour
{
    public BezierCurve curve;

    public Transform targetLookAtOffset;
    public Camera mainCamera;
    public Transform target;

    public float followingSpeed = 0.3f;
    public float followingMaxSpeed = 30.0f;
    public float rotationSpeed = 10.0f;
    public float zoomSpeed = 10.0f;

    public float minZoom = 2.0f;
    public float maxZoom = 50.0f;

    public float zoomControlSensitivity = 0.25f;

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

    float totalCameraCurveLength;

    private void Start()
    {
        rewiredPlayer = ReInput.players.GetPlayer(0);
        LookAtTarget();
        currentZoom = 0;
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
        transform.position = Vector3.SmoothDamp(transform.position, target.position, ref positionDampVelocity, followingSpeed, followingMaxSpeed, Time.unscaledDeltaTime);
    }

    void RotateAroundTarget()
    {
        if (Mathf.Abs(inputRotate) > 0.1f)
        {
            transform.Rotate(inputRotate * Vector3.up);
        }
    }

    void LookAtTarget()
    {
        mainCamera.transform.LookAt(targetLookAtOffset.transform.position);
    }

    private void ZoomCamera()
    {  
        totalCameraCurveLength = curve.GetLengthSimpsons(0f, 1f);

        currentZoom = Mathf.Clamp(currentZoom, 0, totalCameraCurveLength);

        float t = curve.FindTValue(currentZoom, totalCameraCurveLength);
       
        var position = curve.DeCasteljausAlgorithm(t);
     
        mainCamera.transform.position = transform.TransformPoint(position);
    }

    public void SetCameraZoom(float zoom)
    {
        inputZoom = zoom;
        currentZoom += zoom * zoomSpeed * Time.unscaledDeltaTime;
    }

    public void SetCameraRotation(float rotate)
    {
        inputRotate = rotate * rotationSpeed * Time.unscaledDeltaTime;
    }
}
