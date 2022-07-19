using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NPCDialogue : MonoBehaviour
{
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private Text dialogueText;
    [SerializeField] private string[] dialogue;
    private int index;
    [SerializeField] private float wordSpeed;
    //private bool playerIsClose;
    [SerializeField] private GameObject talkButton;
    [SerializeField] private GameObject continueButton;
    [SerializeField] private GameObject player;
    // Update is called once per frame
    void Update()
    {
        if(dialogueText.text == dialogue[index])
        {
            continueButton.SetActive(true);
        }
    }
    
    public void zeroText()
    {
        dialogueText.text = "";
        index = 0;
        dialoguePanel.SetActive(false);
    }

    IEnumerator Typing()
    {
        foreach(char letter in dialogue[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
    }
    public void NextLine()
    {
        continueButton.SetActive(false);
        if(index < dialogue.Length-1)
        {
            index++;
            dialogueText.text = "";
            StartCoroutine(Typing());
        } else{
            zeroText();
            PlayerPrefs.SetFloat("playerPosx",player.transform.position.x);
            PlayerPrefs.SetFloat("playerPosy",player.transform.position.y);
            PlayerPrefs.SetFloat("playerPosz",player.transform.position.z);
            PlayerPrefs.SetInt("openDoor", 1);
            PlayerPrefs.Save();
            changeScene();
        }
    }

    public void changeScene()
    {
        SceneManager.LoadScene("IshiharaQuestionnaire");
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player"))
        {
            //playerIsClose = true;
            talkButton.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if(other.CompareTag("Player"))
        {
            //playerIsClose = false;
            talkButton.SetActive(false);
            zeroText();
        }
    }
    public void onInteractButtonClicked(){
        if(dialoguePanel.activeInHierarchy)
        {
            zeroText();
        }else
        {
            dialoguePanel.SetActive(true);
            StartCoroutine(Typing());
        }
    }
    public void flip()
    {
        transform.localScale = new Vector3(1,1,1);
    }

}
