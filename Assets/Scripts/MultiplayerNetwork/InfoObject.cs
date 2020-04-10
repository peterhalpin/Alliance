using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoObject : MonoBehaviour
{

    private Dictionary<string, string> players;
    private int level;

    private void Awake() {
        players = new Dictionary<string, string>();
        level = 0;
        DontDestroyOnLoad(transform.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
   
    }

    public void UpdatePlayerList(Dictionary<string, string> map) {
        players = map;

    }

    public Dictionary<string, string> GetCharacters() {
        return players;
    }

    public void UpdateLevel(bool increaseOrDecrease) {
        //if its true then increase, if false then decrease
        if(increaseOrDecrease) {
            level++;
            return;
        }
        level--;

    }

}
