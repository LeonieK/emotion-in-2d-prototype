using System.Collections;
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

        
        //StartCoroutine(getData(getURL, 1));
        
        PlayerPrefs.SetString("id", DateTimeOffset.Now.ToUnixTimeMilliseconds().ToString());
        //String h = PlayerPrefs.GetString("h1");
        PlayerPrefs.SetString("colors", "b11,b10,g11,g10,r11,r10,y11,y10,n1");
        PlayerPrefs.SetInt("openDoor", 0);
        PlayerPrefs.SetInt("lvl",0);
        //PlayerPrefs.SetInt("newScene",1);
        PlayerPrefs.Save();
        
    }
    IEnumerator getData(string url, int i)
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

            PlayerPrefs.SetString("h"+i, h);
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
