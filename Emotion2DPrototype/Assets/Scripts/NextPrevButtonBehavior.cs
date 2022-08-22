using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextPrevButtonBehavior : MonoBehaviour
{
    [SerializeField] private Text textField;
    [SerializeField] private Text textFieldCounter;
    [SerializeField] private string text1;
    [SerializeField] private string text2;
    private int currentText;

    private void Start() {
        currentText = 1;
        textFieldCounter.text = currentText+" / 2";
        textField.text = text1;
    }
    
    public void onNextButtonClicked()
    {
        currentText++;
        textFieldCounter.text = currentText+" / 2";
        if(currentText == 1)
        {
            textField.text = text1;
            textFieldCounter.text = "1 / 2";
        } else
        {
            textFieldCounter.text = "2 / 2";
            textField.text = text2;
        }
    }

    public void onPrevButtonClicked()
    {
        currentText--;
        
         if(currentText == 2)
        {
            textField.text = text2;
            textFieldCounter.text = "2 / 2";
        } else
        {
            textFieldCounter.text = "1 / 2";
            textField.text = text1;
        }
    }
}
