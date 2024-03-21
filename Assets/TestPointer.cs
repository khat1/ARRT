using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPointer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var foundLeftPointer = GameObject.Find("Right_ShellHandRayPointer(Clone");

        if(foundLeftPointer != null)
        {

            Debug.Log("Found pointer");
        }
        else
        {
            Debug.Log("Not found");
        }
    }
}
