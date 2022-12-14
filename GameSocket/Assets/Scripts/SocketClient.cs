using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;
using UnityEngine;

public class SocketClient
{
    public static UdpClient udpClient = new UdpClient();

    private readonly Queue<string> incomingQueue = new Queue<string>();
    Thread receiveThread;
    private bool threadRunning = true;
    public static string senderIp;
    public static int senderPort;

    public void StartReceiveThread()
    {
        receiveThread = new Thread(() => ListenForMessages());
        receiveThread.IsBackground = true;
        threadRunning = true;
        receiveThread.Start();
    }

    private void ListenForMessages()
    {
        IPEndPoint remoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);

        while (threadRunning)
        {
            try
            {
                Byte[] receiveBytes = udpClient.Receive(ref remoteIpEndPoint);
                string returnData = Encoding.UTF8.GetString(receiveBytes);

                lock (incomingQueue)
                {
                    incomingQueue.Enqueue(returnData);
                }
            }
            catch (SocketException e)
            {
            }
            catch (Exception e)
            {
            }
        }
    }

    public string[] getMessages()
    {
        string[] pendingMessages = new string[0];

        lock (incomingQueue)
        {
            pendingMessages = new string[incomingQueue.Count];
            int i = 0;
            while (incomingQueue.Count != 0)
            {
                pendingMessages[i] = incomingQueue.Dequeue();
                i++;
            }
        }

        return pendingMessages;
    }

    public static void Send(string message)
    {
        IPEndPoint serverEndpoint = new IPEndPoint(IPAddress.Parse(senderIp), senderPort);
        Byte[] sendBytes = Encoding.UTF8.GetBytes(message);
        udpClient.Send(sendBytes, sendBytes.Length, serverEndpoint);
    }

    public void StopThread()
    {
        threadRunning = false;
        receiveThread.Abort();
    }

    public void Stop()
    {
        receiveThread.Abort();
        udpClient.Close();
    }
}