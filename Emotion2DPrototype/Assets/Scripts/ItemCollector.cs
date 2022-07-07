using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    private int coins = 0;
    [SerializeField] private Text collectibleText;

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Collectible"))
        {
            Destroy(other.gameObject);
            coins++;
            collectibleText.text = "x " + coins;
        }
    }
}
