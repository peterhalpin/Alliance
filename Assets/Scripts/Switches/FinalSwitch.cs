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

    private bool testing;

    private void Awake() {
        try {
            infoObject = GameObject.FindObjectOfType<InfoObject>();
            myPhotonView = GetComponent<PhotonView>();
            testing = false;
        } catch {
            Debug.Log("We must be testing");
            testing = true;
        }
        
    }

    void OnTriggerEnter2D(Collider2D player)
    {
        if(!colliders.Contains(player)){
            colliders.Add(player);
        }
        // changes based on what level we're currently on
        // will need to add more as more levels are added
        if(colliders.Count == 4) {
            if(!testing) {
                if(infoObject.GetLevel() == 1) { //go to level 2
                    colliders.Clear();
                    infoObject.UpdateLevel(true);
                    UpdateLevel(3);
                } else if (infoObject.GetLevel() == 2) { // go to level 3
                    colliders.Clear();
                    infoObject.UpdateLevel(true);
                    UpdateLevel(4);
                } else if (infoObject.GetLevel() == 3) { // go to level 4
                    colliders.Clear();
                    infoObject.UpdateLevel(true);
                    UpdateLevel(5);
                } else if (infoObject.GetLevel() == 4) { // end scene
                    colliders.Clear();
                    infoObject.UpdateLevel(true);
                    UpdateLevel(6);
                } else {
                    Debug.LogError("Level is possibly wrong, possibly error in incrementing or decrementing the infoOjbect level attribute.");
                }
            } else {
                Scene currentScene = SceneManager.GetActiveScene();
                string sceneName = currentScene.name;
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
    }

    private void UpdateLevel(int sceneIndexNum) {
        if (!PhotonNetwork.IsMasterClient) {
            return;
        }
        PhotonNetwork.LoadLevel(sceneIndexNum);
    }
}


