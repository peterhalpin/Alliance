using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VisionController : MonoBehaviourPun
{

    // private BoxCollider2D b;

    private bool testing;

    private float speed;
    private int direction;

    private AreaEffector2D a;
    private Animator animator;
    private GameObject block;
    private BlockController blockcontroller;
    private KeyboardShortcuts kbshortcuts;

    private BoxCollider2D[] boxes;

    private void Awake() {
        direction = 3;
        speed = 2.5f;
        a = GetComponent<AreaEffector2D>();      
        block = GameObject.FindGameObjectWithTag("block");
        animator = GetComponent<Animator>();
        blockcontroller = GameObject.FindObjectOfType<BlockController>();        
        boxes = GetComponents<BoxCollider2D>();
        kbshortcuts = GameObject.FindObjectOfType<KeyboardShortcuts>();

        if(PhotonNetwork.IsConnected) {
            testing = false;
        } else {
            testing = true;
        }
    }

   void Start() {    
        a.enabled = false;
        for(int i=0; i < boxes.Length ; i++){
            boxes[i].enabled = false;
        }
   }

   // Update is called once per frame
    private void Update() {
        a.enabled = false;
        block.GetComponent<Rigidbody2D>().velocity = Vector3.zero;        

        for(int i=0; i < boxes.Length ; i++){
            boxes[i].enabled = false;
        }

       //Necessary for Level 4 to actually interact with mud monster
       //Caveat: This code turns off the "usedByEffector" box in the 2D collider component
       //temporarily not allowing the magnet person interact with the cube untill the monster goes to phase 3.
       //This was done because otherwise the monster will not be registered for going into the trigger box belonging
       //to the magnet person.
       //No fix has been attempted due to time.

        if(GameObject.Find("Mud_Monster") != null  && GameObject.Find("Mud_Monster").GetComponent<MudMonsterController>().phase == 2){
            boxes[0].usedByEffector = false;
            boxes[1].usedByEffector = false;
            boxes[2].usedByEffector = false;
            boxes[3].usedByEffector = false;
        } else {
            boxes[0].usedByEffector = true;
            boxes[1].usedByEffector = true;
            boxes[2].usedByEffector = true;
            boxes[3].usedByEffector = true;
        }



        // //left box
        // boxes[0].enabled = false;
        // //right box
        // boxes[1].enabled = false;
        // //top box
        // boxes[2].enabled = false;
        // //bottom box
        // boxes[3].enabled = false;


        // this part is for changing the animations when the player moves
        // though this section is basically the same in all four characters, adding this all to one player movement script messes with animatons (more specifically the magnet player)
        //idle up
        //idle up
        if(direction == 1) {
            animator.SetFloat("MoveX", .1f);
            animator.SetFloat("MoveY", .25f);
        }
        //idle right
        if(direction == 2) {
            animator.SetFloat("MoveX", .25f);
            animator.SetFloat("MoveY", -.1f);
        } 
        //idle down
        if(direction == 3) {
            animator.SetFloat("MoveX", -.1f);
            animator.SetFloat("MoveY", -.25f);
        }
        //idle left
        if(direction == 4) {
            animator.SetFloat("MoveX", -.25f);
            animator.SetFloat("MoveY", .1f);
        }

        // this is so that player's won't move other characters who don't belong to them, check's if the player is theirs
        if (PhotonNetwork.IsConnected && !photonView.IsMine) {
            return;
        }

        if(kbshortcuts.isInPlayerMap) {
            if (Input.GetKey(KeyCode.LeftArrow)) {
                GetComponent<Rigidbody2D>().isKinematic = false;
                transform.position += Vector3.left * speed * Time.deltaTime;
                animator.SetFloat("MoveX", -.5f);
                animator.SetFloat("MoveY", 0);
                direction = 4;
            }
            if (Input.GetKey(KeyCode.RightArrow)) {
                GetComponent<Rigidbody2D>().isKinematic = false;
                transform.position += Vector3.right * speed * Time.deltaTime;
                animator.SetFloat("MoveX", .5f);
                animator.SetFloat("MoveY", 0);
                direction = 2;
            }
            if (Input.GetKey(KeyCode.UpArrow)) {
                transform.position += Vector3.up * speed * Time.deltaTime;
                animator.SetFloat("MoveX", 0);
                animator.SetFloat("MoveY", 0.5f);
                direction = 1;
            }
            if (Input.GetKey(KeyCode.DownArrow)) {
                GetComponent<Rigidbody2D>().isKinematic = false;
                transform.position += Vector3.down * speed * Time.deltaTime;
                animator.SetFloat("MoveX", 0);
                animator.SetFloat("MoveY", -.5f);
                direction = 3;
            }
        }

        if(Input.GetKey("space")){
            a.enabled = true;

            if(!testing) {

                //up
                if(direction == 1){
                    boxes[2].enabled = true;
                    blockcontroller.UpdateBlockStatus(2, gameObject.name);
                    animator.SetFloat("MoveX", -.5f);
                    animator.SetFloat("MoveY", .5f);
                }

                //right
                if(direction == 2){
                    boxes[1].enabled = true;
                    blockcontroller.UpdateBlockStatus(1, gameObject.name);
                    animator.SetFloat("MoveX", .5f);
                    animator.SetFloat("MoveY", .5f);
                }

                //down
                if(direction == 3){
                    boxes[3].enabled = true;
                    blockcontroller.UpdateBlockStatus(3, gameObject.name);
                    animator.SetFloat("MoveX", .5f);
                    animator.SetFloat("MoveY", -.5f);
                }

                //left
                if(direction == 4){
                    boxes[0].enabled = true;
                    blockcontroller.UpdateBlockStatus(0, gameObject.name);
                    animator.SetFloat("MoveX", -.5f);
                    animator.SetFloat("MoveY", -.5f);
                }
            } else {

                if(direction == 1){
                    boxes[2].enabled = true;
                    animator.SetFloat("MoveX", -.5f);
                    animator.SetFloat("MoveY", .5f);
                }

                //right
                if(direction == 2){
                    boxes[1].enabled = true;
                    animator.SetFloat("MoveX", .5f);
                    animator.SetFloat("MoveY", .5f);
                }

                //down
                if(direction == 3){
                    boxes[3].enabled = true;
                    animator.SetFloat("MoveX", .5f);
                    animator.SetFloat("MoveY", -.5f);
                }

                //left
                if(direction == 4){
                    boxes[0].enabled = true;
                    animator.SetFloat("MoveX", -.5f);
                    animator.SetFloat("MoveY", -.5f);
                }
            }
        }
   }
  

void OnTriggerEnter2D(Collider2D player){
        Debug.Log("Reached");
        
         //To do: Add a null check!
        //Required for Level 4
        if(player.name == "Mud_Monster" && GameObject.Find("Mud_Monster").GetComponent<MudMonsterController>().phase == 2){
           GameObject.Find("Mud_Monster").GetComponent<MudMonsterController>().phase++;
        }
    
    }

}
    




