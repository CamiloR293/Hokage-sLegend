using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Brightness : MonoBehaviour
{
    // Start is called before the first frame update

    public Slider slider;
    public float slidervalue;

    public Image ness; 
    void Start()
    {
        slider.value = PlayerPrefs.GetFloat("Brigth",0.5f);
        ness.color =  new Color(ness.color.r,ness.color.g,ness.color.b,slider.value);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
// Cambia el valor del slider y aumenta o disminuye el brillo
    public void ChangeSlider(float value )
    {
slidervalue = value;
PlayerPrefs.SetFloat("ness",slidervalue);
 ness.color =  new Color(ness.color.r,ness.color.g,ness.color.b,slider.value);


    }
}
