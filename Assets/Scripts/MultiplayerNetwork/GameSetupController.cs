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
    private Vector3[] playerPosition;    
    private Dictionary<string, string> players;

    private void Awake() {
        if(PhotonNetwork.IsConnected) {
            infoObject = GameObject.FindObjectOfType<InfoObject>();
            players = infoObject.GetCharacters();
            playerPosition = new Vector3[4];
        }
    }

    private void Start() {       
        if(PhotonNetwork.IsConnected)
            SetLevelPositions(); // changing the starting positions of each character based on their levels
        CreatePlayer(); // Create a networked player object for each player that loads into the multiplayer scenes.
    }

    private void SetLevelPositions() {
        Scene currentScene = SceneManager.GetActiveScene();
        // will need to add more conditionals as you add more levels and want players to spawn in different positions
        // POSITIONS:
        // 0 == blek - strength character
        // 1 == blue - ice character
        // 2 == green - magnet character
        // 3 == red - fire character
        // values passed into Vector3 need to be integers, otherwise it won't work
        // there might be a work around that though ^
        if(currentScene.name == "TutorialLevel") { 
            playerPosition[0] = new Vector3(0, 0, 0); // strength
            playerPosition[1] = new Vector3(0, 0, 0); // ice
            playerPosition[2] = new Vector3(0, 0, 0); // magnet
            playerPosition[3] = new Vector3(0, 0, 0); // fire
        } else if (currentScene.name == "Level2") {
            playerPosition[0] = new Vector3(11, 7, 0); // strength
            playerPosition[1] = new Vector3(-8, 6, 0); // ice
            playerPosition[2] = new Vector3(0, 6, 0); // magnet
            playerPosition[3] = new Vector3(12, -6, 0); // fire
        } else if (currentScene.name == "Level3") { 
            playerPosition[0] = new Vector3(-23, 9, 0); // strength
            playerPosition[1] = new Vector3(-23, -5, 0); // ice
            playerPosition[2] = new Vector3(-23, -12, 0); // magnet
            playerPosition[3] = new Vector3(-23, 3, 0); // fire
        } else if (currentScene.name == "Level4") { 
            playerPosition[0] = new Vector3(-27, 14, 0); // strength
            playerPosition[1] = new Vector3(27, -7, 0); // ice
            playerPosition[2] = new Vector3(-30, -7, 0); // magnet
            playerPosition[3] = new Vector3(30, 14, 0); // fire
        } else {
            Debug.LogError("Error setting player positions!");
        }
    }

    private void CreatePlayer() {  
        if(PhotonNetwork.IsConnected) {
            string userID = PhotonNetwork.AuthValues.UserId;
            if (players[userID] == "blek") {
                PhotonNetwork.Instantiate(Path.Combine("Prefabs/Characters", "blek"), playerPosition[0], Quaternion.identity);
            } else if (players[userID] == "blue"){
                PhotonNetwork.Instantiate(Path.Combine("Prefabs/Characters", "blue"), playerPosition[1], Quaternion.identity);
            } else if (players[userID] == "green"){
                PhotonNetwork.Instantiate(Path.Combine("Prefabs/Characters", "green"), playerPosition[2], Quaternion.identity);
            } else { // if "red" then this one
                PhotonNetwork.Instantiate(Path.Combine("Prefabs/Characters", "red"), playerPosition[3], Quaternion.identity);
            }
        } else {
            // this will get run if we are testing
            // change the color to whichever character you need
            // change the Vector position to wherever you need the player to spawn
            // the player positions correspoding to each character and each level are listed above in the SetLevelPositions method
            // reference that when changing Vector position for each character
            // comment the bottom line out of if you wish to just add the prefab on the scene
            Object varPrefab = Resources.Load("Prefabs/Characters/red", typeof(GameObject));
            Instantiate(varPrefab, new Vector3(0, 0, 0), Quaternion.identity);
             Object varPrefab2 = Resources.Load("Prefabs/Characters/blue", typeof(GameObject));
            Instantiate(varPrefab2, new Vector3(3, 0, 0), Quaternion.identity);
            //  Object varPrefab3 = Resources.Load("Prefabs/Characters/blek", typeof(GameObject));
            // Instantiate(varPrefab3, new Vector3(0, 3, 0), Quaternion.identity);
            //  Object varPrefab4 = Resources.Load("Prefabs/Characters/green", typeof(GameObject));
            // Instantiate(varPrefab4, new Vector3(0, 3, 0), Quaternion.identity);
            
        }
    }
}
