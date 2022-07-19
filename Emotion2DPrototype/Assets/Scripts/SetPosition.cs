using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPosition : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.HasKey("playerPosx"))
        {
            float x = PlayerPrefs.GetFloat("playerPosx");
            float y = PlayerPrefs.GetFloat("playerPosy");
            float z = PlayerPrefs.GetFloat("playerPosz");
            transform.position = new Vector3(x,y,z);
            PlayerPrefs.DeleteKey("playerPosx");
            PlayerPrefs.DeleteKey("playerPosy");
            PlayerPrefs.DeleteKey("playerPosz");
        }
    }
}
