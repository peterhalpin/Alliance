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

    //figure out how to get name from character
    public String temp_username = "tempUsername1";

    public ChatClient myChatClient;

    // Start is called before the first frame update
    void Start()
    {
        myChatClient = new ChatClient(this);
        print("Chat has Started");
        Connect();
    }

    // Update is called once per frame
    void Update()
    {
        myChatClient.Service();
    }

    public void Connect()
    {
        myChatClient.Connect("a6158b28 - c73c - 4c44 - aa95 - 0629e3a5bf1d", "1.0", new AuthenticationValues(temp_username));
        print("inside Connect() method");
    }

    public void OnConnected()
    {
        myChatClient.Subscribe(new string[] { "global" });
        myChatClient.SetOnlineStatus(ChatUserStatus.Online);
        print("Chat is Connected");
    }

    public void OnDisconnected()
    {
        Debug.Log("Disconnected: " + myChatClient.DisconnectedCause);
    }

    public void OnGetMessages(string channelName, string[] senders, object[] messages)
    {

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
