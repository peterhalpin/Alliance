using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

//IMPORTANT: Object names have to match EXACTLY with the strings provided. If they do not, the switches will not work.

public class Pressure_Switch_2 : MonoBehaviour
{
      [SerializeField]
  GameObject switchOn;
[SerializeField]
  GameObject switchOff;

    //State of all switches 
      /*
      *ison1 -> P1
      *ison2 -> P2
      *ison3 -> P3_A
      *ison4 -> P3_B
      */
    public bool ison1 = false;
    public bool ison2 = false;
    public bool ison3 = false;
    public bool ison4 = false;
    public GameObject SwitchOn { get => switchOn; set => switchOn = value; }
    public GameObject SwitchOff { get => switchOff; set => switchOff = value; }
    private Dictionary<string, Tilemap> tilemapses = new Dictionary<string, Tilemap>();
    private Dictionary<string, GameObject> switches_dict = new Dictionary<string, GameObject>();
    private TileBase dirtTile;
    private TileBase wallTile;

//No idea why the dictionary keeps throwiwng an error at me :/
    private Tilemap MB_Wall;
    // Start is called before the first frame update
    void Start()
    {
          //Set default appearance
         gameObject.GetComponent<SpriteRenderer>().sprite = SwitchOff.GetComponent<SpriteRenderer>().sprite;
         
         //List of tilemaps to easily reference
         var tilemaps = new Tilemap[10];
         tilemaps = FindObjectsOfType<Tilemap>();
          for(int i = 0 ; i < tilemaps.Length ; i++ ){
            tilemapses.Add(tilemaps[i].name, tilemaps[i]);
          }
          MB_Wall = tilemapses["MB_Wall"];

          //Adds All Switches so I don't have to spend time searching 
          switches_dict.Add("P1",GameObject.Find("P1"));
          switches_dict.Add("P2",GameObject.Find("P2"));
          switches_dict.Add("PA",GameObject.Find("P3_A"));
          switches_dict.Add("PB",GameObject.Find("P3_B"));
          
          //Setting tiles to swith dirt/wall
          //Use Mouse Pointer Tool to click on tile and get coordinates
          var tilemapA = tilemapses["Ground"];
          
          dirtTile =  tilemapA.GetTile(new Vector3Int(-38,12,0));
          var tilemapB = tilemapses["BL_Wall"];  
          wallTile = tilemapB.GetTile(new Vector3Int(-25,12,0));
          
        
    }

  

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D col){
        if(col.name == "Rock"){
          gameObject.GetComponent<SpriteRenderer>().sprite = SwitchOn.GetComponent<SpriteRenderer>().sprite;
          ison3 = true;
          if(switches_dict["PB"].GetComponent<Pressure_Switch_2>().ison4){
            //Odd Issue where if rock is on the switch first
            //there will be an error that the MB_wall tilemap can't be found
            //despite the ContainsKey method saying it is 
            //To circumvent this, I created a private value that holds the MB_Wall property
            //and used TryGetValue to catch errors
            //0 idea why this is happening.

            if (tilemapses.TryGetValue("MB_Wall", out MB_Wall)){
              var ty = tilemapses["MT_Wall"];
              ty.SwapTile(wallTile,dirtTile);
              MB_Wall.SwapTile(wallTile,dirtTile);
            }
            else
            { Debug.Log("Not found");
            }
        }
        }
        if(col.name == "Cube"){
          gameObject.GetComponent<SpriteRenderer>().sprite = SwitchOn.GetComponent<SpriteRenderer>().sprite;
          ison4 = true;
          if(switches_dict["PA"].GetComponent<Pressure_Switch_2>().ison3){
             //Checks for pressure switch with rock is on 
             Debug.Log(tilemapses.ContainsKey("MB_Wall"));
            var ty = tilemapses["MT_Wall"];
            ty.SwapTile(wallTile,dirtTile);
            var tz = tilemapses["MB_Wall"];
            tz.SwapTile(wallTile,dirtTile);
          }
          
        }
        if(col.name == "blue(Clone)"){
          gameObject.GetComponent<SpriteRenderer>().sprite = SwitchOn.GetComponent<SpriteRenderer>().sprite;
           ison2 = true;
            if(switches_dict["P1"].GetComponent<Pressure_Switch_2>().ison1){
               //Checks for pressure switch with fire person is on 
            var ty = tilemapses["BL_Wall"];
            ty.SwapTile(wallTile, dirtTile);
            var tz = tilemapses["BR_Wall"];
            tz.SwapTile(wallTile, dirtTile);}
        }
        if(col.name == "red(Clone)"){
          gameObject.GetComponent<SpriteRenderer>().sprite = SwitchOn.GetComponent<SpriteRenderer>().sprite;
           ison1 = true;
           if(switches_dict["P2"].GetComponent<Pressure_Switch_2>().ison2){
              //Checks for pressure switch with ice person is on 
             var ty = tilemapses["BL_Wall"];
            ty.SwapTile(wallTile, dirtTile);
            var tz = tilemapses["BR_Wall"];
            tz.SwapTile(wallTile, dirtTile); }
            
        }
    }

    void OnTriggerExit2D(Collider2D col) {
              Debug.Log(col.name);
          if(col.name == "Rock"){
            gameObject.GetComponent<SpriteRenderer>().sprite = SwitchOff.GetComponent<SpriteRenderer>().sprite;
            ison3 = false;
            var ty = tilemapses["MT_Wall"];
            ty.SwapTile(dirtTile, wallTile);
            var tz = tilemapses["MB_Wall"];
            tz.SwapTile(dirtTile, wallTile);
         }
         if(col.name == "Cube"){
            gameObject.GetComponent<SpriteRenderer>().sprite = SwitchOff.GetComponent<SpriteRenderer>().sprite;
            ison4 = false;
            var ty = tilemapses["MB_Wall"];
            ty.SwapTile(dirtTile, wallTile);
            var tz = tilemapses["MT_Wall"];
            tz.SwapTile(dirtTile, wallTile);
         }
         if(col.name == "blue(Clone)"){
            gameObject.GetComponent<SpriteRenderer>().sprite = SwitchOff.GetComponent<SpriteRenderer>().sprite;
            ison2 = false;
            var ty = tilemapses["BL_Wall"];
            ty.SwapTile(dirtTile, wallTile);
            var tz = tilemapses["BR_Wall"];
            tz.SwapTile(dirtTile, wallTile);
         }
         if(col.name == "red(Clone)"){
            gameObject.GetComponent<SpriteRenderer>().sprite = SwitchOff.GetComponent<SpriteRenderer>().sprite;
            ison1 = false;
            var ty = tilemapses["BR_Wall"];
            ty.SwapTile(dirtTile, wallTile);
            var tz = tilemapses["BL_Wall"];
            tz.SwapTile(dirtTile, wallTile);
         }
     }
}
