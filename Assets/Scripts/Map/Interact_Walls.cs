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
private void Awake() {
        isDestroyed = false;
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
    private bool isDestroyed;

void OnTriggerEnter2D(Collider2D player){

        var tilemapname = gameObject.name.Substring(1);

        if(player.name == "blue" && tilemapname == "F" ){
            isDestroyed = true;
            tilemapses[gameObject.name].SwapTile(fireTile,dirtTile);
        }

        if(player.name == "blek" && tilemapname == "B" ){
            isDestroyed = true;
            tilemapses[gameObject.name].SwapTile(brickTile,dirtTile);
        }

        if(player.name == "red" && tilemapname == "G" ){
            isDestroyed = true;
            tilemapses[gameObject.name].SwapTile(grassTile,dirtTile);
        }
          
            //make doors open
            
            // GameObject[] bottomDoor = GameObject.FindGameObjectsWithTag("Door Bottom");
            // Destroy(bottomDoor[0]);
            // Destroy(bottomDoor[1]);
        }


}
  

