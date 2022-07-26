using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class MainMenu : MonoBehaviour
{
    public void PlayGame (){
        PlayerPrefs.SetString("startTime", DateTime.Now.ToString());
        PlayerPrefs.Save();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1 );
    }

    public void setTouchTrue()
    {
        PlayerPrefs.SetInt("touch", 1);
        PlayerPrefs.Save();
    }
    public void setTouchFalse()
    {
        PlayerPrefs.SetInt("touch", 0);
        PlayerPrefs.Save();
    }

    public void QuitGame(){
        Debug.Log("QUIT");
        Application.Quit();
    }
}
