using System.Net.Sockets;
using System.Net;
using System.Text;
using System;
using System.Threading;
using UnityEngine;
using TMPro;

public static class SocketClient
{
    static Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

    static IPAddress serverAddr = IPAddress.Parse("25.56.142.3");

    static IPEndPoint endPoint = new IPEndPoint(serverAddr, 11000);

    public static void sendTo(string param)
    {
        try
        {
            byte[] send_buffer = Encoding.ASCII.GetBytes(param);

            sock.SendTo(send_buffer, endPoint);
        }
        catch (Exception)
        {

        }
    }

    public static void receiveFrom()
    {
        new Thread(() =>
        {
            while (true)
            {
                try
                {
                    byte[] receive_buffer = new byte[1024];

                    EndPoint senderRemote = (EndPoint)endPoint;

                    int rec = sock.ReceiveFrom(receive_buffer, ref senderRemote);

                    Array.Resize(ref receive_buffer, rec);

                    var str = Encoding.ASCII.GetString(receive_buffer);
                    

                    switch (getCommand(str))
                    {
                        case "GAMEINIT":

                            Debug.Log(getMessage(str));
                            Debug.Log(GameObject.Find("lobbyGUID"));
                            GameObject.Find("lobbyGUID").GetComponent<TextMeshProUGUI>().text = "CIAO";
                            GameObject.Find("lobbyPWD").GetComponent<TextMeshProUGUI>().text = "CIAO";

                            break;

                        default:
                            break;
                    }
                }
                catch (Exception)
                {

                }
            }
        }).Start();
    }

    private static string getCommand(string str)
    {
        return str.Substring(str.IndexOf("</") + 2, str.IndexOf("/>") - 2).ToUpper();
    }

    private static string getMessage(string data)
    {
        return data.Substring(data.LastIndexOf(">") + 1);
    }

}