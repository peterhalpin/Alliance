using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalSwitch : MonoBehaviourPunCallbacks
{

    private GameData gameData;
    private Scene currentScene;
    private TimerController timerController;
    private List<string> playersOnFinalSwitch; // keeps track on who's the final switch

    private void Awake() {
        playersOnFinalSwitch = new List<string>();
        currentScene = SceneManager.GetActiveScene();
        if(PhotonNetwork.IsConnected) {
            gameData = GameObject.FindObjectOfType<GameData>();
            timerController = GameObject.FindObjectOfType<TimerController>();
        }        
    }

    void OnTriggerEnter2D(Collider2D player) {
        if(PhotonNetwork.IsConnected && gameData != null) {
            //DATA COLLECTION CODE-------------------------------------------------------------------------------------------------------------------------------------
            gameData.ActivateSwitchTime(currentScene.name + " FinalSwitch pressed by " + player.name, timerController.GetTime());
            gameData.GameInteraction(currentScene.name + " Player " + player.name + " stepped ON the FINAL switch", timerController.GetTime());
            //DATA COLLECTION CODE-------------------------------------------------------------------------------------------------------------------------------------
        }
        if(!playersOnFinalSwitch.Contains(player.name))
            playersOnFinalSwitch.Add(player.name);
        // changes based on what level we're currently on
        // will need to add more as more levels are added
        if(playersOnFinalSwitch.Count == 4) {
            // if(!testing) {
            if(PhotonNetwork.IsConnected && gameData != null) {
                //DATA COLLECTION CODE-------------------------------------------------------------------------------------------------------------------------------------
                gameData.FinishLevelTime(currentScene.name + " FinalSwitch pressed by " + player.name, timerController.GetTime());
                gameData.GameInteraction(currentScene.name + " Player " + player.name + " stepped ON the FINAL switch", timerController.GetTime());
                 //DATA COLLECTION CODE-------------------------------------------------------------------------------------------------------------------------------------
            }
            if(currentScene.name == "TutorialLevel") { //go to level 2
                UpdateLevel(3);
            } else if (currentScene.name == "Level2") { // go to level 3
                UpdateLevel(4);
            } else if (currentScene.name == "Level3") { // go to level 4
                UpdateLevel(5);
            } else if (currentScene.name == "Level4") { // end scene
                UpdateLevel(6);
            } else {
                Debug.LogError("Level is possibly wrong, possibly error in incrementing or decrementing the infoOjbect level attribute.");
            }
        }
    }

    void OnTriggerExit2D(Collider2D player){
        if(PhotonNetwork.IsConnected && gameData != null) {
            //DATA COLLECTION CODE-------------------------------------------------------------------------------------------------------------------------------------
            gameData.DeactivateSwitchTime(currentScene.name + " FinalSwitch left by " + player.name, timerController.GetTime());
            gameData.GameInteraction(currentScene.name + " Player " + player.name + " got OFF the FINAL switch", timerController.GetTime());
            //DATA COLLECTION CODE-------------------------------------------------------------------------------------------------------------------------------------
        }
        playersOnFinalSwitch.Remove(player.name);
    }

    private void UpdateLevel(int sceneIndexNum) {
        // if not connected then use scene manager to load next scene, if is then use photon network
        if(!PhotonNetwork.IsConnected)
            SceneManager.LoadScene(sceneIndexNum); // only needs to run this because LoadScene forces all previous AsyncOperations to complete so doesn't finish the rest of the method, , use LoadSceneAsync if this is not what you want
        if (PhotonNetwork.IsMasterClient)
            PhotonNetwork.LoadLevel(sceneIndexNum);
    }
}


