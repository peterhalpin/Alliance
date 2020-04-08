using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;

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




    // Start is called before the first frame update
    private void Start()
    {
        myPhotonView = GetComponent<PhotonView>();
        fullGameTimer = maxFullGameWaitTime;
        notFullGameTimer = maxWaitTime;
        timerToStartGame = maxWaitTime;

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
        }
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
