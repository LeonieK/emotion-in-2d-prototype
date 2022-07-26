using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Popup : MonoBehaviour
{
    [SerializeField] private GameObject theUI;
    
    public void closePopup()
    {
        theUI.SetActive(false);
    }
    public void openPopup()
    {
        theUI.SetActive(true);
    }
}
