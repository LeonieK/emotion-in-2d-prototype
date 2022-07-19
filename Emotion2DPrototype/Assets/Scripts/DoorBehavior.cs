using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorBehavior : MonoBehaviour
{
    [SerializeField] private GameObject interactButton;
    private NPCDialogue script;
    // Start is called before the first frame update
    void Start()
    {
        script = GameObject.Find("NPC").GetComponent<NPCDialogue>();
    }


    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player") && PlayerPrefs.GetInt("openDoor")==1)
        {
            interactButton.SetActive(true);
        } else if (other.CompareTag("Player") && PlayerPrefs.GetInt("openDoor")==0)
        {
            script.flip();
            script.onInteractButtonClicked();
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.CompareTag("Player"))
        {
            interactButton.SetActive(false);
        }
    }
     public void onInteractButtonClicked(){
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            interactButton.SetActive(false);
    }
}
