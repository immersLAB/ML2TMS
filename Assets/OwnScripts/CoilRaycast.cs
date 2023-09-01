using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// This script draws a ray from the given position and lets you know it hit something in the debugger.
public class CoilRaycast : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Update()
    {
        if (Physics.Raycast(this.transform.position, this.transform.TransformDirection(Vector3.forward), out RaycastHit hitinfo, 20))
        {
            Debug.Log("Hit Something");
            Debug.DrawRay(this.transform.position, this.transform.TransformDirection(Vector3.forward) * hitinfo.distance, Color.red);
        }
        else
        {
            Debug.Log("Hit Nothing");
            Debug.DrawRay(this.transform.position, this.transform.TransformDirection(Vector3.forward) * 20f, Color.green);
        }

    }

}
