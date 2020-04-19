using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatController : MonoBehaviour
{

    private string teamName;

    // Start is called before the first frame update
    void Start() {
        DontDestroyOnLoad(transform.gameObject);
    }

    public void SetTeamName(string name) {
        teamName = name;
    }

    public string GetTeamName() {
        return teamName;
    }



}
