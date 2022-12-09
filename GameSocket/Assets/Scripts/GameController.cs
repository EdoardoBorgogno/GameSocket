using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private SocketClient sock = new SocketClient();
    // Start is called before the first frame update
    void Start()
    {

    }

    private void Awake()
    {
        SocketClient.senderIp = "25.56.142.3";
        SocketClient.senderPort = 11000;
        
        sock.StartReceiveThread();
    }

    private void Update()
    {
        foreach (var message in sock.getMessages())
            Debug.Log(message);
    }

}
