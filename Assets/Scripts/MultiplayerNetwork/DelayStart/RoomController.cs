using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomController : MonoBehaviourPunCallbacks
{
    [SerializeField]
    public int waitingRoomSceneIndex; //Number for the build index to the multiplay scene.

    public override void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }

    public override void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }

    public override void OnJoinedRoom() // Callback function for whenn we successfully create or join a room
    {
        Debug.Log("joined waiting room");
        SceneManager.LoadScene(waitingRoomSceneIndex);
    }
}
