using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VisionController : MonoBehaviourPun
{
    private Dictionary<string, string> players;
    private PhotonView myPhotonView;
    private InfoObject infoObject;
    private BlockController blockcontroller;
    private string userID;
    private bool testing;
    public Vector3 startPos;

    public float speed = 2.5f;
    public AreaEffector2D a;
    public BoxCollider2D b;
    public GameObject block;
    public int direction = 3;
    
    public BoxCollider2D[] boxes;

    Animator animator;


    private void Awake() {
        try {
            myPhotonView = GetComponent<PhotonView>();
            userID = PhotonNetwork.AuthValues.UserId;
            infoObject = GameObject.FindObjectOfType<InfoObject>();
            blockcontroller = GameObject.FindObjectOfType<BlockController>();        
            players = infoObject.GetCharacters();
            testing = false;
        } catch {
            Debug.Log("Not online so vision controller will run through different methods. Will still work though.");
            testing = true;
        }
        
    }

    // Start is called before the first frame update
   void Start()
   {    
      startPos = transform.position;
      a = GetComponent<AreaEffector2D>();      
      animator = GetComponent<Animator>();
      boxes = GetComponents<BoxCollider2D>();
      block = GameObject.FindGameObjectWithTag("block");
      a.enabled = false;
   }

   // Update is called once per frame
   private void Update() {


        if(GameObject.Find("Mud_Monster") != null  && GameObject.Find("Mud_Monster").GetComponent<MudMonsterController>().phase == 2){
           boxes[0].usedByEffector = false;
           boxes[1].usedByEffector = false;
           boxes[2].usedByEffector = false;
           boxes[3].usedByEffector = false;
        } else {
              boxes[0].usedByEffector = true;
           boxes[1].usedByEffector = true;
           boxes[2].usedByEffector = true;
           boxes[3].usedByEffector = true;
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
        if(Input.GetKey("space")){
            a.enabled = true;

            if(!testing) {

                //up
                if(direction == 1){
                    boxes[2].enabled = true;
                    blockcontroller.UpdateBlockStatus(2);
                    animator.SetFloat("MoveX", -.5f);
                    animator.SetFloat("MoveY", .5f);
                }

                //right
                if(direction == 2){
                    boxes[1].enabled = true;
                    blockcontroller.UpdateBlockStatus(1);
                    animator.SetFloat("MoveX", .5f);
                    animator.SetFloat("MoveY", .5f);
                }

                //down
                if(direction == 3){
                    boxes[3].enabled = true;
                    blockcontroller.UpdateBlockStatus(3);
                    animator.SetFloat("MoveX", .5f);
                    animator.SetFloat("MoveY", -.5f);
                }

                //left
                if(direction == 4){
                    boxes[0].enabled = true;
                    blockcontroller.UpdateBlockStatus(0);
                    animator.SetFloat("MoveX", -.5f);
                    animator.SetFloat("MoveY", -.5f);
                }
            } else {

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
  

void OnTriggerEnter2D(Collider2D player){
        Debug.Log("Reached");
        if(player.name == "Mud_Monster" && GameObject.Find("Mud_Monster").GetComponent<MudMonsterController>().phase == 2){
           GameObject.Find("Mud_Monster").GetComponent<MudMonsterController>().phase++;
        }
    
    }

}
    




