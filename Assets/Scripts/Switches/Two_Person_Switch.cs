using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class Two_Person_Switch : MonoBehaviour
{
private Dictionary<string, Tilemap> tilemapses = new Dictionary<string, Tilemap>();
   
  private List<Collider2D> colliders = new List<Collider2D>();
  private TileBase dirtTile;
    private TileBase wallTile;

void Start(){
  var tilemaps = new Tilemap[11];
         tilemaps = FindObjectsOfType<Tilemap>();
          for(int i = 0 ; i < tilemaps.Length ; i++ ){
            tilemapses.Add(tilemaps[i].name, tilemaps[i]);
          }
           var tilemapA = tilemapses["Ground"];
          //Use Mouse Pointer Tool to click on tile and get coordinates
          dirtTile =  tilemapA.GetTile(new Vector3Int(-38,12,0));
          var tilemapB = tilemapses["BL_Wall"];  
          wallTile = tilemapB.GetTile(new Vector3Int(-25,12,0));
}
    void OnTriggerEnter2D(Collider2D player)
    {
         
        if(!colliders.Contains(player)){
            colliders.Add(player);
        }
        if(colliders.Count > 1){
            //make doors open
            print("Bottom Gate open");
        var tilemapA = tilemapses["TL_Wall"];
        var tilemapB = tilemapses["TR_Wall"];
        tilemapA.SwapTile(wallTile, dirtTile);
        tilemapB.SwapTile(wallTile, dirtTile);

          
        }

    }

    void OnTriggerExit2D(Collider2D player){
        colliders.Remove(player);
    }
}
