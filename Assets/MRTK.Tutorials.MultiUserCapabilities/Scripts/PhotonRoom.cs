using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System.Linq;

using UnityEngine;

namespace MRTK.Tutorials.MultiUserCapabilities
{
    public class PhotonRoom : MonoBehaviourPunCallbacks, IInRoomCallbacks
    {
        public static PhotonRoom Room;

        [SerializeField] private GameObject photonUserPrefab = default;
        //   [SerializeField] private GameObject RightHandPointer = default;

        [SerializeField] private GameObject ButtonSimulation = default;
        //   [SerializeField] private GameObject RightHandPointer = default;
        [SerializeField] private GameObject ButtonSituation = default;
        [SerializeField] private GameObject mainWindowforSimulation = default;
        [SerializeField] private GameObject mainWindowforSituation = default;



        [SerializeField] private GameObject bed = default;
        [SerializeField] private GameObject Xray = default;
        [SerializeField] private GameObject MedCart = default;

        [SerializeField] private GameObject VolumeData = default;
        // [SerializeField] private GameObject mainWindow = default;
        //[SerializeField] private GameObject slider = default;
        //[SerializeField] private Transform roverExplorerLocation = default;

        [SerializeField] public PhotonView pv;
        [SerializeField] public Player[] photonPlayers;
        [SerializeField] public int playersInRoom;
        [SerializeField] public int myNumberInRoom;

        // private GameObject module;
        // private Vector3 moduleLocation = Vector3.zero;

        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            base.OnPlayerEnteredRoom(newPlayer);
            photonPlayers = PhotonNetwork.PlayerList;
            playersInRoom++;
        }

        private void Awake()
        {
            if (Room == null)
            {
                Room = this;
            }
            else
            {
                if (Room != this)
                {
                    Destroy(Room.gameObject);
                    Room = this;
                }
            }
        }

        public override void OnEnable()
        {
            base.OnEnable();
            PhotonNetwork.AddCallbackTarget(this);
        }

        public override void OnDisable()
        {
            base.OnDisable();
            PhotonNetwork.RemoveCallbackTarget(this);
        }

        private void Start()
        {
             pv = GetComponent<PhotonView>();

            // Allow prefabs not in a Resources folder
            if (PhotonNetwork.PrefabPool is DefaultPool pool)
            {
                if (photonUserPrefab != null) pool.ResourceCache.Add(photonUserPrefab.name, photonUserPrefab);
               // if (RightHandPointer != null) pool.ResourceCache.Add(RightHandPointer.name, RightHandPointer);


                if (ButtonSimulation != null) pool.ResourceCache.Add(ButtonSimulation.name, ButtonSimulation);

                if (ButtonSituation != null) pool.ResourceCache.Add(ButtonSituation.name, ButtonSituation);
                if (mainWindowforSimulation != null) pool.ResourceCache.Add(mainWindowforSimulation.name, mainWindowforSimulation);

                if (bed != null) pool.ResourceCache.Add(bed.name, bed);

                if (Xray != null) pool.ResourceCache.Add(Xray.name, Xray);

                if (MedCart != null) pool.ResourceCache.Add(MedCart.name, MedCart);

                if(VolumeData!= null) pool.ResourceCache.Add(VolumeData.name, VolumeData);
                //  if (mainWindowforSituation != null) pool.ResourceCache.Add(mainWindowforSituation.name, mainWindowforSituation);

                //  if (slider != null) pool.ResourceCache.Add(slider.name, slider);
            }
        }

        public override void OnJoinedRoom()
        {
            base.OnJoinedRoom();

            photonPlayers = PhotonNetwork.PlayerList;
            playersInRoom = photonPlayers.Length;
            myNumberInRoom = playersInRoom;
            PhotonNetwork.NickName = myNumberInRoom.ToString();

            StartGame();
        }

        private void StartGame()
        {
            CreatPlayer();

            if (!PhotonNetwork.IsMasterClient) return;

            Debug.Log("StartGamePhoton");

            if (TableAnchor.Instance != null) CreateInteractableObjects();
        }

        private void CreatPlayer()
        {
            var player = PhotonNetwork.Instantiate(photonUserPrefab.name, Vector3.zero, Quaternion.identity);

           // var pointer = PhotonNetwork.Instantiate(RightHandPointer.name,Vector3.zero, Quaternion.identity);
        }

        private void CreateInteractableObjects()
        {
            Debug.Log("rUNNING");
           // var position = ButtonSimulation.position;
            // var positionOnTopOfSurface = new Vector3(position.x, position.y + roverExplorerLocation.localScale.y / 2,
            //  position.z);

          //  var mainWindowPosition = ButtonSimulation.transform.position;
            

            
           // var positionSlider = slider.transform.position;

            var go = PhotonNetwork.Instantiate(ButtonSimulation.name, ButtonSimulation.transform.position,
                ButtonSimulation.transform.rotation);

            var go1 = PhotonNetwork.Instantiate(ButtonSituation.name, ButtonSituation.transform.position,
                ButtonSituation.transform.rotation);
           
            var go2 = PhotonNetwork.Instantiate(mainWindowforSimulation.name, mainWindowforSimulation.transform.position,
               mainWindowforSimulation.transform.rotation);

            var go3 = PhotonNetwork.Instantiate(bed.name, bed.transform.position,
              bed.transform.rotation);

            var go4 = PhotonNetwork.Instantiate(Xray.name, Xray.transform.position,
              Xray.transform.rotation);

           // var go5 = PhotonNetwork.Instantiate(MedCart.name, MedCart.transform.position,
             // MedCart.transform.rotation);


            var go6 = PhotonNetwork.Instantiate(VolumeData.name, VolumeData.transform.position,
              VolumeData.transform.rotation);

            //  var go3 = PhotonNetwork.Instantiate(mainWindowforSituation.name, mainWindowforSituation.transform.position,
            // mainWindowforSituation.transform.rotation);

            //   var goSlider = PhotonNetwork.Instantiate(slider.name, slider.transform.position,
            //        slider.transform.rotation);


            // Set 'goSlider' as a child of 'go' (mainWindow)
            // pv.RPC("Rpc_SetModuleParent", RpcTarget.AllBuffered);
            PhotonView photonView = PhotonView.Get(this);
            photonView.RPC("HideAllModels", RpcTarget.AllBuffered);

        }

        // private void CreateMainLunarModule()
        // {
        //     module = PhotonNetwork.Instantiate(mainWindow.name, Vector3.zero, Quaternion.identity);
        //     pv.RPC("Rpc_SetModuleParent", RpcTarget.AllBuffered);
        // }
        //
         /*[PunRPC]
         private void Rpc_SetModuleParent()
         {
            Debug.Log("Rpc_SetModuleParent- RPC Called");

            slider.transform.parent = mainWindow.transform;

           *//* module.transform.parent = TableAnchor.Instance.transform;
           module.transform.localPosition = moduleLocation;*//*
        }*/
    

    [PunRPC]

    public void HideAllModels()
    {

        Debug.Log("Hide AllModels except Buttons");
        FindGameObjectsAll("MainSlate-Simulation(Clone)").SetActive(false);
        FindGameObjectsAll("XRay(Clone)").SetActive(false);
        FindGameObjectsAll("LINAC(Clone)").SetActive(false);
       // FindGameObjectsAll("MedCart(Clone)").SetActive(false);
        FindGameObjectsAll("VolumeData(Clone)").SetActive(false);

        }



    public static GameObject FindGameObjectsAll(string name) => Resources.FindObjectsOfTypeAll<GameObject>().First(x => x.name == name);

    }
}
