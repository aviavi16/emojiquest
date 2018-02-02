using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Quobject;
using Quobject.SocketIoClientDotNet.Client;
using Newtonsoft.Json;
using UnityEngine.UI;
using Quobject.SocketIoClientDotNet.Parser;

public class SocketManager : MonoBehaviour {
    public class ChatData
    {
        public string id;
        public string msg;
    };

    public string serverURL = "http://35.225.150.27";

    public InputField uiInput = null;
    public Button uiSend = null;
    public Text uiChatLog = null;
    public bool ready = false;
    public bool notified = false;
    public bool firstMessageSent = false;

    public Socket socket = null;
    protected List<string> chatLog = new List<string>();

    void OnDestroy()
    {
        DoClose();
    }

    // Use this for initialization
    void Start()
    {
        Debug.Log("start connect");
        DoOpen();
        Debug.Log("start");

       
    }

    // Update is called once per frame
    void Update()
    {
        if (ready && !notified)
        {
            GameManager.instance.ReadyToPlay();
            notified = true;
        }
        // Debug.Log("var=" + var);

        if (Input.GetKeyDown(KeyCode.M))
        {
            socket.Emit("mission", new string[] { "hit the tree 3 times"});
            Debug.Log("emitted");
        }

        lock (chatLog)
        {
            if (chatLog.Count > 0)
            {
                
                string str = uiChatLog.text;
                foreach (var s in chatLog)

                {
                    Debug.Log("chatLog " + s);
                    str = str + "\n" + s;
                }
                //uiChatLog.text = str;
                
                chatLog.Clear();
            }
        }
        
    }

    void OnMessage(Socket socket)
    {
        // args[0] is the nick of the sender
        // args[1] is the message
        Debug.Log(string.Format("Message from {0}: {1}"));
    }

    void DoOpen()
    {
        if (socket == null)
        {
            socket = IO.Socket(serverURL);

            socket.On(Socket.EVENT_CONNECT, () => {
                Debug.Log("ready");
                ready = true;
            });
            
            socket.On("chat message", (data) => {
             string str = data.ToString();

                //ChatData chat = JsonConvert.DeserializeObject<ChatData>(str);
                //string strChatLog = "user#" + chat.id + ": " + chat.msg;

                // Access to Unity UI is not allowed in a background thread, so let's put into a shared variable
                Debug.Log("gibrish2 "+str);
                Debug.Log("im here");
                if (!firstMessageSent)
                {
                    Debug.Log("im here2");
                    firstMessageSent = true;
                }
                //Debug.Log(data.ToString()[0].GetHashCode());

                lock (chatLog)
              {
                    chatLog.Add(str);
               }
            });


           
        }
    }

    public bool SentFirstMessage()
    {
        return firstMessageSent;
    }

    public void DoClose()
    {
        if (socket != null)
        {
            socket.Disconnect();
            Debug.Log("disconnect");
            socket = null;
        }
    }

    void SendChat(string str)
    {
        if (socket != null)
        {
            socket.Emit("chat", str);
        }
    }

    void OnApplicationQuit()
    {
        Debug.Log("quit");
        Destroy(this.gameObject);
    }
}