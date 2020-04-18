using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LeaveGame : MonoBehaviourPunCallbacks
{
    private PhotonView myPhotonView;
    private int shiftEscCount;

    private InfoObject infoObject;
    private ChatHandler chatHandler; 


    private void Awake() {
        infoObject = GameObject.FindObjectOfType<InfoObject>();   
        chatHandler = GameObject.FindObjectOfType<ChatHandler>();   
        shiftEscCount = 0; 
    }

    private void Update() {
        // if the player presses the shift and escape key, then we will be brought back to the main menu scene
        if (PhotonNetwork.IsConnected && PhotonNetwork.InRoom) {
            if (Input.GetKey("left shift") && Input.GetKey("q") && shiftEscCount == 0){ //input.GetKey("a");
                shiftEscCount = 1;
                infoObject.GoToMainMenu();
                chatHandler.DisconnectFromChat();
                Destroy(infoObject.gameObject);
                Destroy(chatHandler.gameObject);
                SceneManager.LoadScene(0);
                PhotonNetwork.LeaveRoom(true);
                Debug.Log("Leaving the photon room");
            }
        }   
    }

}


