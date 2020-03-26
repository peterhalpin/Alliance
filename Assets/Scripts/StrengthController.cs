﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrengthController : MonoBehaviour
{
    public float speed = 2.5f;
    // Start is called before the first frame update
   void Start()
   {
       
   }
   // Update is called once per frame
   void Update()
   {
      if (Input.GetKey("a")){
         transform.position += Vector3.left * speed * Time.deltaTime;
        }
     if (Input.GetKey("d")){
         transform.position += Vector3.right * speed * Time.deltaTime;
        }
     if (Input.GetKey("w")){
         transform.position += Vector3.up * speed * Time.deltaTime;
        }
     if (Input.GetKey("s")){
         transform.position += Vector3.down * speed * Time.deltaTime;
        }
   }
}
