using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public static float timer;
    public float mins;
    public float secs;
    public string minutes;
    public string seconds;
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
    }

    void OnGUI(){
        style = new GUIStyle(GUI.skin.button);
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
}
