using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class SetColors : MonoBehaviour
{
    Dictionary<string, Color32> colors = new Dictionary<string, Color32>();
    [SerializeField] Material material;
    void Start()
    {
        //initialize color values
        //hue = char; brightness = 1 - high, 0 - low; saturation 1-high, 0-low;
        colors.Add("b11", new Color32(0 ,110 ,255,255 ));
        colors.Add("b10", new Color32(111 ,116 ,157,255 ));
        colors.Add("b01", new Color32(0 ,44 ,113,255 ));
        colors.Add("b00", new Color32(46 ,47 ,63,255 ));
        colors.Add("g11", new Color32(0 ,149 ,0,255 ));
        colors.Add("g10", new Color32(87 ,128 ,97,255 ));
        colors.Add("g01", new Color32(0 ,59 ,0,255 ));
        colors.Add("g00", new Color32(39 ,51 ,44,255 ));
        colors.Add("r11", new Color32(246 ,0 ,0,255 ));
        colors.Add("r10", new Color32(158 ,106 ,93,255 ));
        colors.Add("r01", new Color32(99 ,16 ,0,255 ));
        colors.Add("r00", new Color32(61 ,44 ,43,255 ));
        colors.Add("y11", new Color32(255 ,255 ,0,255 ));
        colors.Add("y10", new Color32(234 ,228 ,176,255 ));
        colors.Add("y01", new Color32(106 ,96 ,0,255 ));
        colors.Add("y00", new Color32(103 ,93 ,70,255 ));
        colors.Add("n00", new Color32(51 ,48 ,48,255 ));
        colors.Add("n10", new Color32(121 ,118 ,119,255 ));
        if(PlayerPrefs.GetInt("newScene") == 1)
        {
            changeColors();
        }
    }
  
    public void changeColors()
    {
        Color color = getRandomColor();
        Debug.Log("Color: " + color.ToString());
        var lights = FindObjectsOfType<Light2D>();
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

    public Color getRandomColor()
    {
        //char h = PlayerPrefs.GetString("h").ToCharArray()[0];
        string colorString = PlayerPrefs.GetString("colors");
        var colorValues = colorString.Split(',');
        int randomNumber = Random.Range(0,colorValues.Length-1);
        colorString.Replace(colorValues[randomNumber]+",", "");
        Debug.Log("Set ColorString to: " + colorString);
        PlayerPrefs.SetString("color", colorString);
        PlayerPrefs.Save();
        
        Debug.Log("Color Values:" + colorValues[randomNumber]);
        
        if (colorValues[randomNumber].Equals("n1")){
            string temp = colorValues[randomNumber] +"0";
            PlayerPrefs.SetString("currentColor", temp);
            PlayerPrefs.Save();
            Debug.Log("Set Saturation: " + material.GetFloat("_Saturation").ToString() + "to 0");
            material.SetFloat("_Saturation", 0f);
            return colors[temp];
        } else 
        {
            string temp = colorValues[randomNumber];
            PlayerPrefs.SetString("currentColor", colorValues[randomNumber]); 
            PlayerPrefs.Save();
            Debug.Log("Set Saturation: " + material.GetFloat("_Saturation").ToString() + "to 1");
            material.SetFloat("_Saturation", 1f);
            return colors[temp];
        }
        
    }
}
