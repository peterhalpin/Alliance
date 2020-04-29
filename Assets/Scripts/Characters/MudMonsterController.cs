
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
    public bool timerOn;

    public bool moveLefttemp;
    public bool moveRighttemp;
    public bool moveUptemp;
    public bool moveDowntemp;

    //indicates what phase  the monster is on 
    public int phase;
    //if phase = 0 -> ice must interact
    //if phase = 1 -> strength must interact
    //if phase = 2 -> magnet must interact
    //if phase = 3 -> fire must interact

    //phase variable is modified by the fire/ice/magnet/strength scripts

    [SerializeField]
    public GameObject secondphase;
    private float waitTime = 5.0f;
    private float timer = 0.0f;

    void Start() {
        animator = GetComponent<Animator>();
        animator.SetFloat("MoveX", 0);
        animator.SetFloat("MoveY", 0);
        moveLeft = true;
        moveRight = false;
        moveDown = false;
        moveUp = false;
        phase = 0;
        timerOn = false;
        moveLefttemp = false;
        moveRighttemp = false;
        moveUptemp = false;
        moveDowntemp = false;

    }

    void Update() {
        // if(PhotonNetwork.IsMasterClient) {
        if (phase == 1 || phase == 3) {
            //saves current movement in temp variables
            //so that when it starts moving it can return to its current path
            if (timerOn == false) {
                moveLefttemp = moveLeft;
                moveRighttemp = moveRight;
                moveUptemp = moveUp;
                moveDowntemp = moveDown;
                moveLeft = false;
                moveRight = false;
                moveUp = false;
                moveDown = false;
            }
            timerOn = true;
            timer += Time.deltaTime;
            //When the timer is up this section activates
            if (timer > waitTime) {
                timer = timer - waitTime;
                moveLeft = moveLefttemp;
                moveRight = moveRighttemp;
                moveUp = moveUptemp;
                moveDown = moveDowntemp;
                if(phase != 2 || phase != 4 ){
                    phase--;
                }
                timerOn = false;
            }
        }

        //Changes appearance of sprite at halfway point
        if(phase >= 3) {
            gameObject.GetComponent<SpriteRenderer>().color =  new Color(0.3f, 0.4f, 0.6f, 1.0f);
        }

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


//Changed to find at runtime

    void OnCollisionEnter2D(Collision2D col) {
        //Resets players to original position
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

    


}

