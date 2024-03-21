using MRTK.Tutorials.AzureSpatialAnchors;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserClicked : MonoBehaviour
{
   public GameObject nextButton;
   public int imageIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        if (PhotonNetwork.PrefabPool is DefaultPool pool)
        {
            if (nextButton != null) pool.ResourceCache.Add(nextButton.name, nextButton);

               }

        createButtons();
        Debug.Log("Next Button created");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void createButtons()
    {
        PhotonNetwork.Instantiate(nextButton.name, new Vector3(0, 0, 0), Quaternion.identity, 0);


    }

    
}
