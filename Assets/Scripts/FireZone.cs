using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireZone : MonoBehaviour
{
    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D other)
    {
        FireController controller = other.GetComponent<FireController >();
        
        print(controller);
        if(other.tag == "IcePlayer"){
            //print(other);
            
        }
    }
}
