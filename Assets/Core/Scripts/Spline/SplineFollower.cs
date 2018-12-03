using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class SplineFollower : MonoBehaviour
{
    public SplineComponent spline;
    public GameObject lookAtTarget;

    public bool forward;

    [Range(0, 1)]
    public float percent;

    public float followSpeed = 10.0f;
    public float direction = 1;

    void Start()
    {
        percent = 0.0f;
    }

    void Update()
    {
        if (percent >= 1.0f)
        {
            direction = -1;
        }
        else if (percent <= 0.0f)
        {
            direction = 1;
        }

        percent += Time.deltaTime * followSpeed * direction;

        var position = spline.GetPoint(percent);

        transform.position = position;
        transform.LookAt(lookAtTarget.transform);
    }
}
