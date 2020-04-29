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



    [SerializeField]
    public int multiplayerSceneIndex;
    [SerializeField]
    public int menuSceneIndex;
    [SerializeField]
    public Text roomCountDisplay;
    [SerializeField]
    public Text timerToStartDisplay;
    [SerializeField]
    public int roomSize;


    private PhotonView myPhotonView;
    private int playerCount;


    
    //bool values for if the timer can count down and if the game is starting
    private bool readyToStart;
    private bool startingGame;

    //countdown timer variables
    private float timerToStartGame;
    private float fullGameTimer;

    //countdown timer to reset variables
    [SerializeField]
    public float maxFullGameWaitTime;
    private List<string> playerslist;
    private Dictionary<string, string> map;
    private Queue<string> charTypes;

    // these two objects are prefabs that are in the scene
    // its how you transfer variables/data from one object to the next
    private InfoObject infoObject;
    private LogHandler logHandler;
    private ChatHandler chatHandler;
    private ChatController chatController;

    // Start is called before the first frame update
    private void Awake() {
        playerslist = new List<string>();
        map = new Dictionary<string, string>();
        charTypes = new Queue<string>();
        infoObject = GameObject.FindObjectOfType<InfoObject>();
        logHandler = GameObject.FindObjectOfType<LogHandler>();
        chatHandler = GameObject.FindObjectOfType<ChatHandler>();
        chatController = GameObject.FindObjectOfType<ChatController>();
    }

    private void Start()
    {
        myPhotonView = GetComponent<PhotonView>();
        fullGameTimer = maxFullGameWaitTime;
        timerToStartGame = maxFullGameWaitTime;
        charTypes.Enqueue("blek");
        charTypes.Enqueue("blue");
        charTypes.Enqueue("red");
        charTypes.Enqueue("green");
        PlayerCountUpdate();
    }

    private void Update() 
    {
        WaitingForMorePlayers();
    }

    private void WaitingForMorePlayers() {
        // if there is only one player in the room the timer will stop and reset
        if (playerCount <= 1) {
            ResetTimer();
        } 
        // when there is enough players in the room the start timer will begin countdown
        if (readyToStart) {
            fullGameTimer -= Time.deltaTime;
            timerToStartGame = fullGameTimer;
        }
        // format and display countdown timer
        string tempTimer = string.Format("{0:00}", timerToStartGame);
        timerToStartDisplay.text = tempTimer;
        // if the countdown timer reaches 0 the game will then start
        if(timerToStartGame <= 1f) {
            // if less than a second create random username
            // this if statement and not below because not all screens sync to the exact 0 time
            if (chatHandler.playerName.text == "") {
                chatHandler.ConnectRandomUserName();
            }
        }
        if (timerToStartGame <= 0f) {
            if (startingGame) {
                return;
            }
            StartGame();
        }
    }

    private void PlayerCountUpdate()
    {
        //updates player count when players join the room
        // displays player count
        // triggers countdown timer
        playerCount = PhotonNetwork.PlayerList.Length;

        roomSize = PhotonNetwork.CurrentRoom.MaxPlayers;
        //roomSize = 2; // this is just for testing
        
        roomCountDisplay.text = playerCount + ":" + roomSize;
        if(playerCount == roomSize) {
            readyToStart = true;
            // infoObject.UpdateLevel(true);
        } 
        else {
            readyToStart = false;
            // if(infoObject.GetLevel() == 1) {
            //     infoObject.UpdateLevel(false);
            // }
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        // called whenever a new player joins the room
        PlayerCountUpdate();
        // send master clients countdown timer to all other players in order to sync
        if(PhotonNetwork.IsMasterClient) {
            Queue<string> charReload = new Queue<string>(new[] {"blek", "blue", "red", "green"});
            charTypes = charReload;
            // adding player id's to list of players variable: playerslist
            playerslist.Clear();
            foreach(Player pl in PhotonNetwork.PlayerList) {
                playerslist.Add(pl.UserId);
            }
            ChooseCharacters(playerslist, charTypes);         
            myPhotonView.RPC("RPC_SendTimer", RpcTarget.All, timerToStartGame);
        }

    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        // called whenever a player leaves the room
        PlayerCountUpdate();
    }
    
    private void ResetTimer()
    {
        // resets the count down timer
        timerToStartGame = maxFullGameWaitTime;
        // notFullGameTimer = maxWaitTime;
        fullGameTimer = maxFullGameWaitTime;
    }

    private void StartGame()
    { // multiplayer scene is loaded to start the game
        startingGame = true;
        if (!PhotonNetwork.IsMasterClient) {
            return;
        }
        Destroy(chatController.gameObject);        
        // closes the current room so now one else joins
        PhotonNetwork.CurrentRoom.IsOpen = false;
        //only the master client calls this because the method itself loads it for all players because we enabled PhotonNetwork.AutomaticallySyncScene for this game
        PhotonNetwork.LoadLevel(multiplayerSceneIndex);
    }

    public void DelayCancel()
    {
        // public function paired to cancel button in waiting room scene
        chatHandler.DisconnectFromChat();
        Destroy(infoObject.gameObject);
        Destroy(chatController.gameObject);
        Destroy(chatHandler.gameObject);        
        Destroy(logHandler.gameObject);
        PhotonNetwork.LeaveRoom(true);
        SceneManager.LoadScene(menuSceneIndex);
    }

    public void ChooseCharacters(List<string> players, Queue<string> types) {
        // shuffles the characters
        var listTypes = types.ToList();
        listTypes = Shuffle(listTypes);
        types = new Queue<string>(listTypes);
        // shuffles the player IDs
        players = Shuffle(players);
        map.Clear();
        foreach(string p in players) {
            map.Add(p, types.Dequeue());
        }
        myPhotonView.RPC("CharacterRemoteAssign", RpcTarget.All, map);
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

    [PunRPC]
    private void CharacterRemoteAssign(Dictionary<string, string> chrs) {
        map = chrs;
        infoObject.UpdatePlayerList(chrs);
    }

    [PunRPC]
    private void RPC_SendTimer(float timeIn) {
        //RPC for syncing the countdown timer to those that join after it has started
        timerToStartGame = timeIn;
        // notFullGameTimer = timeIn;
        if(timeIn < fullGameTimer)
        {
            fullGameTimer = timeIn;
        }
    }
}
