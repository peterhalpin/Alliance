using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps; //ALWAYS IMPORT THIS WHEN WORKING WITH SWITCHES THAT INTERACT WITH WALLS

//Overall C# Notes
  /*No Hashmaps, Only Dictionary
  *Tilemaps =/= Game Objects
  *setting scopes of variables is complicated
  */

 
public class Switch : MonoBehaviour
{
[SerializeField]
  GameObject switchOn;
[SerializeField]
  GameObject switchOff;
    public bool isOn = false;

    public GameObject SwitchOn { get => switchOn; set => switchOn = value; }
    public GameObject SwitchOff { get => switchOff; set => switchOff = value; }
    private Dictionary<string, Tilemap> tilemapses = new Dictionary<string, Tilemap>();
  

    void Start() {
        // code to have the switch in default appearance
        gameObject.GetComponent<SpriteRenderer>().sprite = SwitchOff.GetComponent<SpriteRenderer>().sprite;
        
        //Easy reference to tilemaps in scene
          var tilemaps = new Tilemap[3];
          tilemaps = FindObjectsOfType<Tilemap>();
          for(int i = 0 ; i < tilemaps.Length ; i++ ){
            tilemapses.Add(tilemaps[i].name, tilemaps[i]);
          }
    }
    void OnTriggerEnter2D(Collider2D col) {
        //changes switch to "on" appearance
        gameObject.GetComponent<SpriteRenderer>().sprite = SwitchOn.GetComponent<SpriteRenderer>().sprite;
        
        //Remove Center Wall
        Tilemap ty;
        ty = tilemapses["C_Wall"];
        ty.ClearAllTiles();
        isOn = true;
    }
}
