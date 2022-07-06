using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectibleDisplay : MonoBehaviour
{
    public Text collectibleText;
    public int collectibles = 5;
    public int maxCollectibles = 5;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        collectibleText.text = collectibles.ToString() + "/" + maxCollectibles.ToString();
        if(Input.GetKeyDown(KeyCode.Space)){
            collectibles--;
        }
    }
}
