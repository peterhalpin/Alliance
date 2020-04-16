using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LeaveGame : MonoBehaviourPunCallbacks
{
    private PhotonView myPhotonView;
    private InfoObject infoObject;
    private int shiftEscCount;

    private void Awake() {
        infoObject = GameObject.FindObjectOfType<InfoObject>();       
        shiftEscCount = 0; 
    }

    private void Update() {
        if(Input.GetKey("q") && Input.GetKey("left shift")) {
            print("shitface");
        }
        // if the player presses the shift and escape key, then we will be brought back to the main menu scene
        if (PhotonNetwork.IsConnected && PhotonNetwork.InRoom) {
            if (Input.GetKey("left shift") && Input.GetKey("q") && shiftEscCount == 0){ //input.GetKey("a");
                shiftEscCount = 1;
                // SceneManager.MoveGameObjectToScene(infoObject.gameObject, SceneManager.GetActiveScene());
                infoObject.GoToMainMenu();
                Destroy(infoObject.gameObject);
                SceneManager.LoadScene(0);
                PhotonNetwork.LeaveRoom(true);
                Debug.Log("Leaving the photon room");
            }
        }   
    }

}


