using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using System;
using System.IO;

public class test1 : MonoBehaviour
{

    [SerializeField]
    string m_Dicom_filename;

    string m_Modality;

    // Use this for initialization
    void Start()
    {
        UpdateImage();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void UpdateImage()

    {
        /**
        string a = Path.Combine(Application.streamingAssetsPath);
        Debug.Log("HERE!!" + a);

        string b = a + "/CTHd0001.dcm";
        Debug.Log("Streaming asset link::" + b.ToString());
        //if (!System.IO.File.Exists(m_Dicom_filename))
        //   return;
        //Debug.Log("The file did not get Found Here!");

        // Load DICOM in UI Image coordinate
        var img = DicomLoader.LoadDicomImage(b.ToString());
        Texture2D tex = new Texture2D(2, 2);
        Debug.Log("img data" + img);

        // Failed to load
        // if (img == null) {
        //    Debug.Log("failed to load " + b.ToString());
        //  return;
        // }

       // img.GetTagInfo(new DicomTag(Tags.Modality), out m_Modality);

        // Set texture to UI Image

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
        } */
    }
       
    }
