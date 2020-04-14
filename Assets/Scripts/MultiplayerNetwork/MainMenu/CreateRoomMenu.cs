using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;
using Photon.Pun;

public class CreateRoomMenu : MonoBehaviourPunCallbacks
{

    [SerializeField]
    private Text _roomName;
    [SerializeField]
    public int roomSize; //manually set the number of players in the room at one time.

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public void OnClick_CreateRoom() {

        if (!PhotonNetwork.IsConnected) {
            return;
        }
        // RoomOptions options = new RoomOptions();
        // options.MaxPlayers = 4;
        RoomOptions roomOps = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = (byte)roomSize };
        roomOps.PublishUserId = true;

        PhotonNetwork.JoinOrCreateRoom(_roomName.text, roomOps, TypedLobby.Default);
    }

    public override void OnCreatedRoom() {
        Debug.Log("Created room" + _roomName.text + "successfully.", this);
    }

    public override void OnCreateRoomFailed(short returnCode, string message) {
        Debug.Log("Room created failed: " + message, this);
    }



}
