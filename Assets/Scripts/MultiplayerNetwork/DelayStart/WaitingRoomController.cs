using Photon.Pun;
using Photon.Realtime;



using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


public class WaitingRoomController : MonoBehaviourPunCallbacks
{

    private PhotonView myPhotonView;


    [SerializeField]
    private int multiplayerSceneIndex;
    [SerializeField]
    private int menuSceneIndex;

    private int playerCount;
    private int roomSize;
    [SerializeField]
    private int minPlayersToStart;

    [SerializeField]
    private Text roomCountDisplay;
    [SerializeField]
    private Text timerToStartDisplay;
    
    //bool values for if the timer can count down
    private bool readyToCountDown;
    private bool readyToStart;
    private bool startingGame;

    //countdown timer variables
    private float timerToStartGame;
    private float notFullGameTimer;
    private float fullGameTimer;

    //countdown timer reset variables
    [SerializeField]
    private float maxWaitTime;
    [SerializeField]
    private float maxFullGameWaitTime;
   

    private List<string> playerslist;
    private Dictionary<string, string> map;
    private Queue<string> charTypes;

    private InfoObject infoObject;


    // Start is called before the first frame update
    private void Awake() {

        playerslist = new List<string>();
        map = new Dictionary<string, string>();

        charTypes = new Queue<string>();

        infoObject = GameObject.FindObjectOfType<InfoObject>();

    }

    private void Start()
    {
        myPhotonView = GetComponent<PhotonView>();
        fullGameTimer = maxFullGameWaitTime;
        notFullGameTimer = maxWaitTime;
        timerToStartGame = maxWaitTime;

        
        charTypes.Enqueue("blek");
        charTypes.Enqueue("blue");
        charTypes.Enqueue("red");
        charTypes.Enqueue("green");


        PlayerCountUpdate();
        
    }

    void PlayerCountUpdate()
    {
        //updates player count when players join the room
        // displays player count
        // triggers countdown timer
        playerCount = PhotonNetwork.PlayerList.Length;
    
        roomSize = PhotonNetwork.CurrentRoom.MaxPlayers;
        roomCountDisplay.text = playerCount + ":" + roomSize;

        if(playerCount == roomSize)
        {
            readyToStart = true;
        } 
        else if (playerCount >= minPlayersToStart)
        {
            readyToCountDown = true;
        }
        else
        {
            readyToCountDown = false;
            readyToStart = false;
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        // called whenever a new player joins the room
        PlayerCountUpdate();




        // send master clients countdown timer to all other players in order to sync
        if(PhotonNetwork.IsMasterClient)
        {
            myPhotonView.RPC("RPC_SendTimer", RpcTarget.Others, timerToStartGame);


             Queue<string> charReload = new Queue<string>(new[] {"blek", "blue", "red", "green"});
             charTypes = charReload;

            // adding player id's to list of players variable: playerslist
            foreach(Player pl in PhotonNetwork.PlayerList) {
                if(!playerslist.Contains(pl.UserId)) {
                    playerslist.Add(pl.UserId);
                }
            }
            ChooseCharacters(playerslist, charTypes);
          
        }

    }
    
    private List<string> Shuffle(List<string> list) {
        for (int i = 0; i < list.Count; i++) {
            string temp = list[i];
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
        return list;
    }

    public void ChooseCharacters(List<string> players, Queue<string> types) {

        // shuffles the characters
        var listTypes = types.ToList();
        listTypes = Shuffle(listTypes);
        types = new Queue<string>(listTypes);
        
        // shuffles the player IDs
        players = Shuffle(players);
       

        foreach(string p in players) {
            map.Add(p, types.Dequeue());
        }
        
        myPhotonView.RPC("CharacterRemoteAssign", RpcTarget.All, map);

    }

    [PunRPC]
    private void CharacterRemoteAssign(Dictionary<string, string> chrs) {
        map = chrs;
        infoObject.UpdatePlayerList(chrs);
    }

    [PunRPC]
    private void RPC_SendTimer(float timeIn)
    {
        //RPC for syncing the countdown timer to those that join after it has started
        timerToStartGame = timeIn;
        notFullGameTimer = timeIn;
        if(timeIn < fullGameTimer)
        {
            fullGameTimer = timeIn;
        }
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        // called whenever a player leaves the room
        PlayerCountUpdate();
    }
    
    private void Update() 
    {

        WaitingForMorePlayers();
    }

    void WaitingForMorePlayers()
    {
        // if there is only one player in the room the timer will stop and reset
        if (playerCount <= 1)
        {
            //i think this is where i put 4
            ResetTimer();
        } 
        // when there is enough players in the room the start timer will begin countdown
        if (readyToStart)
        {
            fullGameTimer -= Time.deltaTime;
            timerToStartGame = fullGameTimer;
        }
        else if (readyToCountDown)
        {
            notFullGameTimer -= Time.deltaTime;
            timerToStartGame = notFullGameTimer;
        }
        // format and display countdown timer
        string tempTimer = string.Format("{0:00}", timerToStartGame);
        timerToStartDisplay.text = tempTimer;
        // if the countdown timer reaches 0 the game will then start
        if (timerToStartGame <= 0f)
        {
            if (startingGame) {
                return;
            }
            StartGame();
        }
    }

    void ResetTimer()
    {
        // resets the count down timer
        timerToStartGame = maxWaitTime;
        notFullGameTimer = maxWaitTime;
        fullGameTimer = maxFullGameWaitTime;
    }

    void StartGame()
    { // multiplayer scene is loaded to start the game
        startingGame = true;
        if (!PhotonNetwork.IsMasterClient) {
            return;
        }
        PhotonNetwork.CurrentRoom.IsOpen = false;
        PhotonNetwork.LoadLevel(multiplayerSceneIndex);
    }

    public void DelayCancel()
    {
        // public function paired to cancel button in waiting room scene
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene(menuSceneIndex);
    }

    
   
}
