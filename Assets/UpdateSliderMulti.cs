using ExitGames.Client.Photon;
using Microsoft.MixedReality.Toolkit.UI;
using Photon.Pun;
using Photon.Pun.Demo.PunBasics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class UpdateSliderMulti : MonoBehaviour
{

    public int windowNo = 1;
    public int patientButton = 1;
    public int metaButton = 1;
    public int AcqButton = 1;

    public float synchronosedValueplayer;

    public int currentlyDisplayedImageIndex = -1;

    public GameObject Slider;
    public GameObject prefabWindow;
    public Vector3 thumbshootPos;

    private float lastSentValue = -1.0f;

    private int lastSliderValue = -1; // Initialize with an invalid value

    public GameObject pinchSliderObject;

    public PhotonView PinchSliderPhotonView;



    // Start is called before the first frame update
    void Start()
    {
        pinchSliderObject = GameObject.Find("PinchSlider (1)");
        PinchSliderPhotonView = GetPhotonView("PinchSlider (1)");
        PhotonView p = GetPhotonView("Button (1)");
        p.RPC("UpdateImage", RpcTarget.All, 0);
    }
  
    
 
    public void changerAllSlider()
    {
        if (PinchSliderPhotonView != null)
        {
            PinchSliderPhotonView.RPC("ValueChangeCheck", RpcTarget.AllBuffered);
        }
        else
        {
            Debug.Log("not found");
        }
        
    }

    [PunRPC]
    public void ValueChangeCheck()

    {
        Debug.Log("here");

        var value = pinchSliderObject.GetComponent<PinchSlider>().SliderValue;

        int v = (int)Math.Round(value * 100);

        if (v != currentlyDisplayedImageIndex)
        {
            Debug.Log("123123");
            pinchSliderObject.GetComponent<UpdateSliderMulti>().currentlyDisplayedImageIndex= v;

            var imageObj = GameObject.Find("UGUIButtons").GetComponentInChildren<UnityEngine.UI.Image>();

            PhotonView p = GetPhotonView("Button (1)");

            imageObj.GetComponent<AddSliderData>().m_ImageIndex = v;
            AddSliderData n = imageObj.GetComponent<AddSliderData>();

            if (!PinchSliderPhotonView.IsMine)
            {
                // Handle slider value changes by the local player
              //  PinchSliderPhotonView.RPC("ChangeThumbshootPosition", RpcTarget.All);

                if (value != synchronosedValueplayer)
                {
                    PinchSliderPhotonView.RPC("UpdateSliderValue", RpcTarget.All, value);

                }
            }
            else
            {
                PinchSliderPhotonView.RPC("UpdateSliderValue1", RpcTarget.All, value);
            }

            try
            {
                // p.RPC("setImage", RpcTarget.All, v);
                p.RPC("UpdateImage", RpcTarget.All, v);

                


            }
            catch
            {

                Debug.Log("erroe Here");
            }


      
        }
    }
  
    public static GameObject FindGameObjectsAll(string name) => Resources.FindObjectsOfTypeAll<GameObject>().First(x => x.name == name);



    

    private PhotonView GetPhotonView(string objectName)
    {
        return PhotonView.Get(GameObject.Find(objectName).GetComponent<PhotonView>());
    }

    [PunRPC]
    private void UpdateSliderValue(float newValue)
    {
        // Update the slider value for all players.
        //FindGameObjectsAll("PinchSlider (1)(Clone)").GetComponent<PinchSlider>().SliderValue = newValue;
        pinchSliderObject.GetComponent<UpdateSliderMulti>().synchronosedValueplayer = newValue;
        pinchSliderObject.GetComponent<UpdateSliderMulti>().thumbshootPos = pinchSliderObject.GetComponent<PinchSlider>().thumbrootTransform;

    }

    [PunRPC]
    private void UpdateSliderValue1(float newValue)
    {
        // Update the slider value for all players.
        //FindGameObjectsAll("PinchSlider (1)(Clone)").GetComponent<PinchSlider>().SliderValue = newValue;
        pinchSliderObject.GetComponent <PinchSlider>().SliderValue = newValue;
        pinchSliderObject.GetComponent<UpdateSliderMulti>().synchronosedValueplayer = newValue;
        pinchSliderObject.GetComponent<UpdateSliderMulti>().thumbshootPos = pinchSliderObject.GetComponent<PinchSlider>().thumbrootTransform;
    }



    [PunRPC]
    public void ChangeThumbshootPosition()
    {
        Debug.Log("Change thumbPos Here" + thumbshootPos.x );
        // FindGameObjectsAll("Thum").GetComponent<PinchSlider>().SliderValue = a;

        FindGameObjectsAll("ThumbRoot").GetComponent<Transform>().transform.position = thumbshootPos;
            new Vector3(thumbshootPos.x, 0f, 0f);
    }

    public void showNewModel(string link, string name)
    {

        if (PhotonNetwork.PrefabPool is DefaultPool pool)
        {
            if (prefabWindow != null) pool.ResourceCache.Add(prefabWindow.name, prefabWindow);

        }
        GameObject newObj = PhotonNetwork.Instantiate(prefabWindow.name, new Vector3(0, 0, 0), Quaternion.identity, 0);

        Debug.Log("Next Button created");

        Debug.Log("This ran");
        newObj.name = "Window" + windowNo.ToString(); ;

        var imgObj = newObj.gameObject.GetComponentInChildren<UnityEngine.UI.Image>();
        var titleObj = newObj.gameObject.GetComponentInChildren<TMP_Text>();
        titleObj.text = name;
        //imgObj.GetComponent<DicomLoaderForUIImageBehaviour>().m_Dicom_filename = link;



        var n = newObj.gameObject.name;
        // GameObject.Find(n + "MetaData").GetComponent<ButtonConfigHelper>().OnClick.AddListener(() => showMetaModel(link));
        //  GameObject.Find(n + "AcquisitionData").GetComponent<ButtonConfigHelper>().OnClick.AddListener(() => showAcquisitionModel(link));
        //   GameObject.Find(n + "PatientDemographics").GetComponent<ButtonConfigHelper>().OnClick.AddListener(() => showPatientModel(link));

        windowNo++;
        //newButton.GetComponent<DicomLoaderForUIImageBehaviour>().m_Dicom_filename = $"Assets/DicomFiles/" + v;
        // GameObject.Find("PatientDemographics").GetComponent<ButtonConfigHelper>().OnClick.AddListener(() => showPatientModel(link));
        // GameObject.Find("AcquisitionData").GetComponent<ButtonConfigHelper>().OnClick.AddListener(() => showAcquisitionModel(link));
        //  GameObject.Find("MetaData").GetComponent<ButtonConfigHelper>().OnClick.AddListener(() => showMetaModel(link));
        //Debug.Log(btnPatientData);

    }


}
