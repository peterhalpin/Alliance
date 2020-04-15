using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RoomListing : MonoBehaviour, IPointerClickHandler
{

    [SerializeField]
    private Text _text;

    private int clickCount;

    public RoomInfo RoomInfo {get; private set;}

    private void Awake() {
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
            PhotonNetwork.JoinRoom(RoomInfo.Name);
        } else {
            if (eventData.clickCount >= 2) {
                Debug.Log("Please be patient, room is loading...");
            }
        }
    }
 

}
