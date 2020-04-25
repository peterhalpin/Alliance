using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KeyboardShortcuts : MonoBehaviourPunCallbacks
{
    private PhotonView myPhotonView;
    private int tabQCount;
    // private int tabMCount;
    private bool tabMBool;


    private InfoObject infoObject;
    private ChatHandler chatHandler; 
    private TimerController timerController;
    private GameData gameData;
    public bool isInPlayerMap; // for checking if the player is using the full map or the player map

     

    // [SerializeField]
    private GameObject main_camera;
    

    private void Awake() {
        infoObject = GameObject.FindObjectOfType<InfoObject>();   
        chatHandler = GameObject.FindObjectOfType<ChatHandler>();   
        timerController = GameObject.FindObjectOfType<TimerController>();
        gameData = GameObject.FindObjectOfType<GameData>();
        // main_camera = GameObject.FindWithTag("MainCamera").GetComponentInChildren<Camera>();
        main_camera = GameObject.FindWithTag("MainCamera");
        // main_camera = Resources.FindObjectsOfTypeAll(typeof(GameObject)).FindWithTag("MainCamera");


        tabQCount = 0; 
        tabMBool = false;
        isInPlayerMap = true;;
        // tabMCount = 0;


        // name =  this.name;

    }

    private void Start() {
        // print(name);
        main_camera.SetActive(false);
        if(PhotonNetwork.IsConnected) {
            myPhotonView = GetComponent<PhotonView>();
            if (!myPhotonView.IsMine) {
                this.GetComponentInChildren<Camera>().enabled = false;
            }
        }
    }

    private void Update() {
        // if the player presses the shift and escape key, then we will be brought back to the main menu scene
        if (PhotonNetwork.IsConnected && PhotonNetwork.InRoom) {
            if (Input.GetKey("tab") && Input.GetKeyUp("q")){ //input.GetKey("a");
                // tabQCount = 1;                   
                infoObject.GoToMainMenu();
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

        if(Input.GetKey("tab") && Input.GetKeyUp("m")) {
            if(!tabMBool) {
                tabMBool = true;
                main_camera.SetActive(true);
                this.GetComponentInChildren<Camera>().enabled = false;
                isInPlayerMap = false;
            } else {
                tabMBool = false;
                main_camera.SetActive(false);
                this.GetComponentInChildren<Camera>().enabled = true;
                isInPlayerMap = true;
            }
            // main_camera.SetActive(true);   
        }
    }

    // private 


}


