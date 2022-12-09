using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    
    public void PlayGame ()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void createLobby(string map)
    {
        Debug.Log("Invio al server: </STARTGAME/>" + map);
        SocketClient.sendTo("</STARTGAME/>" + map);
        
    }
}
