using System;
using System.Collections;

using UnityEngine;
using UnityEngine.SceneManagement;

using Photon.Pun;
using Photon.Realtime;
using System.Linq;
using UnityEngine.UI;
using TMPro;
using Microsoft.MixedReality.Toolkit.UI;

public class GameManagerCalls : MonoBehaviourPunCallbacks
{
    public bool situationActive;


    public void ShowSimulationGameobjects()
    {
        PhotonView photonView = PhotonView.Get(this);
        photonView.RPC("OnClick_StartScene", RpcTarget.AllBuffered);
    }

    [PunRPC]
    public void OnClick_StartScene()
    {
        Debug.Log("working here");
        FindGameObjectsAll("MainSlate-Simulation(Clone)").SetActive(true);
        FindGameObjectsAll("XRay(Clone)").SetActive(true);
        FindGameObjectsAll("LINAC(Clone)").SetActive(true);
       // FindGameObjectsAll("MedCart(Clone)").SetActive(true);
        FindGameObjectsAll("VolumeData(Clone)").SetActive(true);
        FindGameObjectsAll("SimulationButton(Clone)").SetActive(false);
        FindGameObjectsAll("SituationButton(Clone)").SetActive(false);

    }

    public static GameObject FindGameObjectsAll(string name) => Resources.FindObjectsOfTypeAll<GameObject>().First(x => x.name == name);


    public void ExitSimulationGameobjects()
    {
        PhotonView photonView = PhotonView.Get(this);
        photonView.RPC("ExitSimulation", RpcTarget.AllBuffered);
    }


    [PunRPC]
    public void ExitSimulation()
    {
        Debug.Log("working here");
        FindGameObjectsAll("MainSlate-Simulation(Clone)").SetActive(false);
        FindGameObjectsAll("XRay(Clone)").SetActive(false);
        FindGameObjectsAll("LINAC(Clone)").SetActive(false);
        //FindGameObjectsAll("MedCart(Clone)").SetActive(false);
        FindGameObjectsAll("VolumeData(Clone)").SetActive(false);
        FindGameObjectsAll("SimulationButton(Clone)").SetActive(true);
        FindGameObjectsAll("SituationButton(Clone)").SetActive(true);
        DestroyWindows("SlateOFEach_final");
        DestroyWindows("AcqWindow");
        DestroyWindows("DemograpWindow");
        DestroyWindows("MetaSlatefinal");

        // Close all buttons with names 
    }

    [PunRPC]
    public void CloseWindow1()
    {
        // Put your logic here to close the window
        // For example, destroy the GameObject representing the window
        Destroy(gameObject); // Assuming this script is attached to the window GameObject
    }

    public void DestroyWindows(string prefix)
    {
        // Find all game objects with names starting with the given prefix
        GameObject[] windows = GameObject.FindObjectsOfType<GameObject>(); 

        foreach (GameObject window in windows)
        {
            // Check if the name starts with the specified prefix
            if (window.name.StartsWith(prefix))
            {
                // Destroy the window
                Destroy(window);
            }
        }
    }

    public void ShowSituationGameobjects()
    {
        PhotonView photonView = PhotonView.Get(this);
        photonView.RPC("OnClick_StartSituation", RpcTarget.AllBuffered);
    }

    [PunRPC]
    public void OnClick_StartSituation()
    {
       //FindGameObjectsAll("MainSlate-Simulation(Clone)").SetActive(true);
      //  FindGameObjectsAll("SimulationButton(Clone)").SetActive(false);
   
        situationActive = !situationActive;
        UpdateVisibility();

    }

    // Update visibility of the object and SITUATION MODE
    void UpdateVisibility()
    {
        if (situationActive == true)
        {
            FindGameObjectsAll("MainSlate-Simulation(Clone)").SetActive(true);
            FindGameObjectsAll("VolumeData(Clone)").SetActive(true);

            FindGameObjectsAll("SimulationButton(Clone)").SetActive(false);

            // FindGameObjectsAll("SituationButton(Clone").GetComponentInChildren<TextMeshPro>();
            FindGameObjectsAll("SituationButton(Clone)").GetComponentInChildren<TextMeshPro>().text = "Exit Situation";

        }
        else
        {
            Debug.Log("Simulation is false ");
            DestroyWindows("SlateOFEach_final");
            DestroyWindows("AcqWindow");
            DestroyWindows("DemograpWindow");
            DestroyWindows("MetaSlatefinal");
            FindGameObjectsAll("MainSlate-Simulation(Clone)").SetActive(false);
            FindGameObjectsAll("SimulationButton(Clone)").SetActive(true);
            FindGameObjectsAll("SituationButton(Clone)").GetComponentInChildren<TextMeshPro>().text = "Enter Situation";




        }

        
    }



}