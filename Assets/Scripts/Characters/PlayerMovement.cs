using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviourPun
{
    private Animator animator;
    private float speed;
    private bool freezeCharacter;
    private int direction;


    private void Awake() {
        animator = GetComponent<Animator>();
        direction = 3;
        freezeCharacter = false;
        speed = 2.5f;

    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

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

        if (photonView.IsMine == false && PhotonNetwork.IsConnected == true) {
            return;
        }

        if(!freezeCharacter) {
            if (Input.GetKey(KeyCode.LeftArrow)){
                GetComponent<Rigidbody2D>().isKinematic = false;
                transform.position += Vector3.left * speed * Time.deltaTime;
                animator.SetFloat("MoveX", -.5f);
                animator.SetFloat("MoveY", 0);
                direction = 4;
            }
            if (Input.GetKey(KeyCode.RightArrow)){
                GetComponent<Rigidbody2D>().isKinematic = false;
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
                GetComponent<Rigidbody2D>().isKinematic = false;
                transform.position += Vector3.down * speed * Time.deltaTime;
                animator.SetFloat("MoveX", 0);
                animator.SetFloat("MoveY", -.5f);
                direction = 3;
            }
        }



    }
}
