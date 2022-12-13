using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
//using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class CountDownTimer : MonoBehaviour
{
    public float timeStart = 180;
    public TextMeshProUGUI Timer;
    public GameObject endMenu;
    void Start()
    {
        Timer.text = timeStart.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        timeStart -= Time.deltaTime;
        Timer.text = Mathf.Round(timeStart).ToString();
        if (timeStart <= 0)
            endGame();
    }

    void endGame()
    {

        endMenu.SetActive(true);

        GameObject winText = GameObject.Find("winText");
        GameObject.Find("Timer").GetComponent<TextMeshProUGUI>().text = "";
        if (int.Parse(GameObject.Find("Coin CounterP1").GetComponent<TextMeshProUGUI>().text) > int.Parse(GameObject.Find("Coin CounterP2").GetComponent<TextMeshProUGUI>().text))
        {
            winText.GetComponent<TextMeshProUGUI>().text = "Red Player has Won!";
            winText.GetComponent<TextMeshProUGUI>().color = Color.red;
        }
        else
        {
            winText.GetComponent<TextMeshProUGUI>().text = "Purple Player has Won!";
            winText.GetComponent<TextMeshProUGUI>().color = Color.magenta;
        }

        SocketClient.Send("</ENDGAME/>");
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
