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
    private string urlSaveData = "https://survey1.emotionin2d.de/saveData.php";
    [SerializeField] private InputField feedbackField;
    private char gender;
    private int age;
    private int experience;
    private string deviceInfo;
    private int[] ishiharaData = new int[11];
    private char ishiharaResult;
    public List<SAMResult> samresults = new List<SAMResult>();
    
    public class SAMResult {
        public char hue;
        public int saturation;
        public int brightness;
        public int arousal;
        public int valence;
        public int dominance;
        public int lvl;

        public SAMResult(char hue, int brightness, int saturation, int arousal, int valence, int dominance, int lvl)
        {
            this.hue = hue;
            this.saturation = saturation;
            this.brightness = brightness;
            this.arousal = arousal;
            this.valence = valence;
            this.dominance = dominance;
            this.lvl = lvl;
        }
        public string toString()
        {
            return "Hue: " + this.hue.ToString() + " // Brightness: " + this.brightness.ToString()+ " // SaturatioN: " + this.saturation.ToString()
                + " // Arousal: " + this.arousal.ToString() + " // Valence: " + this.valence.ToString() + " // Dominance: " + this.dominance.ToString() + " // LVL: " + this.lvl.ToString();
        }
    }

    public void submitButtonPressed()
    {
        //Post Data to Database
        StartCoroutine(postData(urlSaveData));
    }

    IEnumerator postData(string url)
    {
        setGenearlQuestionsData();
        setIshiharaData();
        setSAMData();
        string id = "";
        if(PlayerPrefs.HasKey("id"))
        {
            id = PlayerPrefs.GetString("id");
        } else 
        {
            Debug.LogError("There is no save data!");
        }
        
        Debug.Log("Set general Data to Form: id: " + id + " ;gender: " + gender + " ;age: " + age 
            + " ;experience: " + experience + " ;deviceInfo: " + deviceInfo);
        WWWForm form = new WWWForm();
        form.AddField("idPOST", id);
        form.AddField("genderPOST", gender);
        form.AddField("agePOST", age);
        form.AddField("experiencePOST", experience);
        form.AddField("deviceInfoPOST", deviceInfo);
        
        Debug.Log("Set Time Spent to form");
        DateTime dataValuesStart = DateTime.Parse(PlayerPrefs.GetString("startTime"));
        DateTime datatValuesEnd = DateTime.Now;
        TimeSpan value = datatValuesEnd.Subtract(dataValuesStart);
        Debug.Log("Time Spent: " + value.ToString());
        form.AddField("timeSpendPOST",value.ToString());

        Debug.Log("Set Ishihara Data to Form");
        for(int i = 1; i <= ishiharaData.Length; i++){
            form.AddField("plate"+i.ToString()+"POST", ishiharaData[i-1]);
            Debug.Log("plate"+i.ToString()+"POST");
        }

        form.AddField("ishiharaResultPOST", ishiharaResult);

        Debug.Log("Set SAM Data to Form");
        for(int j = 0; j < samresults.Count; j++)
        {
            form.AddField("samCollection"+(j+1)+"POST", (samresults[j].hue.ToString() + ","
                + samresults[j].saturation.ToString() + "," + samresults[j].brightness.ToString() +"," 
                + samresults[j].arousal.ToString() + "," + samresults[j].valence.ToString() + ","
                + samresults[j].dominance.ToString() + "," + samresults[j].lvl.ToString()));
        }
        form.AddField("feedbackPOST",feedbackField.text);
        
        
    
        Debug.Log("Send WebRequest to server");
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
        Debug.Log("Set General Data. deviceInfo: " + deviceInfo.ToString());
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
                    Debug.Log("Set General Data. age: " + age.ToString() + " ;gender: " + gender.ToString() + " ;experience: " + experience.ToString());
                } else if (dataValues[2].Equals("Keine Angabe"))
                {
                    age = int.Parse(dataValues[2]);
                    gender = 'n';
                    experience = int.Parse(dataValues[4]);
                    Debug.Log("Set General Data. age: " + age.ToString() + " ;gender: " + gender.ToString() + " ;experience: " + experience.ToString());
                } else if (dataValues[1].Equals("Keine Angabe"))
                {
                    age = 0;
                    gender = dataValues[2].ToCharArray()[0];
                    experience = int.Parse(dataValues[3]);
                    Debug.Log("Set General Data. age: " + age.ToString() + " ;gender: " + gender.ToString() + " ;experience: " + experience.ToString());
                } else
                {
                    age = int.Parse(dataValues[1]);
                    gender = dataValues[2].ToCharArray()[0];
                    experience = int.Parse(dataValues[3]);
                    Debug.Log("Set General Data. age: " + age.ToString() + " ;gender: " + gender.ToString() + " ;experience: " + experience.ToString());
                }
            }
            tempCount++;
        }
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
            //dataString1 = dataString1.Replace('"'.ToString(), "");
            var dataValues1 = dataString1.Split(',');
            Debug.Log("Ishihara Data Values Length: " + dataValues1.Length.ToString());

            if(tempCount == 1)
            {
                for(int i = 1; i < dataValues1.Length; i++){
                    ishiharaData[i-1] = int.Parse(dataValues1[i]);
                    Debug.Log("Set Ishihara Data plate" + i.ToString() + ": " + dataValues1[i]);
                }
                ishiharaResult = calculateScore(dataValues1);
                Debug.Log("Set Ishihara Result: " + ishiharaResult);
            }
            tempCount++;
        }
        
    }

    private char calculateScore(string[] data)
    {
        int score = 0;
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
        string originPath = Application.persistentDataPath +"/SAM.csv";
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
            if(tempCount > 0)
            {
                dataString = dataString.Replace('"'.ToString(), "");
                Debug.Log(dataString);
                var dataValues = dataString.Split(',');
               
                var hsb = dataValues[4].ToCharArray();
                Debug.Log("Set SAM Data" + tempCount.ToString());
                Debug.Log("Hue: " + hsb[0] + " // Brightness: " + (hsb[1]-'0')+ " // SaturatioN: " + (hsb[2]-'0')
                + " // Arousal: " + dataValues[1] + " // Valence: " + dataValues[2] + " // Dominance: " + dataValues[3] + " // LVL: " + dataValues[5]);
                SAMResult result = new SAMResult(hsb[0],(hsb[1]-'0'),(hsb[2]-'0'),
                int.Parse(dataValues[1]),int.Parse(dataValues[2]),int.Parse(dataValues[3]),int.Parse(dataValues[5]));
                samresults.Add(result);
            }
            tempCount++;

        }
    }
}
