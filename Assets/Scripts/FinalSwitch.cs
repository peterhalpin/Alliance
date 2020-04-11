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
      
        if(colliders.Count == 2){
            if(PhotonNetwork.IsMasterClient){
                myPhotonView.RPC("LevelCountUpdate", RpcTarget.All, true);
            }
            SceneManager.LoadScene(sceneName: "Level2");
            Debug.Log("shit again");
            // myPhotonView.RPC("LoadNextLevel", RpcTarget.All, "Level2");
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

    // [PunRPC]
    // private void LoadNextLevel(string levelName) {
    //     SceneManager.LoadScene(sceneName: levelName);

    // }


}


