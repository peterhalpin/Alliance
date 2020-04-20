using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalSwitch : MonoBehaviourPunCallbacks
{
    private List<Collider2D> colliders = new List<Collider2D>();
    private InfoObject infoObject;
    private PhotonView myPhotonView;

    private GameData gameData;
    private TimerController timerController;
    private Scene currentScene;
    string sceneName;

    private bool testing;

    private void Awake() {
        currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;
        try {
            testing = false;
            infoObject = GameObject.FindObjectOfType<InfoObject>();
            myPhotonView = GetComponent<PhotonView>();
        //DATA COLLECTION CODE-------------------------------------------------------------------------------------------------------------------------------------
            gameData = GameObject.FindObjectOfType<GameData>();
            timerController = GameObject.FindObjectOfType<TimerController>();
        //DATA COLLECTION CODE-------------------------------------------------------------------------------------------------------------------------------------
        } catch {
            Debug.Log("We must be testing");
            testing = true;
        }

        
    }

    void OnTriggerEnter2D(Collider2D player)
    {
        //DATA COLLECTION CODE-------------------------------------------------------------------------------------------------------------------------------------
        gameData.ActivateSwitchTime(currentScene.name + " FinalSwitch pressed by " + player.name, timerController.GetTime());
        gameData.GameInteraction(currentScene.name + " Player " + player.name + " stepped ON the FINAL switch", timerController.GetTime());
        //DATA COLLECTION CODE-------------------------------------------------------------------------------------------------------------------------------------
        if(!colliders.Contains(player)){
            colliders.Add(player);
        }
        // changes based on what level we're currently on
        // will need to add more as more levels are added
        if(colliders.Count == 2) {
            if(!testing) {
                //DATA COLLECTION CODE-------------------------------------------------------------------------------------------------------------------------------------
                gameData.FinishLevelTime(currentScene.name + " FinalSwitch pressed by " + player.name, timerController.GetTime());
                gameData.GameInteraction(currentScene.name + " Player " + player.name + " stepped ON the FINAL switch", timerController.GetTime());
                 //DATA COLLECTION CODE-------------------------------------------------------------------------------------------------------------------------------------
                // if(infoObject.GetLevel() == 1) { //go to level 2
                if(sceneName == "TutorialLevel") { //go to level 2
                    colliders.Clear();
                    infoObject.UpdateLevel(true);
                    UpdateLevel(3);
                // } else if (infoObject.GetLevel() == 2) { // go to level 3
                } else if (sceneName == "Level2") { // go to level 3
                    colliders.Clear();
                    infoObject.UpdateLevel(true);
                    UpdateLevel(4);
                // } else if (infoObject.GetLevel() == 3) { // go to level 4
                } else if (sceneName == "Level3") { // go to level 4
                    colliders.Clear();
                    infoObject.UpdateLevel(true);
                    UpdateLevel(5);
                // } else if (infoObject.GetLevel() == 4) { // end scene
                } else if (sceneName == "Level4") { // end scene
                    colliders.Clear();
                    infoObject.UpdateLevel(true);
                    UpdateLevel(6);
                } else {
                    Debug.LogError("Level is possibly wrong, possibly error in incrementing or decrementing the infoOjbect level attribute.");
                }
            } else {
                if(sceneName == "TutorialLevel") { //go to level 2
                    SceneManager.LoadScene("Level2");
                } else if (sceneName == "Level2") { // go to level 3
                    SceneManager.LoadScene("Level3");
                } else if (sceneName == "Level3") { // go to level 4
                    SceneManager.LoadScene("Level4");
                } else if (sceneName == "Level4") { // end scene
                    SceneManager.LoadScene("EndScene");
                } else {
                    Debug.LogError("Error loading the next scence or trouble with pressing the final switch.");
                }
            }
        }
    }

    void OnTriggerExit2D(Collider2D player){
        colliders.Remove(player);
        //DATA COLLECTION CODE-------------------------------------------------------------------------------------------------------------------------------------
        gameData.DeactivateSwitchTime(currentScene.name + " FinalSwitch left by " + player.name, timerController.GetTime());
        gameData.GameInteraction(currentScene.name + " Player " + player.name + " got OFF the FINAL switch", timerController.GetTime());
        //DATA COLLECTION CODE-------------------------------------------------------------------------------------------------------------------------------------
    }

    private void UpdateLevel(int sceneIndexNum) {
        if (!PhotonNetwork.IsMasterClient) {
            return;
        }
        PhotonNetwork.LoadLevel(sceneIndexNum);
    }
}


