using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializeLevel : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int lvl = PlayerPrefs.GetInt("lvl");
        PlayerPrefs.SetInt("lvl", lvl+1);
        PlayerPrefs.Save();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
