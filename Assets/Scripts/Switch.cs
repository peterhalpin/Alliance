using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


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
    private string id;

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
        //changes switch to "on"
        Debug.Log("Test2");
        gameObject.GetComponent<SpriteRenderer>().sprite = SwitchOn.GetComponent<SpriteRenderer>().sprite;
        Debug.Log("Game ID"+ gameObject.GetInstanceID());
        id = gameObject.name;
         
        if("TL_P_Switch" == id) {
         Tilemap ty = tilemapses["TL_Wall"];
         Debug.Log("Tilemap" + ty.name);
         ty.ClearAllTiles();
        } else if("TR_P_Switch" == id) {
         Tilemap ty = tilemapses["BR_Wall"];
         Debug.Log("Tilemap" + ty.name);
         ty.ClearAllTiles();
        } else if ("BL_NP_Switch" == id){
         Tilemap ty = tilemapses["C_Wall"];
         Debug.Log("Tilemap" + ty.name);
         ty.ClearAllTiles();
        }
        
        print("Delete Wall");
        
        isOn = true;
    }
}
