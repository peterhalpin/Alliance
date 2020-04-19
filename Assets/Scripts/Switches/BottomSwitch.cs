using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class BottomSwitch : MonoBehaviour
{
    private List<Collider2D> colliders;
    private bool isDestroyed;

    private GameData gameData;
    private TimerController timerController;
    private Scene currentScene;

    private void Awake() {
        colliders = new List<Collider2D>();
        isDestroyed = false;
        //DATA COLLECTION CODE-------------------------------------------------------------------------------------------------------------------------------------
        gameData = GameObject.FindObjectOfType<GameData>();
        timerController = GameObject.FindObjectOfType<TimerController>();
        currentScene = SceneManager.GetActiveScene();
        //DATA COLLECTION CODE-------------------------------------------------------------------------------------------------------------------------------------
    }
    
    void OnTriggerEnter2D(Collider2D player)
    {       
        //DATA COLLECTION CODE-------------------------------------------------------------------------------------------------------------------------------------
        gameData.ActivateSwitchTime(currentScene.name + " BottomSwitch pressed by " + player.name, timerController.GetTime());
        gameData.GameInteraction(currentScene.name + " Player" + player.name + "stepped ON the BOTTOM switch", timerController.GetTime());
        //DATA COLLECTION CODE-------------------------------------------------------------------------------------------------------------------------------------
        if(!colliders.Contains(player)){
            colliders.Add(player);
        }

        if(colliders.Count > 1 && !isDestroyed){
            isDestroyed = true;
            //make doors open

            GameObject[] bottomDoor = GameObject.FindGameObjectsWithTag("Door Bottom");
            Destroy(bottomDoor[0]);
            Destroy(bottomDoor[1]);
        //DATA COLLECTION CODE-------------------------------------------------------------------------------------------------------------------------------------
            gameData.DoorOpen(currentScene.name + " BottomDoor open", timerController.GetTime());
            gameData.GameInteraction(currentScene.name + " BOTTOM door open: both top players stepped on the bottom switch", timerController.GetTime());
        //DATA COLLECTION CODE-------------------------------------------------------------------------------------------------------------------------------------
        
        }

    }

    void OnTriggerExit2D(Collider2D player){
        colliders.Remove(player);
        //DATA COLLECTION CODE-------------------------------------------------------------------------------------------------------------------------------------
        gameData.DeactivateSwitchTime(currentScene.name + " BottomSwitch left by " + player.name, timerController.GetTime());
        gameData.GameInteraction(currentScene.name + " Player " + player.name + " stepped OFF the BOTTOM switch", timerController.GetTime());
        //DATA COLLECTION CODE-------------------------------------------------------------------------------------------------------------------------------------
    }
}
