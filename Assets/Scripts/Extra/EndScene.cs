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
    public InfoObject infoObject;


    private void Awake() {
        try {
            timerController = GameObject.FindObjectOfType<TimerController>();
            minutes = timerController.GetMinutes();
            seconds = timerController.GetSeconds();
            infoObject = GameObject.FindObjectOfType<InfoObject>();       
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

    public void OnClick() {
        infoObject.GoToMainMenu();
        Destroy(infoObject.gameObject);
        Destroy(timerController.gameObject);
        SceneManager.LoadScene(0);
        PhotonNetwork.LeaveRoom(true);
        Debug.Log("Going back to the main menu!");
    }

}
