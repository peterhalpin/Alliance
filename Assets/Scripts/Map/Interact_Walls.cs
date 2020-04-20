using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Interact_Walls : MonoBehaviour{
private Dictionary<string, Tilemap> tilemapses = new Dictionary<string, Tilemap>();
private TileBase dirtTile;
private TileBase fireTile;
private TileBase brickTile;

private TileBase grassTile;
private bool isDestroyed;
private void Awake() {

        isDestroyed = false;

    //filling out dictionary of all tilemaps
        var tilemaps = FindObjectsOfType<Tilemap>();
          for(int i = 0 ; i < tilemaps.Length ; i++ ){
            tilemapses.Add(tilemaps[i].name, tilemaps[i]);
          }
        
        var tilemapA = tilemapses["BG"];
        dirtTile =  tilemapA.GetTile(new Vector3Int(-38,12,0));
        fireTile = tilemapses["7F"].GetTile(new Vector3Int(27, -3,0));
        brickTile = tilemapses["1B"].GetTile(new Vector3Int(-27,9,0));
        grassTile = tilemapses["1G"].GetTile(new Vector3Int(30,9,0));
    }

void OnTriggerEnter2D(Collider2D player){

        var tilemapname = gameObject.name.Substring(1);


        if(player.name == "blue(Clone)" && tilemapname == "F" ){
            isDestroyed = true;
            tilemapses[gameObject.name].SwapTile(fireTile,dirtTile);
        }

        if(player.name == "blek(Clone)" && tilemapname == "B" ){
            isDestroyed = true;
            tilemapses[gameObject.name].SwapTile(brickTile,dirtTile);
        }

        if(player.name == "red(Clone)" && tilemapname == "G" ){
            isDestroyed = true;
            tilemapses[gameObject.name].SwapTile(grassTile,dirtTile);
        }
          
        }


}
  

