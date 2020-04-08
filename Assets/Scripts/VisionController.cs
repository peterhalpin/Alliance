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
    
    public BoxCollider2D[] boxes;
    // Start is called before the first frame update
   void Start()
   {
      a = GetComponent<AreaEffector2D>();
      boxes = GetComponents<BoxCollider2D>();
      

      block = GameObject.FindGameObjectWithTag("block");

      a.enabled = false;
      
   }
   // Update is called once per frame
   void Update()
   {
        if (photonView.IsMine == false && PhotonNetwork.IsConnected == true) {
            return;
        }

        a.enabled = false;
        
        //left box
        boxes[0].enabled = false;
        //right box
        boxes[1].enabled = false;
        //top box
        boxes[2].enabled = false;
        //bottom box
        boxes[3].enabled = false;
        
        block.GetComponent<Rigidbody2D>().velocity = Vector3.zero;

        if (Input.GetKey(KeyCode.LeftArrow)){
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.RightArrow)){
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.UpArrow)){
            transform.position += Vector3.up * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.DownArrow)){
            transform.position += Vector3.down * speed * Time.deltaTime;
        }
        if(Input.GetKey("space")){
            a.enabled = true;
            
            boxes[0].enabled = true;
            boxes[1].enabled = true;
            boxes[2].enabled = true;
            boxes[3].enabled = true;
            
        }
   }
   
}
