using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public static class SocketClient
{
    static Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

    static IPAddress serverAddr = IPAddress.Parse("25.56.142.3");

    static IPEndPoint endPoint = new IPEndPoint(serverAddr, 14000);
    public static void sendTo(string param)
    {
        
        try
        {
            byte[] send_buffer = Encoding.ASCII.GetBytes(param);
            UnityEngine.Debug.Log("STO SPARANDO - CLIENT TO SERVER");
            sock.SendTo(send_buffer, endPoint);
        }
        catch (Exception e)
        {
            UnityEngine.Debug.Log(e);
        }
    }
}
