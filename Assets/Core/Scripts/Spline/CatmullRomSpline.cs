using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatmullRomSpline : MonoBehaviour
{
    public Transform[] controlPoints;
    public bool isLooping = true;

    // the spline resolution.
    // make sure it adds up to 1
    public float resolution = 0.1f;  

    void OnDrawGizmos()
    {
        Gizmos.color = Color.white;

        for (int i = 0; i < controlPoints.Length; i++)
        {
            var skipPoint = (i == 0 || i == controlPoints.Length - 2 || i == controlPoints.Length - 1) && !isLooping;

            if (skipPoint)
            {
                continue;
            }

            DisplayCatmullRomSpline(i);
        }
    }

    // Display a spline between 2 points derivefd with the Catmull-Rom spline algo
    void DisplayCatmullRomSpline(int pos)
    {
        // 4 control points we need to form a spline between p1 and p2
        var p0 = controlPoints[ClampListPos(pos - 1)].position;
        var p1 = controlPoints[pos].position;
        var p2 = controlPoints[ClampListPos(pos + 1)].position;
        var p3 = controlPoints[ClampListPos(pos + 2)].position;

        // the start position of the line
        var lastPos = p1;

        // how many times should we loop?
        int loops = Mathf.FloorToInt(1f / resolution);

        for (int i = 1; i <= loops; i++)
        {
            float t = i * resolution;

            var newPos = GetCatmullRomPosition(t, p0, p1, p2, p3);

            Gizmos.DrawLine(lastPos, newPos);

            lastPos = newPos;
        }
    }

    //Clamp the list positions to allow looping
    int ClampListPos(int pos)
    {
        if (pos < 0)
        {
            pos = controlPoints.Length - 1;
        }

        if (pos > controlPoints.Length)
        {
            pos = 1;
        }
        else if (pos > controlPoints.Length - 1)
        {
            pos = 0;
        }

        return pos;
    }
    
    // Returns a position between 4 Vector3 with Catmull-Rom spline algorithm
    // http://www.iquilezles.org/www/articles/minispline/minispline.htm
    Vector3 GetCatmullRomPosition(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
    {
        //The coefficients of the cubic polynomial (except the 0.5f * which I added later for performance)
        Vector3 a = 2f * p1;
        Vector3 b = p2 - p0;
        Vector3 c = 2f * p0 - 5f * p1 + 4f * p2 - p3;
        Vector3 d = -p0 + 3f * p1 - 3f * p2 + p3;

        //The cubic polynomial: a + b * t + c * t^2 + d * t^3
        Vector3 pos = 0.5f * (a + (b * t) + (c * t * t) + (d * t * t * t));

        return pos;
    }
}
