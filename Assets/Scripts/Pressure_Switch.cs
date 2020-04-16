using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

//IMPORTANT: Object names have to match EXACTLY with the strings provided. If they do not, the switches will not work.
public class Pressure_Switch : MonoBehaviour {
  
  [SerializeField]
  GameObject switchOn;
  [SerializeField]
  GameObject switchOff;
  public bool isOn = false;

    public GameObject SwitchOn { get => switchOn; set => switchOn = value; }
    public GameObject SwitchOff { get => switchOff; set => switchOff = value; }
    private Dictionary<string, Tilemap> tilemapses = new Dictionary<string, Tilemap>();
    private TileBase dirtTile;
    private TileBase wallTile;
    void Start()
    {
        //Set default appearance
         gameObject.GetComponent<SpriteRenderer>().sprite = SwitchOff.GetComponent<SpriteRenderer>().sprite;
         
         //List of tilemaps to easily reference
         var tilemaps = new Tilemap[3];
         tilemaps = FindObjectsOfType<Tilemap>();
          for(int i = 0 ; i < tilemaps.Length ; i++ ){
            tilemapses.Add(tilemaps[i].name, tilemaps[i]);
          }

          //Setting tiles to swith dirt/wall
          var tilemapA = tilemapses["Tilemap"];
          dirtTile =  tilemapA.GetTile(new Vector3Int(0,0,0));
          var tilemapB = tilemapses["TL_Wall"];
          wallTile = tilemapB.GetTile(new Vector3Int(-5,6,0));
    }

    //Switching the tiles of the wall to dirt when pressure is there
    void OnTriggerEnter2D(Collider2D col){
        if(col.name == "blek(Clone)"){
          gameObject.GetComponent<SpriteRenderer>().sprite = SwitchOn.GetComponent<SpriteRenderer>().sprite;
           var ty = tilemapses["TL_Wall"];
            ty.SwapTile(wallTile, dirtTile);
        }
        if(col.name == "Cube"){
          gameObject.GetComponent<SpriteRenderer>().sprite = SwitchOn.GetComponent<SpriteRenderer>().sprite;
           var ty = tilemapses["BR_Wall"];
            ty.SwapTile(wallTile, dirtTile);
        }
    }
   
   //Switching the tiles to wall when pressure is removed
     void OnTriggerExit2D(Collider2D col) {
              Debug.Log(col.name);
          if(col.name == "blek(Clone)"){
            gameObject.GetComponent<SpriteRenderer>().sprite = SwitchOff.GetComponent<SpriteRenderer>().sprite;
            isOn = false;
            var ty = tilemapses["TL_Wall"];
            ty.SwapTile(dirtTile, wallTile);
         }
         if(col.name == "Block"){
            gameObject.GetComponent<SpriteRenderer>().sprite = SwitchOff.GetComponent<SpriteRenderer>().sprite;
            isOn = false;
            var ty = tilemapses["BR_Wall"];
            ty.SwapTile(dirtTile, wallTile);
         }
     }
}
