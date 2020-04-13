using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalSwitch : MonoBehaviourPun
{
    private List<Collider2D> colliders = new List<Collider2D>();
    private InfoObject infoObject;
    private PhotonView myPhotonView;

    private void Awake() {
        infoObject = GameObject.FindObjectOfType<InfoObject>();
        myPhotonView = GetComponent<PhotonView>();
    }


    void OnTriggerEnter2D(Collider2D player)
    {
        if(!colliders.Contains(player)){
            colliders.Add(player);
        }
      
        if(colliders.Count == 4){
            if(PhotonNetwork.IsMasterClient){
                myPhotonView.RPC("LevelCountUpdate", RpcTarget.All, true);
            }
            // changes based on what level we're currently on
            // will need to add more as more levels are added
            if(infoObject.GetLevel() == 2) {
                SceneManager.LoadScene(sceneName: "Level2");
            } else if (infoObject.GetLevel() == 3) {
                SceneManager.LoadScene(sceneName: "Level3");
            } else if (infoObject.GetLevel() == 4) {
                SceneManager.LoadScene(sceneName: "Level4");
            } else {
                Debug.LogError("Level is possibly wrong, possibly error in incrementing or decrementing");
            }
        }

    }

    void OnTriggerExit2D(Collider2D player){
        colliders.Remove(player);
    }


    [PunRPC]
    private void LevelCountUpdate(bool goingToNextLevel) {
        // updates on all players what level we're on
        infoObject.UpdateLevel(goingToNextLevel);
    }
    
}


