using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public string colors;
    public int lvl;
    private void Start() {
        this.colors = PlayerPrefs.GetString("currentColor");
        this.lvl = PlayerPrefs.GetInt("lvl");
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
