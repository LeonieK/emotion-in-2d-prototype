using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class ShowData : MonoBehaviour
{
    string filename ="";

    [System.Serializable]
    public class SurveyData
    {
        public string gender;
        public string deviceInformation;
        public int age;
        public int videoGameExperience;
    }
    [System.Serializable]
    public class SurveyDataList
    {
        public SurveyData[] surveyDataList;
    }
    public SurveyDataList mySurveyDataList = new SurveyDataList();

    private void Start() {
        Debug.Log("Started");
        filename = "/html/html/survey1/Ishihara.csv";
    }
    private void Update() {
        if(Input.GetKeyDown(KeyCode.Space)){
            WriteCSVFile();
        }
    }

    public void WriteCSVFile()
    {
        if(mySurveyDataList.surveyDataList.Length > 0){
            TextWriter tw = new StreamWriter(filename, false);
            tw.WriteLine("Gender, DeviceInfo, Age, Experience");
            tw.Close();

            tw = new StreamWriter(filename, true);

            for (int i = 0; i < mySurveyDataList.surveyDataList.Length; i++){
                tw.WriteLine(mySurveyDataList.surveyDataList[i].gender + "," + mySurveyDataList.surveyDataList[i].deviceInformation + "," +
                    mySurveyDataList.surveyDataList[i].age + "," + mySurveyDataList.surveyDataList[i].videoGameExperience + ",");
            }
            tw.Close();
        }
    }
    
    public void ReadCSVFile()
    {
        string originPath = Application.persistentDataPath +"/Ishihara.csv";
        StreamReader streamReader = new StreamReader(originPath);
        bool endOfFile = false;
        while(!endOfFile)
        {
            string dataString = streamReader.ReadLine();
            if(dataString == null)
            {
                endOfFile = true;
                break;
            }
            var dataValues = dataString.Split(',');
            for(int o = 0; o < dataValues.Length; o++){
                Debug.Log("Value: " +o.ToString() + " " + dataValues[o].ToString());
            }

        }
    }
}
