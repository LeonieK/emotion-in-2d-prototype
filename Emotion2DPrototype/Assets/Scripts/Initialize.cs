using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Networking;

public class Initialize : MonoBehaviour
{
    private string getURL = "https://survey1.emotionin2d.de/selectData.php";
    private string updateURL = "https://survey1.emotionin2d.de/updateTimesTaken.php";
    private string h = "";
    private int timesTaken;

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("Getting hue and setting playerid...");
        StartCoroutine(getData(getURL));
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetString("id", DateTimeOffset.Now.ToUnixTimeMilliseconds().ToString());
        PlayerPrefs.SetString("colors", "11,10,01,00,n1,n0");
        PlayerPrefs.SetInt("openDoor", 0);
        PlayerPrefs.Save();
        //Debug.Log("Updating Times Taken...");
    }

    IEnumerator getData(string url)
    {
        UnityWebRequest wwwGet = UnityWebRequest.Get(url);
        yield return wwwGet.SendWebRequest();
        if (wwwGet.error != null)
        {
            Debug.Log("There was an error getting the next hue: " + wwwGet.error);
        } else{
            string dataText = wwwGet.downloadHandler.text;
            dataText = dataText.Replace('"'.ToString(), "");
            var dataValues = dataText.Split(',');
            h = dataValues[0];
            timesTaken = int.Parse(dataValues[1]) +1;
            Debug.Log("h: " + h +" and times_taken: " + timesTaken);
            PlayerPrefs.SetString("h", h);
            PlayerPrefs.Save();
            StartCoroutine(increaseTimesTaken(updateURL));
        }
        
    }

    IEnumerator increaseTimesTaken(string url)
    {
        WWWForm form = new WWWForm();
        form.AddField("timesTakenPOST", timesTaken);
        form.AddField("hPOST", h);

        UnityWebRequest www = UnityWebRequest.Post(url, form);
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
