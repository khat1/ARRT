using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class SynchronizedSlider : MonoBehaviourPun, IPunObservable
{
    public Slider slider;
    private float syncedSliderValue;

    private void Start()
    {
      //  slider = GetComponent<Slider>();

    }

    private void Update()
    {
        if (photonView.IsMine)
        {
            // Handle slider value changes by the local player.
            float newValue = slider.value;
            if (newValue != syncedSliderValue)
            {
                photonView.RPC("UpdateSliderValue", RpcTarget.AllBuffered, newValue);
                syncedSliderValue = newValue;
            }
        }
    }

    [PunRPC]
    private void UpdateSliderValue(float newValue)
    {
        // Update the slider value for all players.
        slider.value = newValue;
        syncedSliderValue = newValue;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // Sending data: Send the slider value.
            stream.SendNext(slider.value);
        }
        else
        {
            // Receiving data: Update the slider value.
            float receivedValue = (float)stream.ReceiveNext();
            slider.value = receivedValue;
            syncedSliderValue = receivedValue;
        }
    }
}