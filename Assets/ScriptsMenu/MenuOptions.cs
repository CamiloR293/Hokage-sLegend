using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class MenuOptions : MonoBehaviour
{
    public Slider Volume;
  
    public Image Mute;
    public float slidervalue;
 
 

    void Start()
    {
        Volume.value = PlayerPrefs.GetFloat("VolumenAudio",0.5f);
        AudioListener.volume = Volume.value;
        MuteCheck();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

//Vuelve al menu principal
    public void ScenBack(){
        
        SceneManager.LoadScene(0);
        
    }
// El metodo MuteCheck se activara cuando el nivel del sonido sea cero 
    public void MuteCheck()
    {
        if(slidervalue ==0)
        {
            Mute.enabled = true;
        }
        else{
            Mute.enabled = false;

        }
    }
    // Este metodo cambia las variables del slider del volumen
    public void Change (float value)
    {
        slidervalue = value;
        PlayerPrefs.SetFloat("VolumenAudio",slidervalue);
        AudioListener.volume = slidervalue;
        MuteCheck();
    }
}
