using Dicom.Imaging;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class viewDCM : MonoBehaviour
{

    public string m_Dicom_filename;

    // Start is called before the first frame update
    void Start()
    {

        string a = Path.Combine(Application.streamingAssetsPath);
        string b = a + "/"+m_Dicom_filename;


        Debug.Log("Test worki");
        var image = new DicomImage(b);
        Debug.Log(image);
        var tex = image.RenderImage().AsTexture2D();

    
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

}
