using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;
using UnityEngine.UI;
using Newtonsoft.Json.Converters;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public Sprite purpleSoldier;
    private SocketClient sock = new SocketClient();
    public TextMeshProUGUI JoinGUID;
    public TextMeshProUGUI JoinPWD;
    public GameObject LobbyMenu;
    public GameObject JoinLobbyMenu;
    GameObject ServerPlayer;


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
        SocketClient.senderIp = "25.56.142.3";
        SocketClient.senderPort = 11000;

        Debug.Log("Ho creato un Thread");
        sock.StartReceiveThread();
    }

    private void Update()
    {
        foreach (var message in sock.getMessages())
        {
            //Debug.Log(message);
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
                    ServerPlayer.transform.position = new Vector3(float.Parse(getMessage(message).Split(";")[0]), float.Parse(getMessage(message).Split(";")[1]), ServerPlayer.transform.position.z);
                    break;

                case "JUMP":
                    //Debug.Log("Mi è arrivato JUMP");
                    //ServerPlayer.GetComponent<PlayerMovement>().jump = true;
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
        sock.Stop();
    }

}