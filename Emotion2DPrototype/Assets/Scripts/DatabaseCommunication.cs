using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.IO;
using System;

/**
 This class is responsible to collect all data and send it to the database.
**/
public class DatabaseCommunication : MonoBehaviour
{
    private string url = "https://survey1.emotionin2d.de/saveData.php";
    private char gender;
    private int age;
    private int experience;
    private string deviceInfo;
    private int[] ishiharaData = new int[10];
    private char ishiharaResult;
    private SAMResult[] samresults;
    
    private class SAMResult {
        public char hue;
        public int saturation;
        public int brightness;
        public int arousal;
        public int valence;
        public int dominance;
        public int lvl;
    }

    public void submitButtonPressed()
    {
        setGenearlQuestionsData();
        setIshiharaData();
        setSAMData();
        //Post Data to Database
        StartCoroutine(postData(url));
    }

    IEnumerator postData(string url)
    {
        string id = "";
        if(PlayerPrefs.HasKey("id"))
        {
            id = PlayerPrefs.GetString("id");
        } else 
        {
            Debug.LogError("There is no save data!");
        }
        
        WWWForm form = new WWWForm();
        form.AddField("idPOST", id);
        form.AddField("genderPOST", gender);
        form.AddField("agePOST", age);
        form.AddField("experiencePOST", experience);
        form.AddField("deviceInfoPOST", deviceInfo);

        for(int i = 0; i < ishiharaData.Length; i++){
            form.AddField("plate"+(i+1)+"POST", ishiharaData[i]);
        }

        form.AddField("ishiharaResultPOST", ishiharaResult);

        /*for(int j = 0; j < samresults.Length; j++)
        {
            form.AddField("samCollection"+(j+1)+"POST", samresults[j].hue + ","
                + samresults[j].saturation + "," + samresults[j].brightness +"," 
                + samresults[j].arousal + "," + samresults[j].valence + ","
                + samresults[j].dominance + "," + samresults[j].lvl);
        }*/

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

    public void setGenearlQuestionsData(){
        deviceInfo = SystemInfo.deviceModel;
        string originPath = Application.persistentDataPath +"/GeneralData.csv";
        StreamReader streamReader = new StreamReader(originPath);
        bool endOfFile = false;
        int tempCount = 0;

        while(!endOfFile)
        {
            string dataString = streamReader.ReadLine();
            if(dataString == null)
            {
                endOfFile = true;
                break;
            }
            dataString = dataString.Replace('"'.ToString(), "");
            var dataValues = dataString.Split(',');
            if(tempCount == 1)
            {
                //Save To player 
                if(dataValues[1].Equals("Keine Angabe") && dataValues[2].Equals("Keine Angabe"))
                {
                    age = 0;
                    gender = 'n';
                    experience = int.Parse(dataValues[4]);
                } else if (dataValues[2].Equals("Keine Angabe"))
                {
                    age = int.Parse(dataValues[2]);
                    gender = 'n';
                    experience = int.Parse(dataValues[4]);
                } else if (dataValues[1].Equals("Keine Angabe"))
                {
                    age = 0;
                    gender = dataValues[2].ToCharArray()[0];
                    experience = int.Parse(dataValues[3]);
                } else
                {
                    age = int.Parse(dataValues[1]);
                    gender = dataValues[2].ToCharArray()[0];
                    experience = int.Parse(dataValues[3]);
                }
            }
            tempCount++;
        }
        //Remove File to Clean up Space
        //File.Delete(Application.persistentDataPath +"/GeneralData.csv");
    }

    public void setIshiharaData()
    {
        string originPath = Application.persistentDataPath +"/Ishihara.csv";
        StreamReader streamReader = new StreamReader(originPath);
        bool endOfFile = false;
        int tempCount = 0;

        while(!endOfFile)
        {
            string dataString1 = streamReader.ReadLine();
            if(dataString1 == null)
            {
                endOfFile = true;
                break;
            }
            dataString1 = dataString1.Replace('"'.ToString(), "");
            var dataValues1 = dataString1.Split(',');

            if(tempCount == 1)
            {
                for(int i = 0; i < ishiharaData.Length; i++){
                    Debug.Log("Data" +i +int.Parse(dataValues1[i+1]));
                    ishiharaData[i] = int.Parse(dataValues1[i+1]);
                }
                ishiharaResult = calculateScore(dataValues1);
            }
            tempCount++;
        }
        //Remove File to Clean up Space
        //File.Delete(Application.persistentDataPath +"/Ishihara.csv");
    }

    private char calculateScore(string[] data)
    {
        int score = 0;
        Debug.Log("Length; " + data.Length.ToString());
        //calculate score
        if (data[1].Equals("12")){
            score++;
        }
        if (data[2].Equals("8")){
            score++;
        } 
        if (data[3].Equals("5")){
            score++;
        } 
        if (data[4].Equals("29")){
            score++;
        } 
        if (data[5].Equals("74")){
            score++;
        } 
        if (data[6].Equals("7")){
            score++;
        } 
        if (data[7].Equals("45")){
            score++;
        } 
        if (data[8].Equals("2")){
            score++;
        } 
        if (data[9].Equals("16")){
            score++;
        } 
        if (data[10].Equals("35")){
            score++;
        } 
        
        if (data[11].Equals("96")){
            score++;
        } 
        if(score <=7){
            return 'd';
        }  else 
        {
            return 'n';
        } 
    }

    public void setSAMData()
    {
        //ToDo set Sam Data
    }
}
