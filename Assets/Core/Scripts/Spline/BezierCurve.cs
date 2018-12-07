using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class BezierCurve : MonoBehaviour
{
    public Transform startPoint;
    public Transform endPoint;
    public Transform controlPointStart;
    public Transform controLPointEnd;
    public float resolution = 0.1f;
    Vector3 A, B, C, D;

    //An array with colors to display the line segments that make up the final curve
    Color[] colorsArray = { Color.white, Color.red, Color.blue, Color.magenta, Color.black };

    void OnEnable()
    {
        A = startPoint.position;
        B = controlPointStart.position;
        C = controLPointEnd.position;
        D = endPoint.position;
    }
   
    void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Matrix4x4 oldGizmosMatrix = Gizmos.matrix;

        Gizmos.matrix = Matrix4x4.TRS(transform.parent.position, transform.parent.rotation, transform.parent.localScale);
       
        // start position of line
        var lastpos = A;

        int loops = Mathf.FloorToInt(1f / resolution);

        for (int i = 1; i <= loops; i++)
        {
            float t = i * resolution;

            var newPos = DeCasteljausAlgorithm(t);

            Gizmos.DrawLine(lastpos, newPos);

            lastpos = newPos;
        }

        Gizmos.color = Color.green;
       
        Gizmos.DrawLine(A, B);
        Gizmos.DrawLine(C, D);
        Gizmos.matrix = oldGizmosMatrix;
    }

    //Use Newton–Raphsons method to find the t value at the end of this distance d
    public float FindTValue(float d, float totalLength)
    {
        //Need a start value to make the method start
        //Should obviously be between 0 and 1
        //We can say that a good starting point is the percentage of distance traveled
        //If this start value is not working you can use the Bisection Method to find a start value
        //https://en.wikipedia.org/wiki/Bisection_method
        float t = d / totalLength;

        //Need an error so we know when to stop the iteration
        float error = 0.001f;

        //We also need to avoid infinite loops
        int iterations = 0;

        while (true)
        {
            //Newton's method
            float tNext = t - ((GetLengthSimpsons(0f, t) - d) / GetArcLengthIntegrand(t));

            //Have we reached the desired accuracy?
            if (Mathf.Abs(tNext - t) < error)
            {
                break;
            }

            t = tNext;

            iterations += 1;

            if (iterations > 1000)
            {
                break;
            }
        }

        return t;
    }
    //The De Casteljau's Algorithm
    public Vector3 DeCasteljausAlgorithm(float t)
    {
        //Linear interpolation = lerp = (1 - t) * A + t * B
        //Could use Vector3.Lerp(A, B, t)

        //To make it faster
        float oneMinusT = 1f - t;

        //Layer 1
        Vector3 Q = oneMinusT * A + t * B;
        Vector3 R = oneMinusT * B + t * C;
        Vector3 S = oneMinusT * C + t * D;

        //Layer 2
        Vector3 P = oneMinusT * Q + t * R;
        Vector3 T = oneMinusT * R + t * S;

        //Final interpolated position
        Vector3 U = oneMinusT * P + t * T;

        return U;
    }

    //Get the length of the curve between two t values with Simpson's rule
    public float GetLengthSimpsons(float tStart, float tEnd)
    {
        //This is the resolution and has to be even
        int n = 20;

        //Now we need to divide the curve into sections
        float delta = (tEnd - tStart) / (float)n;

        //The main loop to calculate the length

        //Everything multiplied by 1
        float endPoints = GetArcLengthIntegrand(tStart) + GetArcLengthIntegrand(tEnd);

        //Everything multiplied by 4
        float x4 = 0f;
        for (int i = 1; i < n; i += 2)
        {
            float t = tStart + delta * i;

            x4 += GetArcLengthIntegrand(t);
        }

        //Everything multiplied by 2
        float x2 = 0f;
        for (int i = 2; i < n; i += 2)
        {
            float t = tStart + delta * i;

            x2 += GetArcLengthIntegrand(t);
        }

        //The final length
        float length = (delta / 3f) * (endPoints + 4f * x4 + 2f * x2);

        return length;
    }

    //Get and infinite small length from the derivative of the curve at position t
    float GetArcLengthIntegrand(float t)
    {
        //The derivative at this point (the velocity vector)
        Vector3 dPos = DeCasteljausAlgorithmDerivative(t);

        //This the how it looks like in the YouTube videos
        //float xx = dPos.x * dPos.x;
        //float yy = dPos.y * dPos.y;
        //float zz = dPos.z * dPos.z;

        //float integrand = Mathf.Sqrt(xx + yy + zz);

        //Same as above
        float integrand = dPos.magnitude;

        return integrand;
    }

    //The derivative of cubic De Casteljau's Algorithm
    Vector3 DeCasteljausAlgorithmDerivative(float t)
    {
        Vector3 dU = t * t * (-3f * (A - 3f * (B - C) - D));

        dU += t * (6f * (A - 2f * B + C));

        dU += -3f * (A - B);

        return dU;
    }

    //Get the length of the curve with a naive method where we divide the
    //curve into straight lines and then measure the length of each line
    float GetLengthNaive(float tStart, float tEnd)
    {
        //This is the resolution, the higher the better
        int sections = 100;

        //Divide the curve into sections
        float delta = (tEnd - tStart) / (float)sections;

        //The start position of the curve
        Vector3 lastPos = DeCasteljausAlgorithm(tStart);

        //Init length
        float length = 0f;

        //Move along the curve
        for (int i = 1; i <= sections; i++)
        {
            //Calculate the t value at this section
            float t = tStart + delta * i;

            //Find the coordinates at this t
            Vector3 pos = DeCasteljausAlgorithm(t);

            //Add the section to the total length
            length += Vector3.Magnitude(pos - lastPos);

            //Save the latest pos for next loop
            lastPos = pos;
        }

        return length;
    }

    //Divide the curve into equal steps
    void DivideCurveIntoSteps()
    {
        //Find the total length of the curve
        float totalLength = GetLengthSimpsons(0f, 1f);

        Debug.LogFormat("totalLength: {0}", totalLength);

        //How many sections do we want to divide the curve into
        int parts = 100;

        //What's the length of one section?
        float sectionLength = totalLength / (float)parts;

        //Init the variables we need in the loop
        float currentDistance = 0f + sectionLength;

        //The curve's start position
        Vector3 lastPos = A;

        //The Bezier curve's color
        //Need a seed or the line will constantly change color
        Random.InitState(12345);

        int lastRandom = Random.Range(0, colorsArray.Length);

        for (int i = 1; i <= parts; i++)
        {
            //Use Newton–Raphsons method to find the t value from the start of the curve 
            //to the end of the distance we have
            float t = FindTValue(currentDistance, totalLength);

            //Get the coordinate on the Bezier curve at this t value
            Vector3 pos = DeCasteljausAlgorithm(t);


            //Draw the line with a random color
            int newRandom = Random.Range(0, colorsArray.Length);

            //Get a different random number each time
            while (newRandom == lastRandom)
            {
                newRandom = Random.Range(0, colorsArray.Length);
            }

            lastRandom = newRandom;

            Gizmos.color = colorsArray[newRandom];

            Gizmos.DrawLine(lastPos, pos);


            //Save the last position
            lastPos = pos;

            //Add to the distance traveled on the line so far
            currentDistance += sectionLength;
        }
    }
}
