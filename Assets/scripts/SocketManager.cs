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

    public string serverURL = "35.225.170.214";

    public InputField uiInput = null;
    public Button uiSend = null;
    public Text uiChatLog = null;

    protected Socket socket = null;
    protected List<string> chatLog = new List<string>();

    void Destroy()
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
        
        lock (chatLog)
        {
            if (chatLog.Count > 0)
            {
                string str = uiChatLog.text;
                foreach (var s in chatLog)
                {
                    str = str + "\n" + s;
                }
                uiChatLog.text = str;
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
                Debug.Log("gibrish");
            });
            
            socket.On("chat message", (data) => {
                string str = data.ToString();

                //ChatData chat = JsonConvert.DeserializeObject<ChatData>(str);
                //string strChatLog = "user#" + chat.id + ": " + chat.msg;

                // Access to Unity UI is not allowed in a background thread, so let's put into a shared variable
                Debug.Log("gibrish2");
                Debug.Log(data.ToString()[0].GetHashCode());
            });
            
        }
    }

    void DoClose()
    {
        if (socket != null)
        {
            socket.Disconnect();
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
}