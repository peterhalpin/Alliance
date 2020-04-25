using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceController : MonoBehaviourPun
{
    public float speed = 2.5f;
    public int direction = 3;
    public BoxCollider2D[] boxes;
    public Vector3 startPos;
    Animator animator;

    private BlockController blockcontroller;
    private bool testing;
    private KeyboardShortcuts kbshortcuts;



    private void Awake() {
        kbshortcuts = GameObject.FindObjectOfType<KeyboardShortcuts>();

    }

    void Start()
    {
        print(gameObject.name); 

        startPos = transform.position;
        boxes = GetComponents<BoxCollider2D>();
        //rb = GetComponent<RigidBody2D>();
        animator = GetComponent<Animator>();
        for(int i=0; i < boxes.Length ; i++){
            boxes[i].enabled = false;
        }
        blockcontroller = GameObject.FindObjectOfType<BlockController>();        
        if(PhotonNetwork.IsConnected && blockcontroller != null) {
            testing = false;
        } else {
            testing = true;
        }
    }
    // Update is called once per frame
    void Update() {
        for(int i=0; i < boxes.Length ; i++){
            boxes[i].enabled = false;
        }

        //idle up
        if(direction == 1){
            animator.SetFloat("MoveX", .1f);
            animator.SetFloat("MoveY", .25f);
        }
        //idle right
        if(direction == 2){
            animator.SetFloat("MoveX", .25f);
            animator.SetFloat("MoveY", -.1f);
        } 
        //idle down
        if(direction == 3){
            animator.SetFloat("MoveX", -.1f);
            animator.SetFloat("MoveY", -.25f);
        }
        //idle left
        if(direction == 4){
            animator.SetFloat("MoveX", -.25f);
            animator.SetFloat("MoveY", .1f);
        }

        if (photonView.IsMine == false && PhotonNetwork.IsConnected == true) {
            return;
        }
       
        if(kbshortcuts.isInPlayerMap) {
            if (Input.GetKey(KeyCode.LeftArrow)){
                transform.position += Vector3.left * speed * Time.deltaTime;
                animator.SetFloat("MoveX", -.5f);
                animator.SetFloat("MoveY", 0);
                direction = 4;
            }
            if (Input.GetKey(KeyCode.RightArrow)){
                transform.position += Vector3.right * speed * Time.deltaTime;
                animator.SetFloat("MoveX", .5f);
                animator.SetFloat("MoveY", 0);
                direction = 2;
            }
            if (Input.GetKey(KeyCode.UpArrow)){
                transform.position += Vector3.up * speed * Time.deltaTime;
                animator.SetFloat("MoveX", 0);
                animator.SetFloat("MoveY", 0.5f);
                direction = 1;
            }
            if (Input.GetKey(KeyCode.DownArrow)){
                transform.position += Vector3.down * speed * Time.deltaTime;
                animator.SetFloat("MoveX", 0);
                animator.SetFloat("MoveY", -.5f);
                direction = 3;
            }
        }

        if(Input.GetKey("space")){

                //up
                if(direction == 1){
                    boxes[2].enabled = true;
                    if(!testing)
                        blockcontroller.UpdateBlockStatus(2, gameObject.name);                    
                }

                //right
                if(direction == 2){
                    boxes[1].enabled = true;
                    if(!testing)
                        blockcontroller.UpdateBlockStatus(1, gameObject.name);          
                }

                //down
                if(direction == 3){
                    boxes[3].enabled = true;
                    if(!testing)
                        blockcontroller.UpdateBlockStatus(3, gameObject.name);          
                }

                //left
                if(direction == 4){
                    boxes[0].enabled = true;
                    if(!testing)
                        blockcontroller.UpdateBlockStatus(0, gameObject.name);          
                }
            
        }
   }

   void OnTriggerEnter2D(Collider2D player){

        if(player.name == "Mud_Monster" && GameObject.Find("Mud_Monster").GetComponent<MudMonsterController>().phase == 0){
            GameObject.Find("Mud_Monster").GetComponent<MudMonsterController>().phase++;
}
}
}

