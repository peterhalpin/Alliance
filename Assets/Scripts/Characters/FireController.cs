using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireController : MonoBehaviourPun
{
    public float speed = 2.5f;
    public Animator animator;
    public int direction = 3;
    public BoxCollider2D[] boxes;
    public Vector3 startPos;
    // Start is called before the first frame update
   void Start()
   {
       startPos = transform.position;
       boxes = GetComponents<BoxCollider2D>();
       animator = GetComponent<Animator>();
       for(int i=0; i < boxes.Length ; i++){
            boxes[i].enabled = false;
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

                //up
                if(direction == 1){
                    boxes[2].enabled = true;
                }

                //right
                if(direction == 2){
                    boxes[1].enabled = true;
                }

                //down
                if(direction == 3){
                    boxes[3].enabled = true;
                }

                //left
                if(direction == 4){
                    boxes[0].enabled = true;
                }
            
        }

   }

void OnTriggerEnter2D(Collider2D player){

        if(player.name == "Mud_Monster" && GameObject.Find("Mud_Monster").GetComponent<MudMonsterController>().phase == 3){
            var monster=  GameObject.Find("Mud_Monster");
            Destroy(monster);
        }
    
    }
    

}
