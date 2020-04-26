using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KeyboardShortcuts : MonoBehaviourPunCallbacks
{ 
    private InfoObject infoObject;
    private ChatHandler chatHandler; 
    private TimerController timerController;
    private GameData gameData;
    public bool isInPlayerMap; // for checking if the player is using the full map or the player map

    private void Awake() {
        infoObject = GameObject.FindObjectOfType<InfoObject>();   
        chatHandler = GameObject.FindObjectOfType<ChatHandler>();   
        timerController = GameObject.FindObjectOfType<TimerController>();
        gameData = GameObject.FindObjectOfType<GameData>();
        isInPlayerMap = true;
    }

    private void Start() {
        if(PhotonNetwork.IsConnected) {
            if(!photonView.IsMine) {
                // this is so that players only see from their character's cameras
                // the overall map camera is also active but you can only see it whenever your player's camera is inactive
                this.GetComponentInChildren<Camera>().enabled = false;
            }
        }
    }

    private void Update() {
        // if the player presses the tab and Q key, then we will be brought back to the main menu scene
        // only gets called if player is playing online, not if testing via scene
        if (PhotonNetwork.IsConnected && PhotonNetwork.InRoom) {
            if (Input.GetKey("tab") && Input.GetKeyUp("q")){
                chatHandler.DisconnectFromChat();
                Destroy(infoObject.gameObject);
                Destroy(chatHandler.gameObject);
                Destroy(timerController.gameObject);
                Destroy(gameData.gameObject);
                SceneManager.LoadScene(0);
                PhotonNetwork.LeaveRoom(true);
                Debug.Log("Leaving the photon room");
            }
        }   

        // this is for making the player zoom in and out of the map, so that they can view their map or the full screen
        // if it's in full screen though then they can't move around, that is handled in each character's script though
        if(Input.GetKey("tab") && Input.GetKeyUp("m")) {
            if(isInPlayerMap) {
                // if not online then photonView.IsMine will fail but the second will work, the second is meant for testing purposes
                if(photonView.IsMine || !PhotonNetwork.IsConnected)
                    this.GetComponentInChildren<Camera>().enabled = false;
                isInPlayerMap = false;
            } else {
                if(photonView.IsMine || !PhotonNetwork.IsConnected)
                    this.GetComponentInChildren<Camera>().enabled = true;
                isInPlayerMap = true;
            }
        }
    }




}


