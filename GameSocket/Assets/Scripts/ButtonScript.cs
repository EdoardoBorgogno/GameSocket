using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;

public class ButtonScript : MonoBehaviour
{
    public void onClick()
    {
        if(this.GetComponent<Image>().tintColor == Color.red)
        {
            this.GetComponent<Image>().tintColor = Color.green;
            this.GetComponent<TextMeshProUGUI>().text = "Ready";
        }
        else 
        {
            this.GetComponent<Image>().tintColor = Color.red;
            this.GetComponent<TextMeshProUGUI>().text = "Not Ready";
        }
    }
}
