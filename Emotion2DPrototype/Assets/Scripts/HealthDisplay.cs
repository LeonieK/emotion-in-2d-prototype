using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    public Text healthText;
    public int health = 5;
    public int maxHealth = 5;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = health.ToString() + "/" + maxHealth.ToString();
        if(Input.GetKeyDown(KeyCode.Space)){
            health--;
        }
    }
}
