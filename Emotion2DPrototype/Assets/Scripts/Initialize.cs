using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Networking;
using System.IO;

public class Initialize : MonoBehaviour
{
    private string getURL = "https://survey1.emotionin2d.de/selectData.php";
    private string updateURL = "https://survey1.emotionin2d.de/updateTimesTaken.php";
    private string h = "";
    private int timesTaken;

    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.DeleteAll();

        //Remove Files to avoid duplicate Data
        if(File.Exists(Application.persistentDataPath +"/Ishihara.csv")){
            File.Delete(Application.persistentDataPath +"/Ishihara.csv");
        }
        if(File.Exists(Application.persistentDataPath +"/GeneralData.csv")){
            File.Delete(Application.persistentDataPath +"/GeneralData.csv");
        }
        if(File.Exists(Application.persistentDataPath +"/SAM.csv")){
            File.Delete(Application.persistentDataPath +"/SAM.csv");
        }

        //Debug.Log("Getting hue and setting playerid...");
        StartCoroutine(getData(getURL));
        
        PlayerPrefs.SetString("id", DateTimeOffset.Now.ToUnixTimeMilliseconds().ToString());
        Debug.Log("ID:" + DateTimeOffset.Now.ToUnixTimeMilliseconds().ToString());
        PlayerPrefs.SetString("colors", "11,10,01,00,n1,n0");
        PlayerPrefs.SetInt("openDoor", 0);
        PlayerPrefs.SetInt("lvl",0);
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
        Debug.Log("h: " + h +" and times_taken: " + timesTaken);
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
