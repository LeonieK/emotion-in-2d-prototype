using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InitializeLevel : MonoBehaviour
{
    [SerializeField] private Text textField;
    [SerializeField] private bool isRegularLvl;
    // Start is called before the first frame update
    void Start()
    {
        if(isRegularLvl)
        {
            int lvl = PlayerPrefs.GetInt("lvl");
            textField.text = lvl + "/9";
            PlayerPrefs.SetInt("lvl", lvl+1);
            PlayerPrefs.Save();
        } else 
        {
            textField.text = "Tunnel";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
