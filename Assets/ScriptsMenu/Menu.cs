using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
public class Menu : MonoBehaviour
{
    // Start is called before the first frame update
  
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ScenePlay(){
     
        SceneManager.LoadScene(6);
    }

    public void SceneOption(){
         
        SceneManager.LoadScene(5);
      
    }

     public void ScenEXIT(){
         
        Application.Quit();
       
    }
}