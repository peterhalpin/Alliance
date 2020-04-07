using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrengthController : MonoBehaviourPun
{
    public float speed = 2.5f;
    // Start is called before the first frame update
   void Start()
   {
       
   }
   // Update is called once per frame
   void Update()
   {
       if (photonView.IsMine == false && PhotonNetwork.IsConnected == true) {
            return;
        }


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
    //   if (Input.GetKey("a")){
    //      transform.position += Vector3.left * speed * Time.deltaTime;
    //     }
    //  if (Input.GetKey("d")){
    //      transform.position += Vector3.right * speed * Time.deltaTime;
    //     }
    //  if (Input.GetKey("w")){
    //      transform.position += Vector3.up * speed * Time.deltaTime;
    //     }
    //  if (Input.GetKey("s")){
    //      transform.position += Vector3.down * speed * Time.deltaTime;
    //     }
   }
}
