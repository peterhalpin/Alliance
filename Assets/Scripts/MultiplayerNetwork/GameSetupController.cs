using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameSetupController : MonoBehaviourPun
{

    private InfoObject infoObject;
    private Dictionary<string, string> players;
    
    // This script will be added to any multiplayer scene

    private void Awake() {
        infoObject = GameObject.FindObjectOfType<InfoObject>();
        players = infoObject.GetCharacters();
    }

    void Start()
    {
        CreatePlayer(); // Create a networked player object for each player that loads into the multiplayer scenes.
    }

    private void CreatePlayer()
    {  
        string userID = PhotonNetwork.AuthValues.UserId;

     
        if (players[userID] == "blek") {
            PhotonNetwork.Instantiate(Path.Combine("Prefabs", "blek"), new Vector3(-9, 6, 1), Quaternion.identity);
        } else if (players[userID] == "blue"){
            PhotonNetwork.Instantiate(Path.Combine("Prefabs", "blue"), new Vector3(9, -6, 1), Quaternion.identity);
        } else if (players[userID] == "green"){
            PhotonNetwork.Instantiate(Path.Combine("Prefabs", "green"), new Vector3(9, 6, 1), Quaternion.identity);
        } else {
            PhotonNetwork.Instantiate(Path.Combine("Prefabs", "red"), new Vector3(-9, -6, 1), Quaternion.identity);
        }


        
        

        
    }


}
