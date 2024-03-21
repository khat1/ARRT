using Photon.Pun;
using System;
using UnityEngine;

namespace MRTK.Tutorials.MultiUserCapabilities
{
    public class GenericNetSync : MonoBehaviourPun, IPunObservable
    {
        [SerializeField] private bool isUser = default;

        public Camera mainCamera;

        private Vector3 networkLocalPosition;
        private Quaternion networkLocalRotation;

        private Vector3 startingLocalPosition;
        private Quaternion startingLocalRotation;

        void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {
                stream.SendNext(transform.localPosition);
                stream.SendNext(transform.localRotation);
            }
            else
            {
                networkLocalPosition = (Vector3) stream.ReceiveNext();
                networkLocalRotation = (Quaternion) stream.ReceiveNext();
            }
        }

        private void Start()
        {
            mainCamera = Camera.main;


            if (isUser)
            {
                if (TableAnchor.Instance != null) transform.parent = FindObjectOfType<TableAnchor>().transform;

                if (photonView.IsMine) GenericNetworkManager.Instance.localUser = photonView;
            }

            var trans = transform;
            startingLocalPosition = trans.localPosition;
            startingLocalRotation = trans.localRotation;

            networkLocalPosition = startingLocalPosition;
            networkLocalRotation = startingLocalRotation;
        }

        // private void FixedUpdate()
        private void Update()
        {
            try {

                if (mainCamera != null)
                {
                    if (!photonView.IsMine)
                    {
                        var trans = transform;
                        trans.localPosition = networkLocalPosition;
                        trans.localRotation = networkLocalRotation;
                    }

                    if (photonView.IsMine && isUser)
                    {
                        var trans = transform;
                        var mainCameraTransform = mainCamera.transform;
                        trans.position = mainCameraTransform.position;
                        trans.rotation = mainCameraTransform.rotation;
                    }
                }
                else
                {

                }
            }
            catch (Exception e) {
                Debug.Log("ERROR IN UPDATE " + e);
            }
        }
    }
}
