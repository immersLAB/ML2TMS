using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
//using static UnityEditor.FilePathAttribute;
using UnityEditor;
using UnityEngine.UI;

/// <summary>
/// This script takes in the position of the head, active target, and coil 
/// to find the distance and angle from the coil/start position. References CurrentTarget in TargetActivator.cs for HeadTarget.
/// The script displays the distance in cm through Distance Text.
/// </summary>
public class CoilPosMeasure : MonoBehaviour
{
    public Transform CoilCenter, HeadTarget;
    public GameObject Head;
    public Text distanceText;

    // Update is called once per frame
    void Update()
    {
        float coilDist = MeasureDistance(CoilCenter, HeadTarget);
        float convertedDist = coilDist * 100.0f; // Convert to centimeters

        // Update the UI Text with the converted distance value
        distanceText.text = "Distance: " + convertedDist.ToString("F2") + " cm";

        Vector3 targetNormalizedVector = GetTargetNormalizedVector(); // Get the normalized vector from TargetActivator
        Vector3 referenceVector = CoilCenter.forward;

        float angle = CalculateAngle(targetNormalizedVector, referenceVector);

        Debug.Log("Distance: " + convertedDist + ", Angle: " + angle);
    }

    // MeasureDistance is used to return distance from start position to target position.
    float MeasureDistance(Transform start, Transform destination)
    {
        float dist = Vector3.Distance(start.position, destination.GetComponent<TargetActivator>().CurrentTarget.position);
        return dist;
    }

    Vector3 GetTargetNormalizedVector()
    {
        return HeadTarget.GetComponent<TargetActivator>().GetNormalizedVector();
    }

    float CalculateAngle(Vector3 refVector, Vector3 destVector)
    {
        // Calculate dot product of normalized vectors
        float dotProduct = Vector3.Dot(refVector, destVector);

        // Calculate angle in degrees using arccosine
        float angle = Mathf.Rad2Deg * Mathf.Acos(dotProduct);

        return angle;
    }
}

