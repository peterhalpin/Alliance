﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{

    // privte string 
    // private Dictionary<string, string> map;

    private int logCount;
    private int doorOpenLogCount;
    private int onSwitchLogCount;
    private int offSwitchLogCount;
    private int finishLevelLogCount;

    private float timeToPressTopSwitch;
    private float timeToPressBottomSwitch;
    private float timeToGoToNextLevel;

    private Dictionary<string, float> gameInteraction;
    private Dictionary<string, float> levelCompletionTimes;
    private Dictionary<string, float> activatedSwitchTimes;
    private Dictionary<string, float> deactivatedSwitchTimes;
    private Dictionary<string, float> doorOpenTimes;


    

    private TimerController timerController;


    private void Awake() {
        gameInteraction = new Dictionary<string, float>();                
        levelCompletionTimes = new Dictionary<string, float>();        
        activatedSwitchTimes = new Dictionary<string, float>();        
        deactivatedSwitchTimes = new Dictionary<string, float>();        
        doorOpenTimes = new Dictionary<string, float>();
        logCount = 0;
        doorOpenLogCount = 0;
        onSwitchLogCount = 0;
        offSwitchLogCount = 0;
        finishLevelLogCount = 0;
    }

    private void Start()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    public void DoorOpen(string doorType, float time) {
        doorOpenLogCount++;
        doorOpenTimes.Add(doorOpenLogCount + ": " + doorType, time);
    }

    public void ActivateSwitchTime(string switchType, float time) {
        onSwitchLogCount++;
        activatedSwitchTimes.Add(onSwitchLogCount + ": " + switchType, time);
    }


    public void DeactivateSwitchTime(string switchType, float time) {
        offSwitchLogCount++;
        deactivatedSwitchTimes.Add(offSwitchLogCount + ": " + switchType, time);
    }
    

    public void FinishLevelTime(string level, float time) {
        finishLevelLogCount++;
        levelCompletionTimes.Add(finishLevelLogCount + ": " + level, time);

        // foreach(KeyValuePair<string, float> entry in gameInteraction) {
        //     print(entry);
        //     print(entry.Key);
        //     print(entry.Value);
        // }

    }


    public void GameInteraction(string interaction, float time) {
        // any game interaction is logged here
        logCount++;
        gameInteraction.Add(logCount + ": " + interaction, time);
    }

    // public Dictionary<string, float> GetFullLog() {
    //     return gameInteraction;
    // } 

    public string GetFullLog() {
        // the logs of when a player did something and the time is stored in a dictionary
        // here we need to go through it all and store it in a string so that we can send it to an email

        string fullLog = "";

        foreach(KeyValuePair<string, float> entry in gameInteraction) {
            fullLog += entry.Key + " at game time " + entry.Value + "\n\n";

        }

        return fullLog;

    }


    






}
