using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KDicom;

public class DicomVolumeLoaderBehaviour : MonoBehaviour {
    [SerializeField]
    string m_DicomDirectoryPath;

    IDicomVolume m_DicomVolume;
    public IDicomVolume DicomVolume { get { return m_DicomVolume; } }

    // Use this for initialization
    void Start()
    {

        // Load Dicom files
        var volumes = DicomLoader.LoadDicomVolumes(m_DicomDirectoryPath);
        int depthmax = 0;
        foreach (var vol in volumes)
        {
            if (vol.Depth > depthmax)
            {
                depthmax = vol.Depth;
                m_DicomVolume = vol;
            }
        }
        // Dispose unused volumes
        foreach (var vol in volumes)
        {
            if (vol != m_DicomVolume)
            {
                vol.Dispose();
            }
        }

        if (m_DicomVolume == null)
            return;

        // Initialize MainCamera position
        {
            // Culclate volume center position in Unity coordinate
            var volume_centerpos = m_DicomVolume.ToDicomImageAxial(m_DicomVolume.Depth / 2).ImageCenterPosition * 0.001f;
            Camera.main.transform.rotation = Quaternion.Euler(-65.0f, -80.0f, 80.0f);
            Camera.main.transform.position = volume_centerpos + new Vector3(0.08f, -0.35f, -0.05f);
            Camera.main.GetComponent<CameraOperationBehaviour>().CenterPos = volume_centerpos;
        }
    }

    private void OnDestroy()
    {
        if (m_DicomVolume != null)
        {
            m_DicomVolume.Dispose();
            System.GC.Collect();
            Resources.UnloadUnusedAssets();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
