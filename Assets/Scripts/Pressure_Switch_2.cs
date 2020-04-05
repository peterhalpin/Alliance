using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class Pressure_Switch_2 : MonoBehaviour
{
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
    // Start is called before the first frame update
    void Start()
    {
          //Set default appearance
         gameObject.GetComponent<SpriteRenderer>().sprite = SwitchOff.GetComponent<SpriteRenderer>().sprite;
         
         //List of tilemaps to easily reference
         var tilemaps = new Tilemap[11];
         tilemaps = FindObjectsOfType<Tilemap>();
          for(int i = 0 ; i < tilemaps.Length ; i++ ){
            tilemapses.Add(tilemaps[i].name, tilemaps[i]);
          }
          

          //Setting tiles to swith dirt/wall
          var tilemapA = tilemapses["Ground"];
          //Use Mouse Pointer Tool to click on tile and get coordinates
          dirtTile =  tilemapA.GetTile(new Vector3Int(-38,12,0));
          var tilemapB = tilemapses["BL_Wall"];  
          wallTile = tilemapB.GetTile(new Vector3Int(-25,12,0));
          
        
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D col){
        if(col.name == "Rock"){
          gameObject.GetComponent<SpriteRenderer>().sprite = SwitchOn.GetComponent<SpriteRenderer>().sprite;
           var ty = tilemapses["MT_Wall"];
            ty.SwapTile(wallTile, dirtTile);
        }
        if(col.name == "Cube"){
          gameObject.GetComponent<SpriteRenderer>().sprite = SwitchOn.GetComponent<SpriteRenderer>().sprite;
           var ty = tilemapses["MB_Wall"];
            ty.SwapTile(wallTile, dirtTile);
        }
        if(col.name == "ICE"){
          gameObject.GetComponent<SpriteRenderer>().sprite = SwitchOn.GetComponent<SpriteRenderer>().sprite;
           var ty = tilemapses["BL_Wall"];
            ty.SwapTile(wallTile, dirtTile);
        }
        if(col.name == "FIRE"){
          gameObject.GetComponent<SpriteRenderer>().sprite = SwitchOn.GetComponent<SpriteRenderer>().sprite;
           var ty = tilemapses["BR_Wall"];
            ty.SwapTile(wallTile, dirtTile);
        }
    }

    void OnTriggerExit2D(Collider2D col) {
              Debug.Log(col.name);
          if(col.name == "Rock"){
            gameObject.GetComponent<SpriteRenderer>().sprite = SwitchOff.GetComponent<SpriteRenderer>().sprite;
            isOn = false;
            var ty = tilemapses["MT_Wall"];
            ty.SwapTile(dirtTile, wallTile);
         }
         if(col.name == "Cube"){
            gameObject.GetComponent<SpriteRenderer>().sprite = SwitchOff.GetComponent<SpriteRenderer>().sprite;
            isOn = false;
            var ty = tilemapses["MB_Wall"];
            ty.SwapTile(dirtTile, wallTile);
         }
         if(col.name == "ICE"){
            gameObject.GetComponent<SpriteRenderer>().sprite = SwitchOff.GetComponent<SpriteRenderer>().sprite;
            isOn = false;
            var ty = tilemapses["BL_Wall"];
            ty.SwapTile(dirtTile, wallTile);
         }
         if(col.name == "FIRE"){
            gameObject.GetComponent<SpriteRenderer>().sprite = SwitchOff.GetComponent<SpriteRenderer>().sprite;
            isOn = false;
            var ty = tilemapses["BR_Wall"];
            ty.SwapTile(dirtTile, wallTile);
         }
     }
}
