using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCDialogue : MonoBehaviour
{
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private Text dialogueText;
    [SerializeField] private string[] dialogue;
    private int index;
    [SerializeField] private float wordSpeed;
    //private bool playerIsClose;
    [SerializeField] private GameObject interactButton;
    [SerializeField] private GameObject continueButton;
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
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player"))
        {
            //playerIsClose = true;
            interactButton.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if(other.CompareTag("Player"))
        {
            //playerIsClose = false;
            interactButton.SetActive(false);
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

}
