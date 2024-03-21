using UnityEngine;
using System.Collections.Generic;

namespace UnityVolumeRendering
{
    public enum CrossSectionType
    {
        Plane = 1,
        BoxInclusive = 2,
        BoxExclusive = 3,
        SphereInclusive = 4,
        SphereExclusive = 5
    }

    public struct CrossSectionData
    {
        public CrossSectionType type;
        public Matrix4x4 matrix;
    }

    [ExecuteInEditMode]
    public class CrossSectionManager : MonoBehaviour
    {
        private const int MAX_CROSS_SECTIONS = 8;

        private VolumeRenderedObject targetObject;
        private List<CrossSectionObject> crossSectionObjects = new List<CrossSectionObject>();
        private Matrix4x4[] crossSectionMatrices = new Matrix4x4[MAX_CROSS_SECTIONS];
        private float[] crossSectionTypes = new float[MAX_CROSS_SECTIONS];
        private CrossSectionData[] crossSectionData = new CrossSectionData[MAX_CROSS_SECTIONS];
        private Vector3[] previousCrossSectionPositions = new Vector3[MAX_CROSS_SECTIONS];

        public CrossSectionData[] GetCrossSectionData()
        {
            return crossSectionData;
        }

        public void AddCrossSectionObject(CrossSectionObject crossSectionObject)
        {
            crossSectionObjects.Add(crossSectionObject);
        }

        public void RemoveCrossSectionObject(CrossSectionObject crossSectionObject)
        {
            crossSectionObjects.Remove(crossSectionObject);
        }

        private void Awake()
        {
            targetObject = GetComponent<VolumeRenderedObject>();
        }

        private void Update()
        {
            if (targetObject == null)
                return;

            Material mat = targetObject.meshRenderer.sharedMaterial;

            bool crossSectionPositionsChanged = UpdateCrossSectionPositions();

            if (crossSectionPositionsChanged)
            {
                int numCrossSections = Mathf.Min(crossSectionObjects.Count, MAX_CROSS_SECTIONS);

                for (int i = 0; i < numCrossSections; i++)
                {
                    CrossSectionObject crossSectionObject = crossSectionObjects[i];
                    crossSectionMatrices[i] = crossSectionObject.GetMatrix();
                    crossSectionTypes[i] = (int)crossSectionObject.GetCrossSectionType();
                    crossSectionData[i] = new CrossSectionData() { type = crossSectionObject.GetCrossSectionType(), matrix = crossSectionMatrices[i] };
                }

                mat.EnableKeyword("CROSS_SECTION_ON");
                mat.SetMatrixArray("_CrossSectionMatrices", crossSectionMatrices);
                mat.SetFloatArray("_CrossSectionTypes", crossSectionTypes);
                mat.SetInt("_NumCrossSections", numCrossSections);
            }
            else
            {
                mat.DisableKeyword("CROSS_SECTION_ON");
            }
        }

        private bool UpdateCrossSectionPositions()
        {
            bool positionsChanged = false;

            for (int i = 0; i < crossSectionObjects.Count; i++)
            {
                Vector3 currentPos = (crossSectionObjects[i] as MonoBehaviour).transform.position;

                if (currentPos != previousCrossSectionPositions[i])
                {
                    positionsChanged = true;
                    previousCrossSectionPositions[i] = currentPos;
                }
            }

            return positionsChanged;
        }
    }
}