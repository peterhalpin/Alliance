using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using Photon.Chat;
using System.Security.Cryptography.X509Certificates;

#if PHOTON_UNITY_NETWORKING
using Photon.Pun;
#endif



public class ChatHandler : MonoBehaviour, IChatClientListener
{
    private string chatChannelName;

    public ChatClient myChatClient;         //main "connection point" to chat API
    public ChatChannel channel;
    public InputField playerName;
    public Text connectionState;
    public InputField msgInput;
    public Text msgBox;

    public Button hideButton;
    public Button showButton;

    public GameObject introPanel;
    public GameObject chatPanel;

    public GameObject playerReadyDisplay;
    public Text playerReadyText;

    private ChatController chatController;
    public LogHandler logHandler;


    // Start is called before the first frame update

    private void Awake() {
        try {
            chatController = GameObject.FindObjectOfType<ChatController>();      
            chatChannelName = chatController.GetTeamName();
        } catch {
            Debug.Log("Must have loaded lobby scene while testing, not in actual GAME MODE");
        }
        
    }

    
    void Start()
    {
        DontDestroyOnLoad(transform.gameObject);
        Application.runInBackground = true;
        connectionState.text = "Starting...";
    }

    // Update is called once per frame
    void Update()
    {

        if (myChatClient != null)
        {
            myChatClient.Service();
            connectionState.text = myChatClient.State.ToString();
        }
        else
        {
            connectionState.text = "Offline";
        }

        //Enter to submit chat
        if (msgInput.text != "" && Input.GetKeyUp(KeyCode.Return))
        {
            SendMessage();
        }


    }

    public void Connect()
    {
        myChatClient = new ChatClient(this);
        channel = new ChatChannel(chatChannelName);
        myChatClient.ChatRegion = "US";
        // chatChannelName = roomName;        
        // channel.ChatChannel("hero");
        // print(playerName.text);
        if(playerName.text == "") {
            System.Random rand = new System.Random();
            int randNum = rand.Next(0, 100000);
            playerName.text = "RandomPlayer" + randNum;
        }
        myChatClient.Connect("a6158b28-c73c-4c44-aa95-0629e3a5bf1d", "1.0", new AuthenticationValues(playerName.text));
        connectionState.text = "Connecting to Chat";


    } 

    public void ConnectRandomUserName() {
        System.Random rand = new System.Random();
        int randNum = rand.Next(0, 100000);
        playerName.text = "RandomPlayer" + randNum;
        Connect();
    }

    public void OnConnected()
    {
        introPanel.SetActive(false);
        chatPanel.SetActive(true);
        playerReadyText.text = playerName.text + " is ready to play!";
        playerReadyDisplay.SetActive(true);
        connectionState.text = "Connected!";
        myChatClient.Subscribe(chatChannelName);        

        myChatClient.SetOnlineStatus(ChatUserStatus.Online);
        myChatClient.PublishMessage(chatChannelName, "Joined the chat!");

        //Create new Log file (loghander script handles duplicate files)
        logHandler.CreateText(chatChannelName);
        logHandler.LogMessage("System", playerName.text + " Joined the chat!");

        msgBox.text += "Welcome to Alliance! \nControls:" + "\n" + "Arrow Keys = Move" + "\n" + "Space = Special ability" + "\n" + "TAB + M = Full map" + "\n" + "TAB + C = Toggle magnet push/pull" + "\n\n";

    }

    public void OnDisconnected()
    {
        Debug.Log("Disconnected: " + myChatClient.DisconnectedCause);
    }

    public void SendMessage()
    {
        myChatClient.PublishMessage(chatChannelName, msgInput.text);
        logHandler.LogMessage(playerName.text, msgInput.text);
        msgInput.text = "";
    }

    public void PrintMessage(string message)
    {
        msgBox.text += message + "\n";
    }

    public void OnGetMessages(string channelName, string[] senders, object[] messages)
    {

        for (int i = 0; i < senders.Length; i++)
        {
            msgBox.text += senders[i] + ": " + messages[i] + "\n";
        }

    }

    public void OnPrivateMessage(string sender, object message, string channelName)
    {

    }

    public void OnSubscribed(string[] channels, bool[] results)
    {

    }

    public void OnUnsubscribed(string[] channels)
    {

    }

    public void OnUserSubscribed(string channel, string user)
    {

    }

    public void OnUserUnsubscribed(string channel, string user)
    {

    }

    public void OnStatusUpdate(string user, int status, bool gotMessage, object message)
    {

    }

    public void OnChatStateChange(ChatState state)
    {

    }

    public void DebugReturn(ExitGames.Client.Photon.DebugLevel level, string message)
    {

    }

    //methods to show/hide the chat. Invoked by the Show/Hide Chat buttons
    public void HideChat()
    {
        chatPanel.gameObject.SetActive(false);
        hideButton.gameObject.SetActive(false);
        showButton.gameObject.SetActive(true);
    }
    public void ShowChat()
    {
        showButton.gameObject.SetActive(false);
        chatPanel.gameObject.SetActive(true);
        hideButton.gameObject.SetActive(true);

    }


    void OnApplicationQuit()
    {
        if (myChatClient != null)
        {
            myChatClient.Disconnect();
        }
    }

    public void DisconnectFromChat() {
        OnApplicationQuit();
    }

}
