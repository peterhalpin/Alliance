using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;


public class LobbyController : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private Text _text;
    [SerializeField]
    private GameObject createOrJoinRoomCanvas; //button used for creating and joining a game.

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        createOrJoinRoomCanvas.SetActive(true);
    }

    private void Update() {
        _text.text = "Players looking for a room: " + PhotonNetwork.CountOfPlayersOnMaster;        
    }

    public void DelayCancel() //Paired to the cancel button. Used to stop looking for a room to join.
    {
        PhotonNetwork.LeaveRoom();
    }


}
