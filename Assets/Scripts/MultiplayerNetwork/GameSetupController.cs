using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameSetupController : MonoBehaviour
{
    // This script will be added to any multiplayer scene
    void Start()
    {
        CreatePlayer(); // Create a networked player object for each player that loads into the multiplayer scenes.
    }

    private void CreatePlayer()
    {
        Debug.Log("Creating Player");
        System.Random random = new System.Random();
        int num = random.Next(1, 3);
        if (num == 1) {
            PhotonNetwork.Instantiate(Path.Combine("Prefabs", "blek"), Vector3.zero, Quaternion.identity);
        } else {
            PhotonNetwork.Instantiate(Path.Combine("Prefabs", "blue"), Vector3.zero, Quaternion.identity);
        }

        // PhotonNetwork.Instantiate(Path.Combine("Prefabs", "blek"), Vector3.zero, Quaternion.identity);
    }

}
