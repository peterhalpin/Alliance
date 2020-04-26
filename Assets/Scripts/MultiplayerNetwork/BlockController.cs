using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviourPunCallbacks
{
    private PhotonView myPhotonView;
    private BoxCollider2D[] boxes;
    private AreaEffector2D a;
    private GameObject block;
    private bool logCalled;

    private void Awake() {
        logCalled = false;
        myPhotonView = GetComponent<PhotonView>(); 
    }

    // this object might not be used
    public void PullObjects() {
        block = GameObject.FindGameObjectWithTag("block");
        a = GameObject.FindWithTag("VisionPlayer").GetComponent<AreaEffector2D>();
        boxes = GameObject.FindWithTag("VisionPlayer").GetComponents<BoxCollider2D>();
    }

    public void UpdateBlockStatus(int blockNum, string playerName) {        
        myPhotonView.RPC("PullObjectsForEveryone", RpcTarget.All, playerName);                
        myPhotonView.RPC("UpdateBlockForEveryone", RpcTarget.All, blockNum, playerName);  
    }


    [PunRPC]
    private void PullObjectsForEveryone(string playerName) {
        if(playerName == "green(Clone)") {
            block = GameObject.FindGameObjectWithTag("block");
            a = GameObject.FindWithTag("VisionPlayer").GetComponent<AreaEffector2D>();
            
            boxes = GameObject.FindWithTag("VisionPlayer").GetComponents<BoxCollider2D>();
            if(block != null && a != null && boxes != null && !logCalled) {
                Debug.Log("Is In Game!!!!! Nothing to worry about");
                logCalled = true;
            }
        } else if(playerName == "blek(Clone)") {
            boxes = GameObject.FindWithTag("StrengthPlayer").GetComponents<BoxCollider2D>();
        } else if(playerName == "red(Clone)") {
            boxes = GameObject.FindWithTag("FirePlayer").GetComponents<BoxCollider2D>();
        } else if(playerName == "blue(Clone)") {
            boxes = GameObject.FindWithTag("IcePlayer").GetComponents<BoxCollider2D>();
        } else {
            Debug.Log("something is wrong, can't find prefab");
        }

    }

    [PunRPC]
    private void UpdateBlockForEveryone(int blockNumber, string playerName) {

        if(playerName == "green(Clone)") {
            a.enabled = true;
            boxes[blockNumber].enabled = true;
            block.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        } else if(playerName == "blek(Clone)" || playerName == "red(Clone)" || playerName == "blue(Clone)") {
            boxes[blockNumber].enabled = true;
        } else {
            Debug.Log("error: can't find block number");
        }
    }

}
