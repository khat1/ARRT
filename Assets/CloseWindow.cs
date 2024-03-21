using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseWindow : MonoBehaviour
{
    // Start is called before the first frame update

    public PhotonView btn;
    void Start()
    {
       
       btn = GetComponent<PhotonView>();
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnButtonPress()
    {
        // When the button is pressed, send an RPC to close the window
       btn.RPC("CloseWindow1", RpcTarget.All);
    }

    [PunRPC]
    public void CloseWindow1()
    {
        // Put your logic here to close the window
        // For example, destroy the GameObject representing the window
        Destroy(gameObject); // Assuming this script is attached to the window GameObject
    }
}
