using Dicom.Printing;
using Microsoft.MixedReality.Toolkit.WindowsDevicePortal;
using Photon.Pun;
using Photon.Pun.Demo.Cockpit;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using FileInfo = System.IO.FileInfo;

public class testFromAnotherFile : MonoBehaviour
{
    DirectoryInfo newInfo = new DirectoryInfo(Path.Combine(Application.streamingAssetsPath, "DicomFiles"));
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [PunRPC]
    public void setImage1(int b)
    {
        int currentImageIndex = b + 1;
        int index = 0;
        bool found = false;

        string prev = "";

        FileInfo[] fi = newInfo.GetFiles("*dcm");



        for (int i = 0; i < fi.Length; i++)
        {
            if (currentImageIndex == index)
            {
                FileInfo file = fi[currentImageIndex - 1];
                // imageObj.GetComponent<Button>().onClick.RemoveAllListeners();
                //imageObj.GetComponent<Button>().onClick.AddListener(() => showNewModel(Path.Combine(Application.streamingAssetsPath, "DicomFiles/") + file.Name, file.Name));

                Debug.Log(file.Name + "hereeee");
            }
            index++;
        }
    }

    [PunRPC]
    public void setImage(int b)
    {
        int currentImageIndex = b + 1;
        int index = 0;

        string directoryPath = Path.Combine(Application.streamingAssetsPath, "DicomFiles"); // Path to your directory
        DirectoryInfo newInfo = new DirectoryInfo(directoryPath);

        if (!newInfo.Exists)
        {
            Debug.LogError("Directory does not exist: " + directoryPath);
            return;
        }

        FileInfo[] fi = newInfo.GetFiles("*.dcm");

        foreach (FileInfo file in fi)
        {
            if (currentImageIndex == index)
            {
                // You can now perform actions with the file, for example, log its name
                //Debug.Log(file.Name + " hereeee");
               // return;

                // If you want to call a method with the file path and name, you can do it like this:
                // showNewModel(Path.Combine(directoryPath, file.Name), file.Name);
            }
            index++;
        }
    }
}
