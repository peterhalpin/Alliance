using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionController : MonoBehaviour
{
    public Rigidbody rb;
    public float speed = 2.5f;
   
    public AreaEffector2D a;
    public BoxCollider2D b;
    // Start is called before the first frame update
   void Start()
   {
      // (player.GetComponent(typeof(AreaEffector2D))).enabled = false;
      // (player.GetComponents(typeof(BoxCollider2D))).enabled = false;
      a = GetComponent<AreaEffector2D>();
      b = GetComponent<BoxCollider2D>();
      a.enabled = false;
      b.enabled = false;
      
   }
   // Update is called once per frame
   void Update()
   {
      a.enabled = false;
      b.enabled = false;
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
      if (Input.GetKey("space")){
         a.enabled = true;
         b.enabled = true;
      }
      
   }
}
