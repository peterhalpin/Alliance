using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopSwitch : MonoBehaviour
{
    private List<Collider2D> colliders = new List<Collider2D>();

    void OnTriggerEnter2D(Collider2D player)
    {
        if(!colliders.Contains(player)){
            colliders.Add(player);
        }
        print("enter top: "+ player);
        if(colliders.Count > 1){
            //make doors open
            print("Top Gate open");
        }
    }

    void OnTriggerExit2D(Collider2D player){
        colliders.Remove(player);
        print("leave top: "+ player);
    }
}
