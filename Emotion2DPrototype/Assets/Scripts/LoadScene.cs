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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadPrevScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

}
