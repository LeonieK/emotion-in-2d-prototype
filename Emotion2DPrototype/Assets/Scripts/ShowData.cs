using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class ShowData : MonoBehaviour
{
    public Text Textfield;
    public void ReadCSV()
    {
        StreamReader streamReader = new StreamReader(Application.persistentDataPath + "/QuestionnaireResults");
        bool endOfFile = false;
        while(!endOfFile)
        {
            string dataString = streamReader.ReadLine();
            string results = "";
            if(dataString == null)
            {
                Textfield.text = results;
                endOfFile = true;
                break;
            }
            var dataValues = dataString.Split(',');
            for(int i = 0; i < dataValues.Length; i++)
            {
                results = "value:" + i.ToString() + " " + dataValues[i].ToString();
            }
        }
    }
}
