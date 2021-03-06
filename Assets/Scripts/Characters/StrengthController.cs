﻿using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StrengthController : MonoBehaviourPun
{
    private float speed;
    private int direction;

    private Animator animator;
    private Scene currentScene;
    private BlockController blockcontroller;
    private KeyboardShortcuts kbshortcuts;

    private BoxCollider2D[] boxes;

    private void Awake() {
        direction = 3;
        speed = 2.5f;
        animator = GetComponent<Animator>();
        currentScene = SceneManager.GetActiveScene();
        blockcontroller = GameObject.FindObjectOfType<BlockController>();        
        boxes = GetComponents<BoxCollider2D>();
        kbshortcuts = GameObject.FindObjectOfType<KeyboardShortcuts>();
    }

    void Start() {
        for(int i=0; i < boxes.Length ; i++){
            boxes[i].enabled = false;
        }
    }

    void Update() {
        for(int i=0; i < boxes.Length ; i++){
            boxes[i].enabled = false;
        }

        // this part is for changing the animations when the player moves
        // though this section is basically the same in all four characters, adding this all to one player movement script messes with animatons (more specifically the magnet player)
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

        // this is so the player can access the character's super power
        if(Input.GetKey("space")){
            ///up
            if(direction == 1){
                boxes[2].enabled = true;
                animator.SetFloat("MoveX", -.5f);
                animator.SetFloat("MoveY", .5f);
                if(PhotonNetwork.IsConnected)
                    blockcontroller.UpdateBlockStatus(2, gameObject.name);   
            // this called so that it goes too the block controller which will then update this on everyone's screen, other wise it won't work
            }
            //right
            if(direction == 2){
                boxes[1].enabled = true;
                animator.SetFloat("MoveX", .5f);
                animator.SetFloat("MoveY", .5f);
                if(PhotonNetwork.IsConnected)
                        blockcontroller.UpdateBlockStatus(1, gameObject.name);    
            }
            //down
            if(direction == 3){
                boxes[3].enabled = true;
                animator.SetFloat("MoveX", .5f);
                animator.SetFloat("MoveY", -.5f);
                if(PhotonNetwork.IsConnected)
                        blockcontroller.UpdateBlockStatus(3, gameObject.name);    
            }
            //left
            if(direction == 4){
                boxes[0].enabled = true;
                animator.SetFloat("MoveX", -.5f);
                animator.SetFloat("MoveY", -.5f);
                if(PhotonNetwork.IsConnected)
                        blockcontroller.UpdateBlockStatus(0, gameObject.name);    
            }   
        }
    }


    // this is so the player can attack the mud monster on level 4
    void OnTriggerEnter2D(Collider2D player) {
        if (currentScene.name == "Level4") {
            print(GameObject.Find("Mud_Monster").GetComponent<MudMonsterController>().phase);
            print(player.name);
            if(player.name == "Mud_Monster" && GameObject.Find("Mud_Monster").GetComponent<MudMonsterController>().phase == 1){
                if(PhotonNetwork.IsConnected) {
                    photonView.RPC("MudMonsterAttack", RpcTarget.All);
                } else {
                    GameObject.Find("Mud_Monster").GetComponent<MudMonsterController>().phase++;
                    GameObject.Find("Mud_Monster").GetComponent<MudMonsterController>().speed = 2.50f;
                    print(GameObject.Find("Mud_Monster").GetComponent<MudMonsterController>().phase);
                }

                //Code meant for sprite change
                // monster.GetComponent<Animator>().enabled = false;
                // monster.GetComponent<SpriteRenderer>().sprite = monster.GetComponent<MudMonsterController>().secondphase.GetComponent<SpriteRenderer>().sprite; 
            }
        }
    }

    [PunRPC]
    private void MudMonsterAttack() {
        GameObject.Find("Mud_Monster").GetComponent<MudMonsterController>().phase++;
        GameObject.Find("Mud_Monster").GetComponent<MudMonsterController>().speed = 2.50f;
        print(GameObject.Find("Mud_Monster").GetComponent<MudMonsterController>().phase);

    }

 
}
