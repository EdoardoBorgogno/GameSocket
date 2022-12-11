using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;

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
        SocketClient.Send("</ENDGAME/>");
    }
}
