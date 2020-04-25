using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;


public class GameSetupController : MonoBehaviourPun
{
    private InfoObject infoObject;
    private Dictionary<string, string> players;
    private int level;
    private Vector3[] playerPosition;

    private Scene currentScene;
    private string sceneName;


    // false if not testing and developing, true if so
    private bool testing;

    // This script will be added to any multiplayer scene
    private void Awake() {
        currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;
        // if the catch gets executed, then that means that we aren't loading the game properly, most likely because we are testing
        try {
            infoObject = GameObject.FindObjectOfType<InfoObject>();
            players = infoObject.GetCharacters();
            level = infoObject.GetLevel();
            playerPosition = new Vector3[4];
            testing = false;
            if(infoObject == null) {
                testing = true;
            }
        } catch {
            testing = true;
        }
    }

    private void Start() {       
        if (!testing) {
            SetLevelPositions(); // changing the starting positions of each character based on their levels
        }
        CreatePlayer(); // Create a networked player object for each player that loads into the multiplayer scenes.
    }

    private void SetLevelPositions() {
        // will need to add more conditionals as you add more levels and want players to spawn in different positions
        // POSITIONS:
        // 0 == blek - strength character
        // 1 == blue - ice character
        // 2 == green - magnet character
        // 3 == red - fire character
        // values passed into Vector3 need to be integers, otherwise it won't work
        // there might be a work around that though ^
        // if(level == 1) {
        if(sceneName == "TutorialLevel") { 
            playerPosition[0] = new Vector3(-9, 6, 0); // strength
            playerPosition[1] = new Vector3(9, -5, 0); // ice
            playerPosition[2] = new Vector3(9, 6, 0); // magnet
            playerPosition[3] = new Vector3(-9, -6, 0); // fire
        // } else if (level == 2) {
        } else if (sceneName == "Level2") {
            playerPosition[0] = new Vector3(11, 7, 0); // strength
            playerPosition[1] = new Vector3(-8, 6, 0); // ice
            playerPosition[2] = new Vector3(0, 6, 0); // magnet
            playerPosition[3] = new Vector3(12, -6, 0); // fire
        // } else if (level == 3) {
        } else if (sceneName == "Level3") { 
            playerPosition[0] = new Vector3(-23, 9, 0); // strength
            playerPosition[1] = new Vector3(-23, -5, 0); // ice
            playerPosition[2] = new Vector3(-23, -12, 0); // magnet
            playerPosition[3] = new Vector3(-23, 3, 0); // fire
        // } else if (level == 4) {
        } else if (sceneName == "Level4") { 
            playerPosition[0] = new Vector3(-27, 14, 0); // strength
            playerPosition[1] = new Vector3(27, -7, 0); // ice
            playerPosition[2] = new Vector3(-30, -7, 0); // magnet
            playerPosition[3] = new Vector3(30, 14, 0); // fire
        } else {
            print(level);
            Debug.LogError("Error setting player positions!");
        }
    }

    private void CreatePlayer() {  
        if(!testing) {
            string userID = PhotonNetwork.AuthValues.UserId;
            if (players[userID] == "blek") {
                PhotonNetwork.Instantiate(Path.Combine("Prefabs/Characters", "blek"), playerPosition[0], Quaternion.identity);
            } else if (players[userID] == "blue"){
                PhotonNetwork.Instantiate(Path.Combine("Prefabs/Characters", "blue"), playerPosition[1], Quaternion.identity);
            } else if (players[userID] == "green"){
                PhotonNetwork.Instantiate(Path.Combine("Prefabs/Characters", "green"), playerPosition[2], Quaternion.identity);
            } else {
                PhotonNetwork.Instantiate(Path.Combine("Prefabs/Characters", "red"), playerPosition[3], Quaternion.identity);
            }
        } else {
            // this will get run if we are testing
            // change the color to whichever character you need
            // change the Vector position to wherever you need the player to spawn
            // the player positions correspoding to each character and each level are listed above in the SetLevelPositions method
            // reference that when changing Vector position for each character
            // comment the bottom line out of if you wish to just add the prefab on the scene
            Object varPrefab = Resources.Load("Prefabs/Characters/green", typeof(GameObject));
            Instantiate(varPrefab, new Vector3(0, 5, 0), Quaternion.identity);
            //  Object varPrefab2 = Resources.Load("Prefabs/Characters/blue", typeof(GameObject));
            // Instantiate(varPrefab2, new Vector3(17, 0, 0), Quaternion.identity);
            //  Object varPrefab3 = Resources.Load("Prefabs/Characters/blek", typeof(GameObject));
            // Instantiate(varPrefab3, new Vector3(23, 0, 0), Quaternion.identity);
            //  Object varPrefab4 = Resources.Load("Prefabs/Characters/green", typeof(GameObject));
            // Instantiate(varPrefab4, new Vector3(23, 0, 0), Quaternion.identity);
            
        }
    }
}
