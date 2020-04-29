using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerController : MonoBehaviour
{
    private static float timer;
    private float mins;
    private float secs;
    private string minutes;
    private string seconds;
    private GUIStyle style;
    
    //public static bool timeStarted = false;
    
    void Start()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        // print(timer);
    }

    void OnGUI(){
        style = new GUIStyle(GUI.skin.button);
        //style = new GUIStyle();
        style.fontSize = 24;
        
        mins = Mathf.Floor(timer / 60);
        secs = Mathf.RoundToInt(timer%60);

        if(mins < 10){
            minutes= "0" + mins.ToString();
        }
        else{
            minutes = mins.ToString();
        }
        if(secs < 10){
            seconds = "0" + Mathf.RoundToInt(secs).ToString();
        }
        else{
            seconds = Mathf.RoundToInt(secs).ToString();
        }
        
        GUI.Label(new Rect(10, 10, 150, 100), "Time: " + minutes + ":"+seconds, style);
        
    }    

    public string GetMinutes() {
        print(minutes);
        return minutes;
    }

    public string GetSeconds() {
        print(seconds);
        return seconds;
    }

    public float GetTime() {
        return timer;
    }

}
