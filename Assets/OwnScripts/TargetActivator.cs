using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
//using UnityEditor.PackageManager;
using UnityEditor;
using UnityEngine;

/// <summary>
/// This script chooses one target to activate and retrieves its normalized vector. 
/// </summary>
public class TargetActivator : MonoBehaviour
{
    // Declare position, rotation, and scale of 2 targets
    public Transform TargetParent, CurrentTarget;
    // Declare list of all targets
    public List<Transform> allTargets = new List<Transform>();
    // Declare position in the list of targets
    public int index = 0;
    private Transform targetVector;

    // Start function called before first frame update. Used to set all targets to false. 
    private void Start()
    {
        foreach(Transform child in TargetParent)
        {
            allTargets.Add(child);
            child.gameObject.SetActive(false);            
        }
        CurrentTarget = allTargets[index];
        CurrentTarget.gameObject.SetActive(true);

        // Find the child named "DirectionIndicatorCylinder" under the selected target
        targetVector = CurrentTarget.Find("DirectionIndicatorCylinder");
        targetVector.gameObject.SetActive(true);
    }

    // Update is called once per frame. With user input, user can determine which target to set true. 
    void Update()
    {
        //replace with ML2 input
        if (Input.GetKeyDown("space"))
        {
            CurrentTarget.gameObject.SetActive(false);
            index++;
            CurrentTarget = allTargets[index];
            CurrentTarget.gameObject.SetActive(true);

            // Find the child named "DirectionIndicatorCylinder" under the selected target
            targetVector = CurrentTarget.Find("DirectionIndicatorCylinder");
            // Set child active
            targetVector.gameObject.SetActive(true);

        }
    }

    // Expose a public method to retrieve the normalized vector
    public Vector3 GetNormalizedVector()
    {
        Vector3 normal = targetVector.up;
        // reference worldspace position
        Vector3 worldPose = targetVector.transform.position;
        Debug.DrawRay(worldPose, transform.TransformDirection(normal), Color.red);
        return normal;
    }
}
