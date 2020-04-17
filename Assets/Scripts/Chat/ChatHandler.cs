using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using Photon.Chat;

#if PHOTON_UNITY_NETWORKING
using Photon.Pun;
#endif



public class ChatHandler : MonoBehaviour, IChatClientListener
{

    public ChatClient myChatClient;         //main "connection point" to chat API
    public InputField playerName;
    public Text connectionState;
    string worldChat;
    public InputField msgInput;
    public Text msgBox;

    public GameObject introPanel;
    public GameObject chatPanel;


    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(transform.gameObject);
        Application.runInBackground = true;
        connectionState.text = "Starting...";
        worldChat = "world";

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

    }

    public void Connect()
    {
        myChatClient = new ChatClient(this);
        print(playerName.text);
        myChatClient.Connect("a6158b28-c73c-4c44-aa95-0629e3a5bf1d", "1.0", new AuthenticationValues(playerName.text));
        connectionState.text = "Connecting to Chat";
    }

    public void OnConnected()
    {
        introPanel.SetActive(false);
        chatPanel.SetActive(true);
        connectionState.text = "Connected!";
        myChatClient.Subscribe(new string[] { worldChat });
        myChatClient.SetOnlineStatus(ChatUserStatus.Online);
        myChatClient.PublishMessage(worldChat, "Joined the chat!");
    }

    public void OnDisconnected()
    {
        Debug.Log("Disconnected: " + myChatClient.DisconnectedCause);
    }

    public void SendMessage()
    {
        myChatClient.PublishMessage(worldChat, msgInput.text);
        msgInput.text = "";
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

    void OnApplicationQuit()
    {
        if (myChatClient != null)
        {
            myChatClient.Disconnect();
        }
    }

}
