using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    private int coins;
    [SerializeField] private Text collectibleText;
    [SerializeField] private string max;
    private void Start() {
        this.coins = PlayerPrefs.GetInt("coins");
        collectibleText.text = "x "+coins;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Collectible"))
        {
            Destroy(other.gameObject);
            PlayerPrefs.SetInt("coins", coins);
            PlayerPrefs.Save();
            coins++;
            collectibleText.text = "x " + coins;
        }
    }
}
