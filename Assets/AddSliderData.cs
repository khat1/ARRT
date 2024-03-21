using Dicom;
using Dicom.Imaging;
using Dicom.Printing;
using Microsoft.MixedReality.Toolkit.UI;
using MRTK.Tutorials.MultiUserCapabilities;
using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TMPro;
using Unity.VisualScripting;
using Unity.XR.CoreUtils;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class AddSliderData : MonoBehaviourPun
{
    // Start is called before the first frame update
    [SerializeField]
    public int m_ImageIndex;

    [SerializeField]
    bool m_Auto = false;
    [SerializeField]
    float m_WaitTime = 2.0f;


    private int currentlyDisplayedImageIndex = -1;

    string[] m_Dicom_filenames;
    float m_ElapsedTime = 0.0f;

    Texture2D m_Texture = null;

    public GameObject defaultWindow;

    public GameObject metaWindow;
    public GameObject acqWindow;
    public GameObject demWindow;


    public PhotonView AddsliderDataPhotonView;

    DirectoryInfo newInfo = new DirectoryInfo(Path.Combine(Application.streamingAssetsPath, "DicomFiles"));

    // Use this for initialization
    void Start()
    {
        AddsliderDataPhotonView = GetPhotonView("Button (1)");
        //Debug.Log(m_Dicom_directorypath);
        // Check exist
        //if (!System.IO.Directory.Exists(m_Dicom_directorypath))
        //    return;


        var a = Path.Combine(Application.streamingAssetsPath);
        //Debug.Log(a);   

        var files = System.IO.Directory.GetFiles(a + "/DicomFiles", "*.dcm");
        Debug.Log("File Length: " + files.Length);


        // foreach (var b in files)
        // {
        //   //     Debug.Log(b);
        // }

        // Debug.Log(m_Dicom_directorypath);
        // var textures = Resources.LoadAll("DicomFiles");
        //Debug.Log("5555"+textures);


        //Debug.Log(textures.Length);
        m_Dicom_filenames = files;
        //Debug.Log("123123"+ m_Dicom_filenames);

        UpdateImage(m_ImageIndex);
        // Debug.Log("111111111111111111111111111111111111111111111111111111111111" + m_ImageIndex);
    }



    // Update is called once per frame
    void Update()

    {
        //   Debug.Log("111111111111111111111111111111111111111111111111111111111111" + m_ImageIndex);
        //  var localIndex = 0;
        // Debug.Log("22222222222222222222222222222222222222222222222222222222222" + localIndex);
        /**
        Debug.Log("hello");
        if (m_Auto)
        {
            m_ElapsedTime += Time.deltaTime * m_Dicom_filenames.Length / 5.0f;
            m_ImageIndex = (int)m_ElapsedTime;
            if (m_ImageIndex >= m_Dicom_filenames.Length)
            {
                m_ImageIndex = 0;
                m_ElapsedTime = 0.0f;
            }
            UpdateImage();
        }
        **/

    }

    public void testOpenNewWindowOfSlicer()
    {

        /** Debug.Log("Testing click form another class");
        PhotonView photonView = PhotonView.Get(this);

        if (m_ImageIndex != currentlyDisplayedImageIndex)
        {
            currentlyDisplayedImageIndex = m_ImageIndex;

            photonView.RPC("UpdateImage", RpcTarget.All, m_ImageIndex);
            Debug.Log("HERE111" + m_ImageIndex);
        }
       UpdateImage(); **/

        //AddsliderDataPhotonView.RPC("InitnewWindow", RpcTarget.All, GameObject.Find("PinchSlider (1)(Clone)").GetComponent<UpdateSliderMulti>().currentlyDisplayedImageIndex);

       InitnewWindow(GameObject.Find("PinchSlider (1)").GetComponent<UpdateSliderMulti>().currentlyDisplayedImageIndex);
    }


    public void InitnewWindow(int theindexofImage)
    { 
        try
        { 
            string objectName = defaultWindow.name + "_" + theindexofImage;
         
      

            if (GameObject.Find(objectName) == null)
            {
                var a = PhotonNetwork.Instantiate(defaultWindow.name, defaultWindow.transform.position, defaultWindow.transform.rotation);
     
                AddsliderDataPhotonView.RPC("CreateInteractableObjects", RpcTarget.All, objectName, theindexofImage);
            }
            else
            {
                Debug.Log("Already Added cannot add");
            }


        }
        catch (Exception e)
        {
            Debug.Log(e);

        }


    }

    public static GameObject FindGameObjectsAll(string name) => Resources.FindObjectsOfTypeAll<GameObject>().First(x => x.name == name);


    [PunRPC]
    private void CreateInteractableObjects(string objectName, int theindexofImage)
    {
        Debug.Log("HERE111");
        var GameObject = FindGameObjectsAll("SlateOFEach_final(Clone)");
        GameObject.name = objectName;
        Debug.Log(GameObject.name + objectName);
        Debug.Log("aaaa" + newInfo);

        int currentImageIndex = theindexofImage + 1;
        string prev = "";
        FileInfo[] fi = newInfo.GetFiles("*dcm");
        FileInfo file = fi[currentImageIndex];
        Debug.Log("File found" + file.Name);


        Debug.Log("HGGHG" + fi.Length);
       

        // UPDATE THE IMAGE IN THE SUB WINDOW 
        
       var imgObj = GameObject.gameObject.GetComponentInChildren<Image>();
       UpdateImageOnWindow(imgObj, currentImageIndex);

        // ADD ONCLICK LISTENERS TO THE BUTTONS 
      
       GameObject.Find(objectName + "MetaData").GetComponent<ButtonConfigHelper>().OnClick.AddListener(() => showMetaModel(Path.Combine(Application.streamingAssetsPath, "DicomFiles/") + file.Name, currentImageIndex));
       GameObject.Find(objectName + "AcquisitionData").GetComponent<ButtonConfigHelper>().OnClick.AddListener(() => showAcquisitionModel(Path.Combine(Application.streamingAssetsPath, "DicomFiles/") + file.Name, currentImageIndex));
       GameObject.Find(objectName + "PatientDemographics").GetComponent<ButtonConfigHelper>().OnClick.AddListener(() => showDemographicModel(Path.Combine(Application.streamingAssetsPath, "DicomFiles/") + file.Name, currentImageIndex));

        Debug.Log("gameobject Link" + GameObject.Find(objectName));
        // imgObj.GetComponent<DicomLoaderForUIImageBehaviour>().m_Dicom_filename = Path.Combine(Application.streamingAssetsPath, "DicomFiles/") + file.Name;
        Debug.Log("33333"+imgObj);
  

    }

    private void showDemographicModel(string link, int currentImageIndex)
    {
        List<string> patientInfoTags = new List<string>();
        List<string> patientValues = new List<string>();
        //Get the data 

        var number = 0;
        var data_set = DicomFile.Open(link).Dataset;

        //Debug.Log(data_set);
        foreach (var tag_name in data_set)
        {

            if (number > 28 && number < 34)
            {
                patientInfoTags.Add(tag_name.ToString());
                patientValues.Add($"{tag_name.Tag}");
                // Debug.Log(tag_name.Tag);
                // Debug.Log(number + tag_name.ToString());

            }
            number++;
        }

        var counter = 0;
        List<string> dicomdatatotext = new List<string>();
        foreach (var c in patientValues)
        {

            // Debug.Log(c);
            var e = Dicom.DicomTag.Parse(c);
            //  Debug.Log($" {metaTags[counter]} : {data_set.Get(e, "Default")}");
            //  my_Text.text = $" {metaTags[counter]} : {data_set.Get(e, "Default")}";
            dicomdatatotext.Add($" {patientInfoTags[counter]} : {data_set.Get(e, "Default")}");
            counter++;

            //Debug.Log($"{tag_name} : {a.Get(entry.Tag, "Default")}");
        }

        StringBuilder sb = new StringBuilder();

        foreach (string a in dicomdatatotext)
        {
            // s = s + line.ToString() + "\n\n";

            Debug.Log(a);

            sb.AppendFormat(a + "\n");
            sb.AppendFormat("---------------------------------------------------------\n");

        }
        InitDemWindow(sb.ToString(), currentImageIndex);

    }

    

    private void showAcquisitionModel(string link, int currentImageIndex)
    {
        List<string> patientInfoTags = new List<string>();
        List<string> patientValues = new List<string>();
        //Get the data 

        var number = 0;
        var data_set = DicomFile.Open(link).Dataset;

        //Debug.Log(data_set);
        foreach (var tag_name in data_set)
        {

            if (number > 40 && number < 100)
            {
                patientInfoTags.Add(tag_name.ToString());
                patientValues.Add($"{tag_name.Tag}");
                //Debug.Log(tag_name.Tag);
                // Debug.Log(number + tag_name.ToString());

            }
            number++;
        }

        var counter = 0;
        List<string> dicomdatatotext = new List<string>();
        foreach (var c in patientValues)
        {

            // Debug.Log(c);
            var e = Dicom.DicomTag.Parse(c);
            //  Debug.Log($" {metaTags[counter]} : {data_set.Get(e, "Default")}");
            //  my_Text.text = $" {metaTags[counter]} : {data_set.Get(e, "Default")}";
            dicomdatatotext.Add($" {patientInfoTags[counter]} : {data_set.Get(e, "Default")}");
            counter++;

            //Debug.Log($"{tag_name} : {a.Get(entry.Tag, "Default")}");
        }

        StringBuilder sb = new StringBuilder();

        foreach (string a in dicomdatatotext)
        {
            // s = s + line.ToString() + "\n\n";

            Debug.Log(a);

            sb.AppendFormat(a + "\n");
            sb.AppendFormat("---------------------------------------------------------\n");

        }
       // AddsliderDataPhotonView.RPC("IniAcquisitionWindow", RpcTarget.All, sb.ToString(), currentImageIndex);

        InitAcqWindow(sb.ToString(), currentImageIndex);

    }

    private void showMetaModel(string link, int currentImageIndex)
    {
        Debug.Log("OnclickShowMetaModel");

        List<string> patientInfoTags = new List<string>();
        List<string> patientValues = new List<string>();


        //Get the data 

        var number = 0;
        var data_set = DicomFile.Open(link).Dataset;

        foreach (var tag_name in data_set)
        {

            if (number < 15)
            {
                patientInfoTags.Add(tag_name.ToString());
                patientValues.Add($"{tag_name.Tag}");
                //Debug.Log(tag_name.Tag);
                //   Debug.Log(number + tag_name.ToString());

            }
            number++;
        }

        var counter = 0;
        List<string> dicomdatatotext = new List<string>();
        foreach (var c in patientValues)
        {

            // Debug.Log(c);
            var e = Dicom.DicomTag.Parse(c);
            //  Debug.Log($" {metaTags[counter]} : {data_set.Get(e, "Default")}");
            //  my_Text.text = $" {metaTags[counter]} : {data_set.Get(e, "Default")}";
            dicomdatatotext.Add($" {patientInfoTags[counter]} : {data_set.Get(e, "Default")}");
            counter++;

            //Debug.Log($"{tag_name} : {a.Get(entry.Tag, "Default")}");
        }

        StringBuilder sb = new StringBuilder();

        foreach (string a in dicomdatatotext)
        {
            // s = s + line.ToString() + "\n\n";

            // Debug.Log(a);

            sb.AppendFormat(a + "\n");
            sb.AppendFormat("---------------------------------------------------------\n");

        }

        
       // AddsliderDataPhotonView.RPC("InitMetaWindow", RpcTarget.All, sb.ToString(), currentImageIndex);

        InitMetaWindow(sb.ToString(), currentImageIndex);

      //  Debug.Log("Somedata" + sb);
      //  Debug.Log("Somedata" + sb);

        //  var number = 0;
        //   var data_set = DicomFile.Open(link).Dataset;



    }


    private void InitAcqWindow(string sb, int currentImageIndex)
    {

        Debug.Log("HERE" + currentImageIndex);
        try
        {
            string objectName = acqWindow.name + "_" + currentImageIndex;



            if (GameObject.Find(objectName) == null)
            {
                Debug.Log(sb);
                var a = PhotonNetwork.Instantiate(acqWindow.name, acqWindow.transform.position, acqWindow.transform.rotation);
                AddsliderDataPhotonView.RPC("CreateInteractableObjectsAcqWindow", RpcTarget.All, objectName, currentImageIndex, sb);
            }
            else
            {
                Debug.Log("Already Added cannot add");
            }


        }
        catch (Exception e)
        {
            Debug.Log(e);

        }



    }

    private void InitMetaWindow(string sb, int currentImageIndex)
    {

        Debug.Log("HERE" + currentImageIndex);
        try
        {
            string objectName = metaWindow.name + "_" + currentImageIndex;



            if (GameObject.Find(objectName) == null)
            {
                Debug.Log(sb);
                var a = PhotonNetwork.Instantiate(metaWindow.name, metaWindow.transform.position, metaWindow.transform.rotation);
                AddsliderDataPhotonView.RPC("CreateInteractableObjectsMetaWindow", RpcTarget.All, objectName, currentImageIndex, sb);

            }
            else
            {
                Debug.Log("Already Added cannot add");
            }


        }
        catch (Exception e)
        {
            Debug.Log(e);

        }



    }
    private void InitDemWindow(string sb, int currentImageIndex)
    {

        Debug.Log("HERE" + currentImageIndex);
        try
        {
            string objectName = demWindow.name + "_" + currentImageIndex;



            if (GameObject.Find(objectName) == null)
            {
                Debug.Log(sb);
                var a = PhotonNetwork.Instantiate(demWindow.name, demWindow.transform.position, demWindow.transform.rotation);
                AddsliderDataPhotonView.RPC("CreateInteractableObjectsDemWindow", RpcTarget.All, objectName, currentImageIndex, sb);
            }
            else
            {
                Debug.Log("Already Added cannot add");
            }


        }
        catch (Exception e)
        {
            Debug.Log(e);

        }
    }
    [PunRPC]
    private void CreateInteractableObjectsMetaWindow(string objectName, int theindexofImage,string sb)
    {
        Debug.Log("HERE111" );
        var GameObject = FindGameObjectsAll("MetaSlatefinal(Clone)");
        GameObject.name = objectName;

        var editTextObject = GameObject.gameObject.GetNamedChild("EditedText");

        var component = editTextObject.GetComponents<TMP_Text>();
        Debug.Log("123123123" + component);
        var textComponent = editTextObject.GetComponent<TMP_Text>();
        if (textComponent != null)
        {
            textComponent.text = sb; // Set IDCOM META desired text 
        }
        else
        {
            Debug.LogError("TMP_Text component not found on 'EditedText' game object.");
        }



    }

    [PunRPC]
    private void CreateInteractableObjectsAcqWindow(string objectName, int theindexofImage, string sb)
    {
        Debug.Log("HERE111");
        var GameObject = FindGameObjectsAll("AcqWindow(Clone)");
        GameObject.name = objectName;

        var editTextObject = GameObject.gameObject.GetNamedChild("EditedText");

        var component = editTextObject.GetComponents<TMP_Text>();
        Debug.Log("123123123" + component);
        var textComponent = editTextObject.GetComponent<TMP_Text>();
        if (textComponent != null)
        {
            textComponent.text = sb; // Set IDCOM META desired text 
        }
        else
        {
            Debug.LogError("TMP_Text component not found on 'EditedText' game object.");
        }



    }


    [PunRPC]
    private void CreateInteractableObjectsDemWindow(string objectName, int theindexofImage, string sb)
    {
        Debug.Log("HERE111");
        var GameObject = FindGameObjectsAll("DemograpWindow(Clone)");
        GameObject.name = objectName;

        var editTextObject = GameObject.gameObject.GetNamedChild("EditedText");

        var component = editTextObject.GetComponents<TMP_Text>();
        Debug.Log("123123123" + component);
        var textComponent = editTextObject.GetComponent<TMP_Text>();
        if (textComponent != null)
        {
            textComponent.text = sb; // Set IDCOM META desired text 
        }
        else
        {
            Debug.LogError("TMP_Text component not found on 'EditedText' game object.");
        }



    }




    private void RenameObjectAndAddData(string objectName)
    {
        Debug.Log("Rename Add Data");
    }

    [PunRPC]
    public void UpdateImage( int index)
    {
        try
        {
          


        
        Debug.Log("working here234234234");

        if (index < 0 || index >= m_Dicom_filenames.Length)
            return;

        if (!System.IO.File.Exists(m_Dicom_filenames[index]))
            return;

        var img = new DicomImage(m_Dicom_filenames[index]);

        // Debug.Log("helloooo" + img);

        // Failed to load
        if (img == null)
        {
            Debug.Log("Failed to load DICOM image");
            return;
        }

        // Debug.Log(image);
        var tex = img.RenderImage().AsTexture2D();

        Sprite texture_sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), Vector2.zero);
       this.GetComponent<Image>().sprite = texture_sprite;

        // Change local scale
        if (tex.width < tex.height)
        {
            this.transform.localScale = new Vector3((float)tex.width / (float)tex.height, 1.0f, 1.0f);
        }
        else
        {
            this.transform.localScale = new Vector3(1.0f, (float)tex.height / (float)tex.width, 1.0f);
        }
        }
        catch (Exception ex)
        {
            Debug.Log("couldnot load first slider" + ex);
        }
        {
            Debug.Log("Right Hand Tracking Failed");

        }
    }


    [PunRPC]
    public void UpdateImageOnWindow(Image a, int index)
    {

        Debug.Log("working here");

        if (index < 0 || index >= m_Dicom_filenames.Length)
            return;

        if (!System.IO.File.Exists(m_Dicom_filenames[index]))
            return;

        var img = new DicomImage(m_Dicom_filenames[index]);

        // Debug.Log("helloooo" + img);

        // Failed to load
        if (img == null)
        {
            Debug.Log("Failed to load DICOM image");
            return;
        }

        // Debug.Log(image);
        var tex = img.RenderImage().AsTexture2D();

        Sprite texture_sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), Vector2.zero);
        //this.GetComponent<Image>().sprite = texture_sprite;
        a.sprite = texture_sprite;

        // Change local scale
        if (tex.width < tex.height)
        {
            this.transform.localScale = new Vector3((float)tex.width / (float)tex.height, 1.0f, 1.0f);
        }
        else
        {
            this.transform.localScale = new Vector3(1.0f, (float)tex.height / (float)tex.width, 1.0f);
        }
    }
    private PhotonView GetPhotonView(string objectName)
    {
        return PhotonView.Get(GameObject.Find(objectName).GetComponent<PhotonView>());
    }


    public void closewindow()
    {
        AddsliderDataPhotonView.RPC("CloseWindowInall", RpcTarget.All);
    }

    [PunRPC]
    public void CloseWindowInall()
    {
        this.gameObject.SetActive(false);
    }

}
