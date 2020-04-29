using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using UnityEngine;

public class LogHandler : MonoBehaviour
{

    private string path;
    private InfoObject infoObject;
    private ChatController chatController;

    public void Awake()
    {
        infoObject = GameObject.FindObjectOfType<InfoObject>();   
        chatController = GameObject.FindObjectOfType<ChatController>();
        DontDestroyOnLoad(transform.gameObject);            //Preserves the gameobject and script throughout the game. 
    }
    
    //Create new file for each day. 
    public void CreateText(string teamName)
    {

        //Path of the file Assets/Resources/ChatLogs/chat{date}.txt
        string fileName = chatController.GetTeamName() + "_Chat&GameData_" + DateTime.Now.ToString("yyyy-MM-dd") + "_" +DateTime.Now.ToString("h;mm;ss_tt") + ".txt";
        infoObject.fileName = fileName;
        path = Application.dataPath + "/Resources/ChatLogs/" + fileName;

        if (!File.Exists(path))     //create new file if it doesnt exist
        {
            File.WriteAllText(path, "Team: " + teamName + "\n\n");      //this method would erase any existing text, N/A in this scenario
        } 
        else                        // File already exists, create new section header
        {
            File.AppendAllText(path, "\n\n" + "Team: " + teamName + "\n\n");        //Adds to bottom of existing file
        }

    }

    public void LogMessage(string user, string message)
    {
        File.AppendAllText(path, user + ": " + message + "\n" + "   " + DateTime.Now.ToString("h:mm:ss tt") + "\n" );
        
    }

    public void DeleteFile(string fileName) {
        File.Delete(Application.dataPath + "/Resources/ChatLogs/" + fileName);
    }

}
