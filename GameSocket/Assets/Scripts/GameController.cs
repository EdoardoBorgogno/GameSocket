using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;
using UnityEngine.UI;
using Newtonsoft.Json.Converters;

public class GameController : MonoBehaviour
{
    public Sprite purpleSoldier;
    private SocketClient sock = new SocketClient();
    public TextMeshProUGUI JoinGUID;
    public TextMeshProUGUI JoinPWD;

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
        {
            switch (getCommand(message))
            {
                case "GAMEINIT":
                    Debug.Log(getMessage(message));
                    Debug.Log(message.Substring(message.IndexOf("/>") + 2, 8));
                    GameObject.Find("lobbyGUID").GetComponent<TextMeshProUGUI>().text = "Game ID:" + message.Substring(message.IndexOf("/>") + 2, 7);
                    GameObject.Find("lobbyPWD").GetComponent<TextMeshProUGUI>().text = "Password:" + message.Substring(message.IndexOf(";") + 1, 5);
                    break;

                case "READY":
                    GameObject.Find("ReadyBtnP2").GetComponent<UnityEngine.UI.Image>().color = Color.green;
                    GameObject.Find("TextReadyP2").GetComponent<TextMeshProUGUI>().text = "READY";
                    GameObject.Find("ReadyBtnP1Joined").GetComponent<UnityEngine.UI.Image>().color = Color.green;
                    GameObject.Find("TextReadyP1Joined").GetComponent<TextMeshProUGUI>().text = "READY";
                    break;

                case "JOINEDTOGAME":
                    GameObject.Find("PlayerTwo").GetComponent<UnityEngine.UI.Image>().sprite = purpleSoldier;
                    break;

                default:
                    Debug.Log("Comando non valido");
                    break;
            }
            Debug.Log(message);
        }


            

        
    }

    private static string getCommand(string str)
    {
        return str.Substring(str.IndexOf("</") + 2, str.IndexOf("/>") - 2).ToUpper();
    }

    private static string getMessage(string data)
    {
        return data.Substring(data.LastIndexOf(">") + 1);
    }

    public void JoinGame()
    {
        /*string GUID = GameObject.Find("joinGUID").GetComponent<InputField>().text;
        string PWD = GameObject.Find("joinPWD").GetComponent<InputField>().text;*/
        //GameObject errorAlert = GameObject.Find("errorAlert");

        /*if(GUID == string.Empty && PWD == string.Empty)
        {
            errorAlert.GetComponent<TextMeshProUGUI>().text = "Inserisci il Game ID e la Password!";
        }
        else if (GUID == string.Empty)
        {
            errorAlert.GetComponent<TextMeshProUGUI>().text = "Inserisci il Game ID!";
        }
        else if (PWD == string.Empty)
        {
            errorAlert.GetComponent<TextMeshProUGUI>().text = "Inserisci la Password!";
        }
        else
        {
            
        }*/
        SocketClient.Send("</JOINGAME/>" + JoinGUID.text + ";" + JoinPWD.text);
    }

}
