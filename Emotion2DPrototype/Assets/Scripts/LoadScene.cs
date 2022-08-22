using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class LoadScene : MonoBehaviour
{
    public string colors;
    public int lvl;
    public String time;
    private void Start() {
        this.colors = PlayerPrefs.GetString("currentColor");
        this.lvl = PlayerPrefs.GetInt("lvl");
        DateTime dataValuesStart = DateTime.Parse(PlayerPrefs.GetString("startTimeLevel"));
        DateTime datatValuesEnd = DateTime.Now;
        TimeSpan value = datatValuesEnd.Subtract(dataValuesStart);
        string output = string.Format("{0}:{1:00}", 
        (int)value.TotalMinutes, // <== Note the casting to int.
        value.Seconds); 
        this.time = output;
    }
    public void LoadNextScene()
    {
        //PlayerPrefs.SetInt("openDoor", 0);
        PlayerPrefs.SetInt("newScene",1);
        PlayerPrefs.Save();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void restartScene()
    {
        
        PlayerPrefs.SetInt("newScene",0);
        PlayerPrefs.Save();
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadPrevScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

}
