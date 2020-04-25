using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomListingMenu : MonoBehaviourPunCallbacks
{
    private List<RoomListing> _listings = new List<RoomListing>();    
    [SerializeField]
    public Transform _content;
    [SerializeField]
    public RoomListing _roomListing;

    public override void OnRoomListUpdate(List<RoomInfo> roomList) {
        foreach (RoomInfo info in roomList) {
            // Removed from rooms list
            if (info.RemovedFromList || !info.IsOpen) {
                int index = _listings.FindIndex(x => x.RoomInfo.Name == info.Name);
                if (index != -1) {
                    Destroy(_listings[index].gameObject);
                    _listings.RemoveAt(index);
                }
            } 
            //  Added to roooms list
            else {
                int index = _listings.FindIndex(x => x.RoomInfo.Name == info.Name);
                if (index == -1) {
                    RoomListing listing = Instantiate(_roomListing, _content);
                    if (listing != null) {
                        listing.SetRoomInfo(info);
                        _listings.Add(listing);
                    }
                } else {
                    // we can modify a listing here (a listed room)
                    // _listings[index].whatever so .modify or whatever
                }       
            }
        }
    }

    public override void OnJoinedRoom() {
        _content.DestroyChildren();
        _listings.Clear();
    }

}
