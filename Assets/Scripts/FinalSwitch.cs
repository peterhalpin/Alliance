using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalSwitch : MonoBehaviour
{
    private List<Collider2D> colliders = new List<Collider2D>();

    void OnTriggerEnter2D(Collider2D player)
    {
        if(!colliders.Contains(player)){
            colliders.Add(player);
        }
        print("enter bottom: "+ player);
        if(colliders.Count == 4){
             SceneManager.LoadScene(sceneName:"Level2");
           

        }

    }

    void OnTriggerExit2D(Collider2D player){
        colliders.Remove(player);
        
    }
}
