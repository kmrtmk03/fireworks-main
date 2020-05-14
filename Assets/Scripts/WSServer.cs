using UnityEngine;
using WebSocketSharp;
using WebSocketSharp.Server;
using MyManager;

public class WSServer : MonoBehaviour
{
    WebSocketServer server;

    public static WSServer instance = null;

    private void Awake()
    {
        if(instance == null)
            instance = this;
        else
            Destroy(this);
    }

    void Start()
    {
        server = new WebSocketServer(5000);
        server.AddWebSocketService<Echo>("/");
        server.Start();
    }

    void OnDestroy()
    {
        server.Stop();
        server = null;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            WSServer.instance.OnReceive("Update");
    }


    public void OnReceive(string _data)
    {
        ManagerInput.instance.OnMessage(_data);
    }
}

public class Echo : WebSocketBehavior
{
    protected override void OnMessage(MessageEventArgs e)
    {
        Sessions.Broadcast(e.Data);
        Debug.Log(e.Data);
    }
}
