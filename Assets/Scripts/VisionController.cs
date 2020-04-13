﻿using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionController : MonoBehaviourPun
{
    public float speed = 2.5f;
    public AreaEffector2D a;
    public BoxCollider2D b;
    public GameObject block;
    public int direction = 3;
    
    public BoxCollider2D[] boxes;

    Animator animator;
    // Start is called before the first frame update
   void Start()
   {
      animator = GetComponent<Animator>();
      a = GetComponent<AreaEffector2D>();
      boxes = GetComponents<BoxCollider2D>();
      

      block = GameObject.FindGameObjectWithTag("block");

      a.enabled = false;
      
   }
   // Update is called once per frame
   void Update()
   {
        a.enabled = false;
       
       //left box
        boxes[0].enabled = false;
        //right box
        boxes[1].enabled = false;
        //top box
        boxes[2].enabled = false;
        //bottom box
        boxes[3].enabled = false;
       
            if(direction == 1){
                animator.SetFloat("MoveX", .1f);
                animator.SetFloat("MoveY", .25f);
            }
            if(direction == 2){
                animator.SetFloat("MoveX", .25f);
                animator.SetFloat("MoveY", -.1f);
            }
            if(direction == 3){
                animator.SetFloat("MoveX", -.1f);
                animator.SetFloat("MoveY", -.25f);
            }
            if(direction == 4){
                animator.SetFloat("MoveX", -.25f);
                animator.SetFloat("MoveY", .1f);
            }
       
        
        
        
        block.GetComponent<Rigidbody2D>().velocity = Vector3.zero;

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
            a.enabled = true;
     
           

            ///up
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
