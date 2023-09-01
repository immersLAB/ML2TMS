using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Target : MonoBehaviour
{
    public GameObject targetPrefab;
    public GameObject[] targets;

    // Start is called before the first frame update
    void Start()
    {
        // get list
        if (targets == null || targets.Length == 0)
            targets = GameObject.FindGameObjectsWithTag("Target");
        
        // choose one random number from 0 to length of list (use Random.Range and floor)
        int chooseRandom = Random.Range(0, targets.Length);

        //activate
        GameObject chosenTarget = targets[chooseRandom];
        if (!chosenTarget.activeSelf)
        {
            chosenTarget.SetActive(true);
        }
    }

}
