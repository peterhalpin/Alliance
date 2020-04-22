
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
  public GameObject secondphase;
    private float waitTime = 5.0f;
    private float timer = 0.0f;

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
        timerOn = false;
        moveLefttemp = false;
        moveRighttemp = false;
        moveUptemp = false;
        moveDowntemp = false;
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
            if (phase == 1 || phase == 3) {
                if (timerOn == false){
                var curr = phase;
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
                 
                 if (timer > waitTime){
            // Remove the recorded 2 seconds.
                timer = timer - waitTime;
                moveLeft = moveLefttemp;
                moveRight = moveRighttemp;
                moveUp = moveUptemp;
                moveDown = moveDowntemp;
                if(phase != 2 || phase != 4 ){
                    //testing
                    // phase--;
                }
                timerOn = false;
        }

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
    


}

