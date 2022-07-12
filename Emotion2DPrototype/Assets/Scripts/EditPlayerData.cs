using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class EditPlayerData : MonoBehaviour
{
    [SerializeField] private Player player;

    public void initializePlayer(){
        long milliseconds = DateTimeOffset.Now.ToUnixTimeMilliseconds();
        player.setID(milliseconds);
        player.setDeviceInfo(SystemInfo.deviceModel);
    }

    public void saveGenearlQuestionsData(){
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
                if(dataValues[2].Equals("Keine Angabe") && dataValues[3].Equals("Keine Angabe"))
                {
                    player.age = 0;
                    player.gender = 'n';
                    player.experience = int.Parse(dataValues[4]);
                } else if (dataValues[3].Equals("Keine Angabe"))
                {
                    player.age = int.Parse(dataValues[2]);
                    player.gender = 'n';
                    player.experience = int.Parse(dataValues[4]);
                } else if (dataValues[2].Equals("Keine Angabe"))
                {
                    player.age = 0;
                    player.gender = dataValues[3].ToCharArray()[0];
                    player.experience = int.Parse(dataValues[4]);
                } else
                {
                    player.age = int.Parse(dataValues[2]);
                    player.gender = dataValues[3].ToCharArray()[0];
                    player.experience = int.Parse(dataValues[4]);
                }
            }
            tempCount++;
        }
        //Remove File to Clean up Space
        //File.Delete(Application.persistentDataPath +"/GeneralData.csv");
    }

    public void saveIshiharaData()
    {
        string originPath = Application.persistentDataPath +"/Ishihara.csv";
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
                player.ishiharaResult = calculateScore(dataValues);
                for(int i = 0; i < player.ishiharaData.Length; i++){
                    player.setIshiharaData(i,int.Parse(dataValues[i+1]));
                }
            }
            tempCount++;
        }
        //Remove File to Clean up Space
        //File.Delete(Application.persistentDataPath +"/Ishihara.csv");
    }

    private char calculateScore(string[] dataValues)
    {
        int score = 0;
        //calculate score
        if (dataValues[1].Equals("12")){
            score++;
        }
        if (dataValues[2].Equals("8")){
            score++;
        } 
        if (dataValues[3].Equals("5")){
            score++;
        } 
        if (dataValues[4].Equals("29")){
            score++;
        } 
        if (dataValues[5].Equals("74")){
            score++;
        } 
        if (dataValues[6].Equals("7")){
            score++;
        } 
        if (dataValues[7].Equals("45")){
            score++;
        } 
        if (dataValues[8].Equals("2")){
            score++;
        } 
        if (dataValues[9].Equals("16")){
            score++;
        } 
        if (dataValues[10].Equals("35")){
            score++;
        } 
        if (dataValues[11].Equals("96")){
            score++;
        } 
        if(score <=7){
            return 'd';
        }  else 
        {
            return 'n';
        } 
    }
}
