using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

//Overall C# Notes
  /*No Hashmaps, Only Dictionary
  *Tilemaps =/= Game Objects
  *setting scopes of variables is complicated
  */

  //Pressure Switch, 
    //Have it continously check if the Box/Strength is touching  in the update function
      //if not then set the boolean value to false and have the tilemap the same
      //if it is then "hide" the collider 2D
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
        
          var tilemaps = new Tilemap[3];
          tilemaps = FindObjectsOfType<Tilemap>();
          
          for(int i = 0 ; i < tilemaps.Length ; i++ ){
            tilemapses.Add(tilemaps[i].name, tilemaps[i]);
          }
    }
    void OnTriggerEnter2D(Collider2D col) {
        //changes switch to "on" appearance
        gameObject.GetComponent<SpriteRenderer>().sprite = SwitchOn.GetComponent<SpriteRenderer>().sprite;

        string id = gameObject.name;
        Tilemap ty;
        Debug.Log(col.name);
        if("TL_P_Switch" == id) {
         ty =  tilemapses["TL_Wall"];
        //  Debug.Log("Tilemap" + ty.name);
         ty.ClearAllTiles();
        } else if("TR_P_Switch" == id) {
         ty = tilemapses["BR_Wall"];
        //  Debug.Log("Tilemap" + ty.name);
         ty.ClearAllTiles();
        } else if ("BL_NP_Switch" == id){
         ty = tilemapses["C_Wall"];
        //  Debug.Log("Tilemap" + ty.name);
         ty.ClearAllTiles();
        }
        
        isOn = true;
    }
}
