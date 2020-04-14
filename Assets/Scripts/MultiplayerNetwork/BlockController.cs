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
        myPhotonView = GetComponent<PhotonView>();   
        logCalled = false;
    }

    public void PullObjects() {
        block = GameObject.FindGameObjectWithTag("block");
        a = GameObject.FindWithTag("VisionPlayer").GetComponent<AreaEffector2D>();
        boxes = GameObject.FindWithTag("VisionPlayer").GetComponents<BoxCollider2D>();
    }

    public void UpdateBlockStatus(int blockNum) {        
        myPhotonView.RPC("PullObjectsForEveryone", RpcTarget.All);                
        myPhotonView.RPC("UpdateBlockForEveryone", RpcTarget.All, blockNum);        
    }


    [PunRPC]
    private void PullObjectsForEveryone() {
        block = GameObject.FindGameObjectWithTag("block");
        a = GameObject.FindWithTag("VisionPlayer").GetComponent<AreaEffector2D>();
        boxes = GameObject.FindWithTag("VisionPlayer").GetComponents<BoxCollider2D>();
        if(block != null && a != null && boxes != null && !logCalled) {
            Debug.Log("Is In Game!!!!! Nothing to worry about");
            logCalled = true;
        }
    }

    [PunRPC]
    private void UpdateBlockForEveryone(int blockNumber) {
        a.enabled = true;
        boxes[blockNumber].enabled = true;
        block.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
    }

}
