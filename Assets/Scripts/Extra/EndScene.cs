using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;


public class EndScene : MonoBehaviourPun
{

    [SerializeField]
    public Text _time;
    private string minutes;
    private string seconds;

    private Email mail;
    private InfoObject infoObject;
    private ChatHandler chatHandler; 
    private GameData gameData;
    private LogHandler logHandler;
    private TimerController timerController;


    private void Awake() {
        if(PhotonNetwork.IsConnected) {
            mail = GameObject.FindObjectOfType<Email>();
            gameData = GameObject.FindObjectOfType<GameData>();
            logHandler = GameObject.FindObjectOfType<LogHandler>();
            infoObject = GameObject.FindObjectOfType<InfoObject>();   
            chatHandler = GameObject.FindObjectOfType<ChatHandler>();   
            timerController = GameObject.FindObjectOfType<TimerController>();
        }
    }

    private void Start() {
        // show time taken to complete game
        if(PhotonNetwork.IsConnected) {
            minutes = timerController.GetMinutes();
            seconds = timerController.GetSeconds();
            _time.text = "Finished in: " + minutes + ":" + seconds;
            mail.SendEmail(infoObject.fileName); // send email
            logHandler.DeleteFile(infoObject.fileName); // delete the data file
        }
    }

    // don't really need this if we're sending email 
    public void SendData() {
        // this is where we write the code to send all of the game data to the database
        // if new team makes a database and uses that then they would want to probably use this
        // not enough time so we just decided to send emails with some data
        Debug.Log(gameData.GetFullLog());;
    }

    public void OnClick() {
        if(PhotonNetwork.IsConnected) {
            Destroy(gameData.gameObject);
            Destroy(logHandler.gameObject);
            Destroy(infoObject.gameObject);
            Destroy(chatHandler.gameObject);
            Destroy(timerController.gameObject);
            SceneManager.LoadScene(0);
            PhotonNetwork.LeaveRoom(true);
        } else {
            SceneManager.LoadScene(0);            

        }
        Debug.Log("Going back to the main menu!");
    }

}
