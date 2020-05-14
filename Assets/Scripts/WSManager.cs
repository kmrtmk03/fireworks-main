using UnityEngine;
using System.Collections;
using WebSocketSharp;
using WebSocketSharp.Net;
using MyManager;
using System.Threading;

//https://mslgt.hatenablog.com/entry/2018/07/20/071102

public class WSManager : MonoBehaviour
{

    WebSocket ws;

    void Start()
    {
        StartCoroutine(this.Init(0.0f));
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
            ws.Send("Happy!!");
    }

    private IEnumerator Init(float _delayTime)
    {
        yield return new WaitForSeconds(_delayTime);

        ws = new WebSocket("ws://localhost:5000/");

        ws.OnOpen += (sender, e) =>
        {
            Debug.Log("WebSocket Open");
        };

        // Main ThreadのContextを取得する.
        var context = SynchronizationContext.Current;

        ws.OnMessage += (sender, e) =>
        {
            context.Post(state => {
                ManagerInput.instance.OnMessage(e.Data);
            }, e.Data);
        };

        ws.OnError += (sender, e) =>
        {
        };

        ws.OnClose += (sender, e) =>
        {
        };

        ws.Connect();
    }

    void OnDestroy()
    {
        ws.Close();
        ws = null;
    }
}
