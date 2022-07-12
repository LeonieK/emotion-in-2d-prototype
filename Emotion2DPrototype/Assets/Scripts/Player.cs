using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SAMResult {
    public char hue;
    public int saturation;
    public int brightness;
    public int arousal;
    public int valence;
    public int dominance;
    public int lvl;
}
[System.Serializable]
public class Player
{
    public long id;
    public int age;
    public char gender;
    public int experience;
    public string deviceInfo;
    public int[] ishiharaData = new int[10];
    public char ishiharaResult;
    public SAMResult[] samresults;

    public Player()
    {

    }

    public void setID(long id){
        this.id = id;
    }

    public void setDeviceInfo(string deviceInfo){
        this.deviceInfo = deviceInfo;
    }

    public string toStringGeneralPlayerInfos(){
        string infos = "ID: " + id.ToString() + " / Age: " + age.ToString() + " / Gender: " + gender.ToString()
        + " / Experience: " + experience.ToString() + " / Device Info: " + deviceInfo;
        return infos;
    }

    public void setIshiharaData(int index, int value){
        ishiharaData[index] = value;
    }
}
