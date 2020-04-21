using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneDetector : MonoBehaviour
{

    public ChatHandler chatHandler;
    public LogHandler logHandler;
    private string sceneName;
    private Scene currentScene;
    private static float timer;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(transform.gameObject);

        currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;

        if (sceneName == "Lobby")
        {
            //print("sceneDetector lobby");
            chatHandler.PrintMessage("Welcome to the Lobby!");
        }
        else if (sceneName == "TutorialLevel")
        {
            timer = 0;
            chatHandler.PrintMessage("Welcome to Alliance! Use the arrow keys to move around.");
            logHandler.LogMessage("System", "Welcome to Alliance! Use the arrow keys to move around.");
        }
        else if (sceneName == "Level2")
        {
            timer = 0;
            chatHandler.PrintMessage("Welcome to Level 2!");
            logHandler.LogMessage("System", "Welcome to Level 2!");
        }
        else if (sceneName == "Level3")
        {
            timer = 0;
            chatHandler.PrintMessage("Welcome to Level 3!");
            logHandler.LogMessage("System", "Welcome to Level 2!");
        }
        else if (sceneName == "Level4")
        {
            timer = 0;
            chatHandler.PrintMessage("Welcome to Level 4!");
            logHandler.LogMessage("System", "Welcome to Level 2!");
        }

    }

    void Update()
    {
        timer += Time.deltaTime;

    }

    public string GetTime()
    {
        return Mathf.Floor(timer / 60) + ":" + Mathf.RoundToInt(timer % 60);

    }

    public string GetSceneName()
    {
        return sceneName;
    }

    


}
