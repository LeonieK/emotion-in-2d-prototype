using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class SetNeutrals : MonoBehaviour
{
    [SerializeField] Material material;
    [SerializeField] Color color;
    void Start()
    {
        changeColors();
    }
  
    public void changeColors()
    {
        var lights = FindObjectsOfType<Light2D>();
        material.SetFloat("_Saturation", 0f);
        for(int i = 0; i < lights.Length; i++)
        {
            if(lights[i].CompareTag("BackgroundLight"))
            {
                lights[i].GetComponent<Light2D>().color = color;
            } else if(lights[i].CompareTag("EnvironmentLight") )
            {
                lights[i].GetComponent<Light2D>().color = color;
            } else if(!lights[i].CompareTag("dontChange"))
            {
                //Don't change
            }
        }
    }
}
