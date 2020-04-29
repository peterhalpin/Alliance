using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class TopSwitch : MonoBehaviour
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
        gameData.ActivateSwitchTime(currentScene.name + " TopSwitch pressed by " + player.name, timerController.GetTime());
        gameData.GameInteraction(currentScene.name + " Player " + player.name + " stepped ON the TOP switch " + DateTime.Now.ToString("h:mm:ss tt") + ", " , timerController.GetTime());
        //DATA COLLECTION CODE-------------------------------------------------------------------------------------------------------------------------------------
        if(!colliders.Contains(player) && player.name != "Boulder" && player.name != "Block"){
            colliders.Add(player);
        }
        
        if(colliders.Count > 1 && !isDestroyed){
            isDestroyed = true;
            //make doors open
            GameObject[] topDoor = GameObject.FindGameObjectsWithTag("Door Top");
            Destroy(topDoor[0]);
            Destroy(topDoor[1]);
        //DATA COLLECTION CODE-------------------------------------------------------------------------------------------------------------------------------------
            gameData.DoorOpen(currentScene.name + " TopDoor open", timerController.GetTime());
            gameData.GameInteraction(currentScene.name + " TOP door open: both top players stepped on the top switch " + DateTime.Now.ToString("h:mm:ss tt") + ", " , timerController.GetTime());
        //DATA COLLECTION CODE-------------------------------------------------------------------------------------------------------------------------------------

        } 
    }

    void OnTriggerExit2D(Collider2D player){
        colliders.Remove(player);
        //DATA COLLECTION CODE-------------------------------------------------------------------------------------------------------------------------------------
        gameData.DeactivateSwitchTime(currentScene.name + " TopSwitch left by " + player.name, timerController.GetTime());
        gameData.GameInteraction(currentScene.name + " Player " + player.name + " stepped OFF the TOP switch " + DateTime.Now.ToString("h:mm:ss tt") + ", " , timerController.GetTime());
        //DATA COLLECTION CODE-------------------------------------------------------------------------------------------------------------------------------------

        
    }
}
