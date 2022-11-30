using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemCollector : MonoBehaviour
{
    private int Coin = 0;
    [SerializeField] private TextMeshProUGUI CoinCounter;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Coin"))
        {
            Destroy(collision.gameObject);
            Coin++;
            CoinCounter.text = ":" + Coin;
        }
    }
}
