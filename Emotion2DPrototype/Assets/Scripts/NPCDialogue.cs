using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NPCDialogue : MonoBehaviour
{
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private Text dialogueText;
    [SerializeField] private string[] dialogueEnoughCoins;
    [SerializeField] private string[] dialogueNotEnoughCoins;
    [SerializeField] private int coinNr;
    private int index;
    [SerializeField] private float wordSpeed;
    [SerializeField] private GameObject talkButton;
    [SerializeField] private GameObject continueButton;
    [SerializeField] private GameObject player;

    void Update()
    {
        if(PlayerPrefs.GetInt("coins") >= coinNr && dialogueText.text == dialogueEnoughCoins[index])
        {
                continueButton.SetActive(true);
        }
        if(PlayerPrefs.GetInt("coins") < coinNr && dialogueText.text == dialogueNotEnoughCoins[index])
        {
                continueButton.SetActive(true);
        }
        
    }
    
    public void zeroText()
    {
        dialogueText.text = "";
        index= 0;
        dialoguePanel.SetActive(false);
    }
    IEnumerator TypingEnough()
    {
        foreach(char letter in dialogueEnoughCoins[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
    }
    IEnumerator TypingNotEnough()
    {
        foreach(char letter in dialogueNotEnoughCoins[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
    }
    public void NextLine()
    {
        continueButton.SetActive(false);
        if(PlayerPrefs.GetInt("coins") >= coinNr)
        {
            if(index < dialogueEnoughCoins.Length-1)
            {
                dialogueText.text = "";
                index++;
                StartCoroutine(TypingEnough());
            } else 
            {
                player.GetComponent<PlayerMovement>().canMove = true;
                zeroText();
                PlayerPrefs.SetInt("openDoor", 1);
                PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins")-coinNr);
                PlayerPrefs.Save();
                changeScene();
            }
        }else
        {
            if(index < dialogueNotEnoughCoins.Length-1)
            {
                dialogueText.text = "";
                index++;
                StartCoroutine(TypingNotEnough());
            }else 
            {
                player.GetComponent<PlayerMovement>().canMove = true;
                zeroText();
            }
        }
    }

    public void changeScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player"))
        {
            talkButton.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if(other.CompareTag("Player"))
        {
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
            //disable movement
            player.GetComponent<PlayerMovement>().canMove = false;
            if(PlayerPrefs.GetInt("coins")>=coinNr)
            {
                StartCoroutine(TypingEnough());
            }
            else 
            {
                StartCoroutine(TypingNotEnough());
            }
           
        }
    }
    public void flip()
    {
        transform.localScale = new Vector3(1,1,1);
    }

}
