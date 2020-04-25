using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RoomListing : MonoBehaviour, IPointerClickHandler
{
    public ChatController chatController;

    [SerializeField]
    public Text _text;

    private int clickCount;

    public RoomInfo RoomInfo {get; private set;}

    private void Awake() {
        chatController = GameObject.FindObjectOfType<ChatController>();        
        clickCount = 0;

    }


    public void SetRoomInfo(RoomInfo roomInfo) {
        RoomInfo = roomInfo;
        _text.text = roomInfo.Name + ": " + roomInfo.PlayerCount;   
    }

    public virtual void OnPointerClick(PointerEventData eventData)
    {
        if (clickCount == 0) {
            clickCount++;
            chatController.SetTeamName(RoomInfo.Name);
            PhotonNetwork.JoinRoom(RoomInfo.Name);
        } else {
            if (eventData.clickCount >= 2) {
                Debug.Log("Please be patient, room is loading...");
            }
        }
    }
 

}
