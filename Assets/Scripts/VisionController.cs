using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionController : MonoBehaviour
{
    public float speed = 2.5f;
    public AreaEffector2D a;
    public BoxCollider2D b;
    public GameObject block;
    // Start is called before the first frame update
   void Start()
   {
      a = GetComponent<AreaEffector2D>();
      b = GetComponent<BoxCollider2D>();
      block = GameObject.FindGameObjectWithTag("block");
      a.enabled = false;
      b.enabled = false;
   }
   // Update is called once per frame
   void Update()
   {
      a.enabled = false;
      b.enabled = false;
      block.GetComponent<Rigidbody2D>().velocity = Vector3.zero;


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
      if(Input.GetKey("space")){
         a.enabled = true;
         b.enabled = true;
      }
   }
}
