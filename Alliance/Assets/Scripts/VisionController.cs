using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionController : MonoBehaviour
{
    public float speed = 2.5f;
    // Start is called before the first frame update
   void Start()
   {
   }
   // Update is called once per frame
   void Update()
   {
      if (Input.GetKey("j")){
         transform.position += Vector3.left * speed * Time.deltaTime;
        }
     if (Input.GetKey("l")){
         transform.position += Vector3.right * speed * Time.deltaTime;
        }
     if (Input.GetKey("i")){
         transform.position += Vector3.up * speed * Time.deltaTime;
        }
     if (Input.GetKey("k")){
         transform.position += Vector3.down * speed * Time.deltaTime;
        }
   }
}
