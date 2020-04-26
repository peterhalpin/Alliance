using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoObject : MonoBehaviourPun
{
    public int level;
    private Dictionary<string, string> players;

    private void Awake() {
        players = new Dictionary<string, string>();
    }

    private void Start() {
        DontDestroyOnLoad(transform.gameObject);
    }

    // gets called in the waiting room controller to update the player corresponding with their character
    // stored in the infoObject because it gameSetupController uses this to assign players to characters each time
    // otherwise keeping players from being destroyed would be too much data to pass/impossible
    public void UpdatePlayerList(Dictionary<string, string> map) {
        players = map;
    }

    public Dictionary<string, string> GetCharacters() {
        return players;
    }

}
