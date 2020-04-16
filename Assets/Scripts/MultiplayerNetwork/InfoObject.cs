using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoObject : MonoBehaviourPun
{
    private PhotonView myPhotonView;    
    private Dictionary<string, string> players;
    public int level;

    private void Awake() {
        myPhotonView = GetComponent<PhotonView>();
        players = new Dictionary<string, string>();
        // level = 0;
    }

    private void Start() {
        DontDestroyOnLoad(transform.gameObject);
        
    }

    public void UpdatePlayerList(Dictionary<string, string> map) {
        players = map;
    }

    public Dictionary<string, string> GetCharacters() {
        return players;
    }

    public int GetLevel() {
        return level;
    }

    public void UpdateLevel(bool increaseOrDecreaseLevelCount) {
        Debug.Log("before: " + level);
        //if its true then increase, if false then decrease
        // myPhotonView.RPC("UpdateLevelForEveryone", RpcTarget.All, increaseOrDecreaseLevelCount);
        if(increaseOrDecreaseLevelCount) {
            level++;
        Debug.Log("after: " + level);
        } else {
            // this is here in the case we want to implement going back a level, but won't need for now, maybe in future versions
            // for whoever picks this project up
            level--;
        }
    }

    public void GoToMainMenu() {
        // this updates the level 0 because we're going to the main menu
        level = 0;
    }

}
