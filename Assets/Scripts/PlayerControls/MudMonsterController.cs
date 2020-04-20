
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
    public GameObject magPlayer;
    public GameObject firePlayer;
    public GameObject icePlayer;
    public GameObject strPlayer;
    public Vector3 magPos;
    public Vector3 icePos;
    public Vector3 firePos;
    public Vector3 strPos;

    Rigidbody2D rigidbody2D;
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
        try {
            strPlayer = GameObject.FindWithTag("StrengthPlayer");
            strPos = strPlayer.transform.position;
            magPlayer = GameObject.FindWithTag("VisionPlayer");
            magPos = magPlayer.transform.position;
            icePlayer = GameObject.FindWithTag("IcePlayer");
            icePos = icePlayer.transform.position;
            firePlayer = GameObject.FindWithTag("FirePlayer");
            firePos = firePlayer.transform.position;
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


    }

    void OnCollisionEnter2D(Collision2D col){
        
        if(col.gameObject == firePlayer){
            firePlayer.transform.position = firePos;
        }
        if(col.gameObject == icePlayer){
            icePlayer.transform.position = icePos;
        }
        if(col.gameObject == magPlayer){
            magPlayer.transform.position = magPos;
        }
        if(col.gameObject == strPlayer){
            strPlayer.transform.position = strPos;
        }
        
        

    }
}
