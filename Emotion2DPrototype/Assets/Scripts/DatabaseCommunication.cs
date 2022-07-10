using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class DatabaseCommunication : MonoBehaviour
{
    [SerializeField]private Player player;
    private string url = "http://survey1.emotionin2d.de/saveData.php";
    
    private void Start() 
    {
       StartCoroutine(postData(url, player));
    }

    IEnumerator postData(string url, Player player)
    {
        WWWForm form = new WWWForm();
        form.AddField("idPOST", player.id);
        form.AddField("genderPOST", player.gender);
        form.AddField("agePOST", player.age);
        form.AddField("experiencePOST", player.experience);
        form.AddField("deviceInfoPOST", player.deviceInfo);

        for(int i = 0; i < player.ishiharaData.Length; i++){
            form.AddField("plate"+(i+1)+"POST", player.ishiharaData[i]);
        }

        form.AddField("ishiharaResultPOST", player.ishiharaResult);


        UnityWebRequest www = UnityWebRequest.Post(url, form);

        //Send the request then wait here until it returns
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log("Error While Sending: " + www.error);
        }
        else
        {
            Debug.Log("Received: " + www.downloadHandler.text);
        }
    }
}
