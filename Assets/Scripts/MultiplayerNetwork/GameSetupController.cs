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
    Vector3[] playerPosition;

    // This script will be added to any multiplayer scene
    private void Awake() {
        infoObject = GameObject.FindObjectOfType<InfoObject>();
        players = infoObject.GetCharacters();
        level = infoObject.GetLevel();
        playerPosition = new Vector3[4];
    }

    void Start() {       
        SetLevelPositions(); // changing the starting positions of each character based on their levels
        CreatePlayer(); // Create a networked player object for each player that loads into the multiplayer scenes.
    }

    private void SetLevelPositions() {
        // will need to add more conditionals as you add more levels and want players to spawn in different positions
        // POSITIONS:
        // 0 == blek
        // 1 == blue
        // 2 == green
        // 3 == red
        // values passed into Vector3 need to be integers, otherwise it won't work
        // there might be a work around that though ^
        if(level == 1) {
            playerPosition[0] = new Vector3(-9, 6, 100);
            playerPosition[1] = new Vector3(9, -6, 100);
            playerPosition[2] = new Vector3(9, 6, 100);
            playerPosition[3] = new Vector3(-9, -6, 100);
        } else if (level == 2) {
            playerPosition[0] = new Vector3(3, 3, 100);
            playerPosition[1] = new Vector3(-9, 2, 100);
            playerPosition[2] = new Vector3(0, 3, 100);
            playerPosition[3] = new Vector3(9, -6, 100);
        } else if (level == 3) {
            playerPosition[0] = new Vector3(-26, 8, 100);
            playerPosition[1] = new Vector3(-26, 2, 100);
            playerPosition[2] = new Vector3(-26, -7, 100);
            playerPosition[3] = new Vector3(-26, -13, 100);
        } else if (level == 4) {
            playerPosition[0] = new Vector3(0, 0, 100);
            playerPosition[1] = new Vector3(0, 0, 100);
            playerPosition[2] = new Vector3(0, 0, 100);
            playerPosition[3] = new Vector3(0, 0, 100);
        } else {
            Debug.LogError("Error setting player positions!");
        }
        
    }

    private void CreatePlayer() {  
        string userID = PhotonNetwork.AuthValues.UserId;
        if (players[userID] == "blek") {
            PhotonNetwork.Instantiate(Path.Combine("Prefabs", "blek"), playerPosition[0], Quaternion.identity);
        } else if (players[userID] == "blue"){
            PhotonNetwork.Instantiate(Path.Combine("Prefabs", "blue"), playerPosition[1], Quaternion.identity);
        } else if (players[userID] == "green"){
            PhotonNetwork.Instantiate(Path.Combine("Prefabs", "green"), playerPosition[2], Quaternion.identity);
        } else {
            PhotonNetwork.Instantiate(Path.Combine("Prefabs", "red"), playerPosition[3], Quaternion.identity);
        }
    }

}
