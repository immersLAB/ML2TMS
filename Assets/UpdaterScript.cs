using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdaterScript : MonoBehaviour
{
    public GameObject coil;
    public GameObject cube;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        cube.transform.position = coil.transform.position;
        cube.transform.rotation = coil.transform.rotation;

    }
}
