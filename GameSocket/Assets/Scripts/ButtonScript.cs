using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;
using Unity.VisualScripting;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
    public TextMeshProUGUI Text;
    public GameObject btnPlayerOne;
    public GameObject btnPlayerTwo;
    //public UnityEngine.UI.Button Btn;
    public bool isReadyP1 = false;
    public bool isReadyP2 = false;

    private void Start()
    {

    }

    private void Update()
    {
        if (btnPlayerTwo.GetComponent<UnityEngine.UI.Image>().color == Color.green) isReadyP2 = true;
    }

    public void changeState()
    {
        Text.text = "Ready";
        btnPlayerOne.GetComponent<UnityEngine.UI.Image>().color = Color.green;
        btnPlayerOne.GetComponent<UnityEngine.UI.Button>().interactable = false;

        Debug.Log("READY!");
        SocketClient.Send("</READY/>");
    }

}






