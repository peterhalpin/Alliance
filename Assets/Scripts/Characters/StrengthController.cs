using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrengthController : MonoBehaviourPun
{
    public float speed = 2.5f;
    public Animator animator;
    public int direction = 3;
     public BoxCollider2D[] boxes;
    public Vector3 startPos;

    private BlockController blockcontroller;
    private bool testing;




   void Start()
   {
        print(gameObject.name); 

       startPos = transform.position;
       animator = GetComponent<Animator>();
       boxes = GetComponents<BoxCollider2D>();
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
   void Update()
   {
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
        if(Input.GetKey("space")){
           
            ///up
            if(direction == 1){
                boxes[2].enabled = true;
                animator.SetFloat("MoveX", -.5f);
                animator.SetFloat("MoveY", .5f);
                if(!testing)
                    blockcontroller.UpdateBlockStatus(2, gameObject.name);   
            }

            //right
            if(direction == 2){
                boxes[1].enabled = true;
                animator.SetFloat("MoveX", .5f);
                animator.SetFloat("MoveY", .5f);
                if(!testing)
                        blockcontroller.UpdateBlockStatus(1, gameObject.name);    
            }

            //down
            if(direction == 3){
                boxes[3].enabled = true;
                animator.SetFloat("MoveX", .5f);
                animator.SetFloat("MoveY", -.5f);
                if(!testing)
                        blockcontroller.UpdateBlockStatus(3, gameObject.name);    
            }

            //left
            if(direction == 4){
                boxes[0].enabled = true;
                animator.SetFloat("MoveX", -.5f);
                animator.SetFloat("MoveY", -.5f);
                if(!testing)
                        blockcontroller.UpdateBlockStatus(0, gameObject.name);    
            }

            
        }

   }
 
}
