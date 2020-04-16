using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrengthController : MonoBehaviourPun
{
    public float speed = 2.5f;
    public Animator animator;
    public int direction = 3;
    // Start is called before the first frame update
   void Start()
   {
       animator = GetComponent<Animator>();
   }
   // Update is called once per frame
   void Update()
   {
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
                
                animator.SetFloat("MoveX", -.5f);
                animator.SetFloat("MoveY", .5f);
            }

            //right
            if(direction == 2){
               
                animator.SetFloat("MoveX", .5f);
                animator.SetFloat("MoveY", .5f);
            }

            //down
            if(direction == 3){
                
                animator.SetFloat("MoveX", .5f);
                animator.SetFloat("MoveY", -.5f);
            }

            //left
            if(direction == 4){
            
                animator.SetFloat("MoveX", -.5f);
                animator.SetFloat("MoveY", -.5f);
            }

            
        }

   }
}
