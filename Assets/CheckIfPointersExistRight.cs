using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckIfPointersExistRight : MonoBehaviour
{

    [SerializeField] public GameObject pointerRight;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        try
        {
            var objposition = GameObject.Find("Right_ShellHandRayPointer(Clone)");
            if (objposition != null)
            {
                Debug.Log(objposition.transform.position);
                Debug.Log(objposition.transform.rotation);
                var a = PhotonNetwork.Instantiate(pointerRight.name, objposition.transform.position, objposition.transform.rotation);

                Debug.Log(a.name);
            }


        }
        catch {
            Debug.Log("Right Hand Tracking Failed");
        
        }
    }
}
