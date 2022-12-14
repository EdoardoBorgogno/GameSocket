using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;
using UnityEngine.UI;
using Newtonsoft.Json.Converters;
using UnityEngine.SceneManagement;
using System.Net;
using System;

public class GameController : MonoBehaviour
{
    public Sprite purpleSoldier;
    public SocketClient sock = new SocketClient();
    public TextMeshProUGUI JoinGUID;
    static bool serverSet = false;
    public TextMeshProUGUI JoinPWD;
    public GameObject LobbyMenu;
    public GameObject JoinLobbyMenu;
    GameObject ServerPlayer;
    public GameObject NoServerInRunning;
    float oldX = -100;
    float afterJumpY;


    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().name != "Menu")
        {
            Debug.Log("Port: " + SocketClient.senderPort);
            Debug.Log("You Are: " + PlayerPrefs.GetString("color"));
            if (PlayerPrefs.GetString("color") == "Purple")
                ServerPlayer = GameObject.Find("Red");
            else
                ServerPlayer = GameObject.Find("Purple");
        }
    }

    private void Awake()
    {
        if (SceneManager.GetActiveScene().name == "Menu" && SocketClient.senderIp == null)
        {
            SocketClient.senderPort = 11000;

            SocketClient.udpClient.Client.ReceiveTimeout = 2000;

            WWW www = new WWW("https://paolobruno1280.altervista.org/Esercizi/GameSocket/getServerPHP.php");

            while (!www.isDone) { }

            string data = www.text;

            data = data.Replace("\"", "");
            data = data.Substring(0, data.Length - 1);

            bool connectionEndedCorrectly;
            string[] serverIPs = data.Split(';');

            foreach (string serverIP in serverIPs)
            {
                connectionEndedCorrectly = true;

                SocketClient.senderIp = serverIP;
                try
                {
                    SocketClient.Send("</SEARCHSERVER/>");

                    var endPoint = new IPEndPoint(IPAddress.Parse(SocketClient.senderIp), 11000);

                    var serverResponse = SocketClient.udpClient.Receive(ref endPoint);

                    if (serverResponse == null)
                        connectionEndedCorrectly = false;

                }
                catch (Exception ex)
                {
                    Debug.Log(ex.Message + " ----- " + SocketClient.senderIp);
                    connectionEndedCorrectly = false;
                    SocketClient.senderIp = null;
                }

                if (connectionEndedCorrectly)
                    break;
            }

            if (SocketClient.senderIp == null)
            {
                NoServerInRunning.SetActive(true);
                GameObject.Find("MainMenu").SetActive(false);
            }
            else
                Debug.Log(SocketClient.senderIp);

        }

        sock.StartReceiveThread();
    }


    private void Update()
    {
        foreach (var message in sock.getMessages())
        {
            Debug.Log(message);
            switch (getCommand(message))
            {
                case "GAMEINIT":
                    //Debug.Log(getMessage(message));
                    GameObject.Find("LoadingScreen").SetActive(false);
                    LobbyMenu.SetActive(true);
                    //Debug.Log(message.Substring(message.IndexOf("/>") + 2, 8));
                    GameObject.Find("lobbyGUID").GetComponent<TextMeshProUGUI>().text = "Game ID:" + message.Substring(message.IndexOf("/>") + 2, 7);
                    GameObject.Find("lobbyPWD").GetComponent<TextMeshProUGUI>().text = "Password:" + message.Substring(message.IndexOf(";") + 1, 5);
                    break;

                case "READY":
                    //Debug.Log(getMessage("E ARRIVATO READY"));
                    if (!JoinLobbyMenu.active)
                    {
                        GameObject.Find("ReadyBtnP2").GetComponent<UnityEngine.UI.Image>().color = Color.green;
                        GameObject.Find("ReadyBtnP2").GetComponent<UnityEngine.UI.Button>().interactable = false;
                        GameObject.Find("TextReadyP2").GetComponent<TextMeshProUGUI>().text = "Ready";
                    }
                    else
                    {
                        GameObject.Find("ReadyBtnP1Joined").GetComponent<UnityEngine.UI.Image>().color = Color.green;
                        GameObject.Find("ReadyBtnP1Joined").GetComponent<UnityEngine.UI.Button>().interactable = false;
                        GameObject.Find("TextReadyP1Joined").GetComponent<TextMeshProUGUI>().text = "Ready";
                    }
                    break;

                case "PLAYERJOINED":
                    //Debug.Log(getMessage(message));
                    GameObject.Find("PlayerTwo").GetComponent<UnityEngine.UI.Image>().sprite = purpleSoldier;
                    break;

                case "JOINEDTOGAME":
                    //Debug.Log(getMessage("SEMO JOINATI ALE"));
                    GameObject.Find("LoadingScreen").SetActive(false);
                    JoinLobbyMenu.SetActive(true);
                    break;

                case "STARTGAME":
                    sock.StopThread();
                    SceneManager.LoadScene(getMessage(message));
                    break;

                case "SHOOT":
                    ServerPlayer.GetComponent<Weapon>().Shoot();
                    break;

                case "MOVE":
                    //Debug.Log(float.Parse(getMessage(message)));
                    //ServerPlayer.GetComponent<CharacterController2D>().Move(float.Parse(getMessage(message)) * Time.fixedDeltaTime, ServerPlayer.GetComponent<PlayerMovement>().jump);

                    if (ServerPlayer.GetComponent<CharacterController2D>().OnLandEvent != null)
                        ServerPlayer.transform.position = new Vector3(float.Parse(getMessage(message).Split(";")[0]), float.Parse(getMessage(message).Split(";")[1]), ServerPlayer.transform.position.z);
                    else
                        ServerPlayer.transform.position = new Vector3(float.Parse(getMessage(message).Split(";")[0]), ServerPlayer.transform.position.y, ServerPlayer.transform.position.z);

                    if (oldX != -100)
                    {
                        if (oldX < float.Parse(getMessage(message).Split(";")[0]) && !ServerPlayer.GetComponent<CharacterController2D>().m_FacingRight)
                        {
                            ServerPlayer.GetComponent<CharacterController2D>().Flip();
                        }
                        else if (oldX > float.Parse(getMessage(message).Split(";")[0]) && ServerPlayer.GetComponent<CharacterController2D>().m_FacingRight)
                        {
                            ServerPlayer.GetComponent<CharacterController2D>().Flip();
                        }

                        if (oldX != float.Parse(getMessage(message).Split(";")[0]))
                            ServerPlayer.GetComponent<Animator>().SetBool("IsRunning", true);
                        else
                            ServerPlayer.GetComponent<Animator>().SetBool("IsRunning", false);
                    }



                    oldX = float.Parse(getMessage(message).Split(";")[0]);

                    break;

                case "CLOSEGAME":
                    sock.StopThread();
                    break;

                case "JUMP":
                    //Debug.Log("Mi è arrivato JUMP");
                    //ServerPlayer.GetComponent<PlayerMovement>().jump = true;
                    ServerPlayer.GetComponent<CharacterController2D>().Jump();
                    ServerPlayer.GetComponent<PlayerMovement>().JumpAnimation();
                    break;

                default:
                    // Debug.Log(getMessage(message));
                    //Debug.Log("Comando non valido");
                    break;
            }
            //Debug.Log(message);
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
        Debug.Log("</JOINGAME/>" + JoinGUID.text + ";" + JoinPWD.text);
        SocketClient.Send("</JOINGAME/>" + JoinGUID.text + ";" + JoinPWD.text);
    }

    public void youAre(string color)
    {
        PlayerPrefs.SetString("color", color);
    }

    private void OnApplicationQuit()
    {
        SocketClient.Send("</ENDGAME/>");
        sock.Stop();
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

}