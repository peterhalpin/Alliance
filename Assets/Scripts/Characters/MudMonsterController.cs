﻿
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MudMonsterController : MonoBehaviourPun
{
    public float speed = 5.0f;
    public Animator animator;
    public bool moveLeft;
    public bool moveRight;
    public bool moveDown;
    public bool moveUp;
    // public GameObject magPlayer;
    // public GameObject firePlayer;
    // public GameObject icePlayer;
    // public GameObject strPlayer;
    // public Vector3 magPos;
    // public Vector3 icePos;
    // public Vector3 firePos;
    // public Vector3 strPos;

    //indicates what phase 
    public int phase;

    Rigidbody2D rigidbody2D;

[SerializeField]
  GameObject secondphase;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        animator.SetFloat("MoveX", 0);
        animator.SetFloat("MoveY", 0);
        moveLeft = true;
        moveRight = false;
        moveDown = false;
        moveUp = false;
        phase = 0;
        try {
            // Don't really need this

            // strPlayer = GameObject.FindWithTag("StrengthPlayer");
            // strPos = strPlayer.transform.position;
            // magPlayer = GameObject.FindWithTag("VisionPlayer");
            // magPos = magPlayer.transform.position;
            // icePlayer = GameObject.FindWithTag("IcePlayer");
            // icePos = icePlayer.transform.position;
            // firePlayer = null;
            // firePos = firePlayer.transform.position;
        } catch {
            Debug.Log("Testing mode!");
        }
        

    }

    // Update is called once per frame
    void Update()
    {
        // if(PhotonNetwork.IsMasterClient) {
            if(moveLeft){
                if(transform.position.x >= -6.75){
                    transform.position += Vector3.left * speed * Time.deltaTime;
                    animator.SetFloat("MoveX", -.5f);
                    animator.SetFloat("MoveY", 0);
                }
                else{
                    moveLeft = false;
                    moveDown = true;
                    
                }
            }
            if(moveDown){
                if(transform.position.y >= -2.92){
                    transform.position += Vector3.down * speed * Time.deltaTime;
                    animator.SetFloat("MoveX", 0);
                    animator.SetFloat("MoveY", -.5f);
                }
                else{
                    moveDown = false;
                    moveRight = true;
                    
                }
            }
            if(moveRight){
                if(transform.position.x <= 11.375){
                    transform.position += Vector3.right * speed *Time.deltaTime;
                    animator.SetFloat("MoveX", .5f);
                    animator.SetFloat("MoveY", 0);
                }
                else{
                    moveRight = false;
                    moveUp = true;
                }
                
            }
            if(moveUp){
                if(transform.position.y <= 9.56){
                    transform.position += Vector3.up * speed *Time.deltaTime;
                    animator.SetFloat("MoveX", 0);
                    animator.SetFloat("MoveY", .5f);
                }
                else{
                    moveUp = false;
                    moveLeft = true;
                }
            }
            //rigidbody2D.MovePosition(position);
            // rigidbody2D.MovePosition(position);
        // }
        if(phase == 0){
                // pause function
        }
        if(phase == 2){
                // pause function 
        }

    }


//Changed to find at runtime
    void OnCollisionEnter2D(Collision2D col){
        var name = col.gameObject.name;
        if(name == "red(Clone)"){
            GameObject.Find("red(Clone)").transform.position = new Vector3(30, 14, 100);
        }
        if(name == "blue(Clone)"){
            GameObject.Find("blue(Clone)").transform.position = new Vector3(27, -7, 100); 
        }
        if(name == "green(Clone)"){
            GameObject.Find("green(Clone)").transform.position = new Vector3(-30, -7, 100);
        }
        if(name == "blek(Clone)"){
            GameObject.Find("blek(Clone)").transform.position = new Vector3(-27, 14, 100);
        }
        

    }

    //Interactions with Monster
    void OnTriggerEnter2D(Collider2D player){

        if(player.name == "ice(Clone)" && phase == 0 ){
            phase++;
            //pause 
            
        }

        if(player.name == "blek(Clone)" && phase == 1 ){
            phase++;
         gameObject.GetComponent<SpriteRenderer>().sprite = secondphase.GetComponent<SpriteRenderer>().sprite;
        }

        if(player.name == "green(Clone)" && phase == 2 ){
            phase++;
            //pause
        }

        if(player.name == "fire(Clone)" && phase == 3 ){
            phase++;
            Destroy(gameObject);
        }

          
        }


}

