using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Interact_Walls : MonoBehaviourPunCallbacks
{
private Dictionary<string, Tilemap> tilemapses = new Dictionary<string, Tilemap>();
private TileBase dirtTile;
private TileBase fireTile; 
private TileBase brickTile;

private TileBase grassTile;
private bool isDestroyed;

private PhotonView myPhotonView;
private bool testing;

private void Awake() {
    try {
        myPhotonView = GetComponent<PhotonView>();
        testing = false;
        if(myPhotonView == null) {
            testing = true;
        }
    } catch {
        print("not connected to network, probably testing.");
        testing = true;
    }
    // print(testing);

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
        if(!testing) {
            myPhotonView.RPC("UpdateTilesOnline", RpcTarget.All, player.name, tilemapname, gameObject.name);
        } else {
            UpdateTilesOnline(player.name, tilemapname, gameObject.name);
        }
          
}

[PunRPC]
private void UpdateTilesOnline(string playerName, string tileMapName, string gameObjectName) {
  if(playerName == "blue(Clone)" && tileMapName == "F" ){
        isDestroyed = true;
        tilemapses[gameObjectName].SwapTile(fireTile,dirtTile);
    }

    if(playerName == "blek(Clone)" && tileMapName == "B" ){
        isDestroyed = true;
        tilemapses[gameObjectName].SwapTile(brickTile,dirtTile);
    }

    if(playerName == "red(Clone)" && tileMapName == "G" ){
        isDestroyed = true;
        tilemapses[gameObjectName].SwapTile(grassTile,dirtTile);
    }

}


}
  

