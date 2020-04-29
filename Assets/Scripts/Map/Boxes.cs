using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boxes : MonoBehaviourPun
{
    private List<Collider2D> colliders = new List<Collider2D>();
    
    private BlockController blockcontroller;
    // private bool called;
    
    public BoxCollider2D[] greenBoxes;
    public GameObject green;
    public AreaEffector2D a;
    public CapsuleCollider2D cap;
    public Rigidbody2D rb;



    private void Awake() {
        blockcontroller = GameObject.FindObjectOfType<BlockController>();
        // called = false;
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
        
        cap = green.GetComponent<CapsuleCollider2D>();
        rb = green.GetComponent<Rigidbody2D>();
        
    
        if(col == greenBoxes[0]){
            a.forceAngle = 0;
            rb.isKinematic = true;
            rb.constraints = RigidbodyConstraints2D.FreezePositionX;
            rb.constraints = RigidbodyConstraints2D.FreezePositionY;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            colliders.Add(col);

        }
        if(col == greenBoxes[1]){
            a.forceAngle = 180;
            rb.isKinematic = true;
            rb.constraints = RigidbodyConstraints2D.FreezePositionX;
            rb.constraints = RigidbodyConstraints2D.FreezePositionY;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            colliders.Add(col);
        }
        if(col == greenBoxes[2]){
            a.forceAngle = 270;
            rb.isKinematic = true;
            rb.constraints = RigidbodyConstraints2D.FreezePositionX;
            rb.constraints = RigidbodyConstraints2D.FreezePositionY;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            colliders.Add(col);
        }
        if(col == greenBoxes[3]){
            a.forceAngle = 90;
            rb.isKinematic = true;
            rb.constraints = RigidbodyConstraints2D.FreezePositionX;
            rb.constraints = RigidbodyConstraints2D.FreezePositionY;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            colliders.Add(col);
        }

        
        
    }

     void OnTriggerExit2D(Collider2D col){
         colliders.Remove(col);
         
     }


}
