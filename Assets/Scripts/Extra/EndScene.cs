using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class EndScene : MonoBehaviour
{

    [SerializeField]
    private Text _time;
    private string minutes;
    private string seconds;

    private TimerController timerController;
    private InfoObject infoObject;
    private ChatHandler chatHandler; 
    private GameData gameData;


    private void Awake() {
        try {
            timerController = GameObject.FindObjectOfType<TimerController>();
            minutes = timerController.GetMinutes();
            seconds = timerController.GetSeconds();
            infoObject = GameObject.FindObjectOfType<InfoObject>();   
            chatHandler = GameObject.FindObjectOfType<ChatHandler>();   
            gameData = GameData.FindObjectOfType<GameData>();
        }
        catch {
            Debug.Log("Cannot find timer, must be because you are testing and are not loading the game from tutorial.");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _time.text = "Finished in: " + minutes + ":" + seconds;
    }

    public void SendData() {
        // this is where we write the code to send all of the game data to the database
    }

    public void OnClick() {
        SendData();
        infoObject.GoToMainMenu();
        Destroy(infoObject.gameObject);
        Destroy(timerController.gameObject);
        Destroy(chatHandler.gameObject);
        Destroy(gameData.gameObject);
        SceneManager.LoadScene(0);
        PhotonNetwork.LeaveRoom(true);
        Debug.Log("Going back to the main menu!");
    }

}
