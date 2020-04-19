using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Two_Person_Switch2 : MonoBehaviour
{
   private List<Collider2D> colliders;
    private bool isDestroyed;

    private Dictionary<string, Tilemap> tilemapses = new Dictionary<string, Tilemap>();
    private TileBase dirtTile;
    private TileBase wallTile;

    private void Awake() {
        colliders = new List<Collider2D>();
        isDestroyed = false;
        var tilemaps = FindObjectsOfType<Tilemap>();
          for(int i = 0 ; i < tilemaps.Length ; i++ ){
            tilemapses.Add(tilemaps[i].name, tilemaps[i]);
          }
        
        dirtTile =  tilemapses["BG"].GetTile(new Vector3Int(-38,12,0));
        wallTile = tilemapses["C_Switch_Wall"].GetTile(new Vector3Int(-19, 8 ,0));
    }
    
    void OnTriggerEnter2D(Collider2D player)
    {
        if(!colliders.Contains(player)){
            colliders.Add(player);
        }

        if(colliders.Count > 1 && !isDestroyed){
            isDestroyed = true;
            tilemapses["C_Switch_Wall"].SwapTile(wallTile,dirtTile);
            //make doors open

        }

    }

    void OnTriggerExit2D(Collider2D player){
        colliders.Remove(player);
    }
}
