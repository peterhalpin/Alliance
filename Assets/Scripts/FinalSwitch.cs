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

    private void Awake() {
        infoObject = GameObject.FindObjectOfType<InfoObject>();
        myPhotonView = GetComponent<PhotonView>();
    }

    void OnTriggerEnter2D(Collider2D player)
    {
        if(!colliders.Contains(player)){
            colliders.Add(player);
        }
      
        if(colliders.Count == 4) {
            // changes based on what level we're currently on
            // will need to add more as more levels are added
            if(infoObject.GetLevel() == 1) { //go to level 2
                colliders.Clear();
                infoObject.UpdateLevel(true);
                Debug.Log("Going to level 2!");
                Debug.Log(colliders.Count);
                Debug.Log(infoObject.GetLevel());
                UpdateLevel(3);
            } else if (infoObject.GetLevel() == 2) { // go to level 3
                colliders.Clear();
                infoObject.UpdateLevel(true);
                Debug.Log("Going to level 3!");
                Debug.Log(colliders.Count);
                Debug.Log(infoObject.GetLevel());
                UpdateLevel(4);
            } else if (infoObject.GetLevel() == 3) { // go to level 4
                colliders.Clear();
                infoObject.UpdateLevel(true);
                UpdateLevel(5);
            } else {
                Debug.LogError("Level is possibly wrong, possibly error in incrementing or decrementing");
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


