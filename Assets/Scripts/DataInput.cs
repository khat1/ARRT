using Dicom;
using Microsoft.MixedReality.Toolkit.UI;
using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DataInput : MonoBehaviour
{
    // public TextMeshProUGUI my_Text;

    public int windowNo = 1;
    public int patientButton = 1;
    public int metaButton = 1;
    public int AcqButton = 1;

    public GameObject buttonParent;
    public GameObject buttonPrefab;

    public GameObject prefabWindow;

    public GameObject prefabPatientData;
    public GameObject prefabMetaData;
    public GameObject prefabAcquisition;
    DirectoryInfo newInfo = new DirectoryInfo(Path.Combine(Application.streamingAssetsPath, "DicomFiles"));

   // public GameObject slider;

    async void Start()
    {
        // Debug.Log(newInfo.ToString());


        //Debug.Log("123123"+path);
        // videoPlayer.url = Path.Combine(Application.streamingAssetsPath, "SampleVideo_1280x720_5mb.mp4");

        // my_Text = GetComponent<TextMeshProUGUI>();

        // GetComponent<ButtonConfigHelper>().OnClick.AddListener(() => showPatientModel(link));
        Debug.Log("Loaded once");
        List<string> metaTags = new List<string>();
        List<string> values = new List<string>();

        //  var info = new DirectoryInfo("Assets/DicomFiles");
       // GameObject newButton = PhotonNetwork.Instantiate(buttonParent.name, new Vector3(0, 0, 0), Quaternion.identity);  

       // GameObject newButton = Instantiate(buttonPrefab, buttonParent.transform);
        // GameObject player = (GameObject)Instantiate(buttonPrefab, (Vector3)data[0], (Quaternion)data[1]);
        // PhotonView photonView = player.GetComponent<PhotonView>();
        // photonView.ViewID = (int)data[2];
        //newButton.GetComponent<Button>().onClick.AddListener(() => Debug.Log("helloworld"));

        //var photonViews = UnityEngine.Object.FindObjectsOfType<PhotonView>();
       // Debug.Log(photonViews.Length);
        
        

        //slider.GetComponent<PinchSlider>().OnValueUpdated.AddListener(delegate { ValueChangeCheck(); });

        //  newButton.GetComponent<DicomLoaderForUIImageWithDirectorypathBehaviour>().m_Dicom_directorypath = newInfo.ToString();

        //  Debug.Log("1313123132" + newInfo.ToString());
        // Debug.Log("1313123132"+ Path.Combine(Application.streamingAssetsPath,"DicomFiles"));

        /*  try
          {
              int a = getNumber();

              foreach (FileInfo file in newInfo.GetFiles("*.dcm"))
              {
                  // Debug.Log("11111111111" + file.Name);
                  if (0 == a)
                  {
                      // Debug.Log(file.Name);
                      //  Debug.Log(index);
                      var imageObj = GameObject.Find("Column1").GetComponentInChildren<Image>();
                      imageObj.GetComponent<Button>().onClick.AddListener(() => showNewModel(Path.Combine(Application.streamingAssetsPath, "DicomFiles/") + file.Name, file.Name));


                  }
                  break;
              }


          }
          catch { }*/

        //Debug.Log(a);


        //var inf1 = info.GetFiles("*.dcm");


        //  slider.GetComponent<PinchSlider>().OnValueUpdated.AddListener(delegate { ValueChangeCheck(new); });

        // setImage(info, newButton, 0);

        /*int currentImageIndex = getNumber();
        int counter_image_Int = 0;
        

        foreach (FileInfo file in info.GetFiles("*.dcm"))
        {
            if(currentImageIndex == counter_image_Int)
            {
            Debug.Log(file.Name);
            newButton.GetComponent<Button>().onClick.AddListener(() => showNewModel($"Assets/DicomFiles/" + file.Name, file.Name));

            }
            counter_image_Int++;
             
         //   GameObject newButton = Instantiate(buttonPrefab, buttonParent.transform);
            //  newButton.GetComponent<Image>();
        //    newButton.GetComponent<Button>().onClick.AddListener(() => showNewModel($"Assets/DicomFiles/" + file.Name, file.Name));
           // newButton.GetComponent<DicomLoaderForUIImageBehaviour>().m_Dicom_filename = $"Assets/DicomFiles/" + file.Name;


        }*/






        // var data_set = DicomFile.Open(@"CT1.2.752.243.1.1.20200602155610500.3000.65254.dcm").Dataset;

        // Debug.Log(data_set);

        /*foreach (var tag_name in data_set)
        {
            metaTags.Add(tag_name.ToString());
          //  Debug.Log(tag_name);
         
        }

        foreach (var b in data_set)
        {
            values.Add($"{b.Tag}");
            //Debug.Log(b.Tag);
        }

        var counter = 0;
        List<string> dicomdatatotext = new List<string>();

        foreach (var c in values)
        {

            // Debug.Log(c);
            var e = Dicom.DicomTag.Parse(c);
          //  Debug.Log($" {metaTags[counter]} : {data_set.Get(e, "Default")}");
          //  my_Text.text = $" {metaTags[counter]} : {data_set.Get(e, "Default")}";
            dicomdatatotext.Add($" {metaTags[counter]} : {data_set.Get(e, "Default")}");
            counter++;
            //Debug.Log($"{tag_name} : {a.Get(entry.Tag, "Default")}");
        }
        string s = "";
        StringBuilder sb = new StringBuilder();
        
        foreach (string a in dicomdatatotext)
        {
           // s = s + line.ToString() + "\n\n";

            

            Debug.Log(a);

            sb.AppendFormat(a + "\n\n");
            
        }
        Debug.Log(sb);
        my_Text.text = sb.ToString();*/

    }
    /*
   public void showNewModel(string link, string name)
   {

       Debug.Log("This ran");

       GameObject newObj = Instantiate(prefabWindow);
       newObj.name = "Window" + windowNo.ToString(); ;

       var imgObj = newObj.gameObject.GetComponentInChildren<Image>();
       var titleObj = newObj.gameObject.GetComponentInChildren<TMP_Text>();
       titleObj.text = name;
       imgObj.GetComponent<DicomLoaderForUIImageBehaviour>().m_Dicom_filename = link;



       var n = newObj.gameObject.name;
       GameObject.Find(n + "MetaData").GetComponent<ButtonConfigHelper>().OnClick.AddListener(() => showMetaModel(link));
       GameObject.Find(n + "AcquisitionData").GetComponent<ButtonConfigHelper>().OnClick.AddListener(() => showAcquisitionModel(link));
       GameObject.Find(n + "PatientDemographics").GetComponent<ButtonConfigHelper>().OnClick.AddListener(() => showPatientModel(link));

       windowNo++;
       //newButton.GetComponent<DicomLoaderForUIImageBehaviour>().m_Dicom_filename = $"Assets/DicomFiles/" + v;
       // GameObject.Find("PatientDemographics").GetComponent<ButtonConfigHelper>().OnClick.AddListener(() => showPatientModel(link));
       // GameObject.Find("AcquisitionData").GetComponent<ButtonConfigHelper>().OnClick.AddListener(() => showAcquisitionModel(link));
       //  GameObject.Find("MetaData").GetComponent<ButtonConfigHelper>().OnClick.AddListener(() => showMetaModel(link));
       //Debug.Log(btnPatientData);

   }
   public void showAcquisitionModel(string link)
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
       //Debug.Log(sb);

       GameObject prefabAcc = Instantiate(prefabAcquisition);
       prefabAcc.name = "Acquisition" + AcqButton.ToString();
       var name = prefabAcc.gameObject.name;



       var someData = GameObject.Find(name + "UGUIScrollViewContent").GetComponentInChildren<TMP_Text>();
       someData.text = sb.ToString();

       AcqButton++;

       //pd.GetComponent<TMP_Text>().text = sb.ToString();

       //  .GetComponent<TMP_Text>().text = sb.ToString();
       //   var someData = GameObject.Find("SomeData").GetComponent<TMP_Text>();
       //  someData.text = sb.ToString();

   }

   public void showMetaModel(string link)
   {
       List<string> patientInfoTags = new List<string>();
       List<string> patientValues = new List<string>();
       //Get the data 

       var number = 0;
       var data_set = DicomFile.Open(link).Dataset;

       //Debug.Log(data_set);
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

       GameObject patientMetaData = Instantiate(prefabMetaData);
       patientMetaData.name = "MetaData" + metaButton.ToString();

       var name = patientMetaData.gameObject.name;
       var someData = GameObject.Find(name + "UGUIScrollViewContent").GetComponentInChildren<TMP_Text>();
       someData.text = sb.ToString();
       metaButton++;
   }

   public void showPatientModel(string link)
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
       //Debug.Log(sb);

      GameObject patientWindowObj = Instantiate(prefabPatientData);
      patientWindowObj.name = "PatientDemographics" + patientButton.ToString();

      var name = patientWindowObj.gameObject.name;
      var someData = GameObject.Find(name + "UGUIScrollViewContent").GetComponentInChildren<TMP_Text>();
      someData.text = sb.ToString();
      patientButton++;

      //Debug.Log("Patient Button Pressed");
  }
  
public void setImage(Image imageObj, int b)
    {

        int currentImageIndex = b + 1;
        int index = 0;
        bool found = false;
        // int a = getNumber();
        // Debug.Log("11111" + a);
        // Debug.Log("2222" + currentImageIndex);
        string prev = "";
        FileInfo[] fi = newInfo.GetFiles("*dcm");

        // Debug.Log(fi);
        for (int i = 0; i < fi.Length; i++)
        {
            if (currentImageIndex == index)
            {
                FileInfo file = fi[currentImageIndex - 1];
                imageObj.GetComponent<Button>().onClick.RemoveAllListeners();
                imageObj.GetComponent<Button>().onClick.AddListener(() => showNewModel(Path.Combine(Application.streamingAssetsPath, "DicomFiles/") + file.Name, file.Name));

                //  Debug.Log(file.Name);
            }
            index++;
        }
        // Debug.Log(info.GetFiles("*.dcm"));

       
                FileInfo[] infos = info.GetFiles("*.dcm");


                Debug.Log(infos.Length);*/

    /*
            foreach (FileInfo file in info.GetFiles("*.dcm"))
            {
                if (currentImageIndex  == index)
                {
                    Debug.Log(file.FullName);
                    Debug.Log(index);
                    imageObj.GetComponent<Button>().onClick.RemoveAllListeners();
                    imageObj.GetComponent<Button>().onClick.AddListener(() => showNewModel($"Assets/DicomFiles/" + file.Name, file.Name));
                    break;

                }
                else
                {
                    index++;
                }

            }
    */

    /* foreach (FileInfo file in info.GetFiles("*.dcm"))
     {
         if (currentImageIndex == index && getNumber() == index)
         {
             //Debug.Log(file.Name);
           //  Debug.Log(index);
             imageObj.GetComponent<Button>().onClick.AddListener(() => showNewModel($"Assets/DicomFiles/" + file.Name, file.Name));
             found = true;
             break;
         }
         index++;
     }

*/
    /*foreach (FileInfo file in info.GetFiles("*.dcm"))
    {
        if (currentImageIndex == counter_image_Int)
        {
            Debug.Log(file.Name);
            found = true;
            break;

        }



}
public int getNumber()
{
    var imageObj = GameObject.Find("UGUIButtons").GetComponentInChildren<Image>();
    var imgNumber = imageObj.GetComponent<DicomLoaderForUIImageWithDirectorypathBehaviour>().m_ImageIndex;
    return imgNumber;
}
}*/
    // Update is called once per frame


    public void ValueChangeCheck()

    {
        //Debug.Log("Value is changed");
        //var value = slider.GetComponent<PinchSlider>().SliderValue;
        //int v = (int)Math.Round(value * 100);
        var imageObj = GameObject.Find("UGUIButtons").GetComponentInChildren<Image>();

       //imageObj.GetComponent<AddSliderData>().m_ImageIndex = v;
       //AddSliderData n = imageObj.GetComponent<AddSliderData>();
     // try { n.testClicked(); }
     // catch { Debug.Log("erroe Here"); }



        //setImage(imageObj, v);


    }

    public void setImage(Image imageObj, int b)
    {

        int currentImageIndex = b + 1;
        int index = 0;
        bool found = false;
        // int a = getNumber();
        // Debug.Log("11111" + a);
        // Debug.Log("2222" + currentImageIndex);
        string prev = "";
        FileInfo[] fi = newInfo.GetFiles("*dcm");

        // Debug.Log(fi);
        for (int i = 0; i < fi.Length; i++)
        {
            if (currentImageIndex == index)
            {
                FileInfo file = fi[currentImageIndex - 1];
                imageObj.GetComponent<Button>().onClick.RemoveAllListeners();
              //  imageObj.GetComponent<Button>().onClick.AddListener(() => showNewModel(Path.Combine(Application.streamingAssetsPath, "DicomFiles/") + file.Name, file.Name));

                //  Debug.Log(file.Name);
            }
            index++;
        }
    }

    /*public void showNewModel(string link, string name)
    {

        Debug.Log("This ran");

        GameObject newObj = Instantiate(prefabWindow);
        newObj.name = "Window" + windowNo.ToString(); ;

        var imgObj = newObj.gameObject.GetComponentInChildren<Image>();
        var titleObj = newObj.gameObject.GetComponentInChildren<TMP_Text>();
        titleObj.text = name;
        imgObj.GetComponent<DicomLoaderForUIImageBehaviour>().m_Dicom_filename = link;



        var n = newObj.gameObject.name;
        GameObject.Find(n + "MetaData").GetComponent<ButtonConfigHelper>().OnClick.AddListener(() => showMetaModel(link));
        GameObject.Find(n + "AcquisitionData").GetComponent<ButtonConfigHelper>().OnClick.AddListener(() => showAcquisitionModel(link));
        GameObject.Find(n + "PatientDemographics").GetComponent<ButtonConfigHelper>().OnClick.AddListener(() => showPatientModel(link));

        windowNo++;
        //newButton.GetComponent<DicomLoaderForUIImageBehaviour>().m_Dicom_filename = $"Assets/DicomFiles/" + v;
        // GameObject.Find("PatientDemographics").GetComponent<ButtonConfigHelper>().OnClick.AddListener(() => showPatientModel(link));
        // GameObject.Find("AcquisitionData").GetComponent<ButtonConfigHelper>().OnClick.AddListener(() => showAcquisitionModel(link));
        //  GameObject.Find("MetaData").GetComponent<ButtonConfigHelper>().OnClick.AddListener(() => showMetaModel(link));
        //Debug.Log(btnPatientData);

    }*/
}
