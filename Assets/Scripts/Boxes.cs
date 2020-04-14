using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boxes : MonoBehaviourPun
{
    private string userID;
    private Dictionary<string, string> players;
    private List<Collider2D> colliders = new List<Collider2D>();
    private PhotonView myPhotonView;
    private InfoObject infoObject;
    private BlockController blockcontroller;
    private bool called;
    
    public BoxCollider2D[] greenBoxes;
    public GameObject green;
    public AreaEffector2D a;


    private void Awake() {
        blockcontroller = GameObject.FindObjectOfType<BlockController>();
        called = false;
    }

    private void Start() {
        try {   
            green = GameObject.FindWithTag("VisionPlayer");
            greenBoxes = green.GetComponents<BoxCollider2D>();
            a = green.GetComponent<AreaEffector2D>();
        } catch {
            Debug.Log("If the message (Is In Game!) appears, then ignore this: Trying to find the magnet player but it isn't in the game: lines: 14-16 in Boxes.cs, this is the error but if you just load the magnet player then it will be fixed.");
        }   
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        green = GameObject.FindWithTag("VisionPlayer");
        greenBoxes = green.GetComponents<BoxCollider2D>();
        a = green.GetComponent<AreaEffector2D>();
    
        if(col == greenBoxes[0]){
            a.forceAngle = 0;
        }
        if(col == greenBoxes[1]){
            a.forceAngle = 180;
        }
        if(col == greenBoxes[2]){
            a.forceAngle = 270;
        }
        if(col == greenBoxes[3]){
            a.forceAngle = 90;
        }
    }


}
