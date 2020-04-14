using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoObject : MonoBehaviour
{
    private Dictionary<string, string> players;
    private int level;

    private void Awake() {
        players = new Dictionary<string, string>();
        level = 1;
        DontDestroyOnLoad(transform.gameObject);
    }

    public void UpdatePlayerList(Dictionary<string, string> map) {
        players = map;
    }

    public Dictionary<string, string> GetCharacters() {
        return players;
    }

    public void UpdateLevel(bool increaseOrDecreaseLevelCount) {
        //if its true then increase, if false then decrease
        if(increaseOrDecreaseLevelCount) {
            level++;
        } else {
            // this is here in the case we want to implement going back a level, but we won't need it for now
            level--;
        }
    }

    public int GetLevel() {
        return level;
    }

}
