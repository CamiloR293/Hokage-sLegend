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
//Al seleccionar el boton "Play" Cargara la escena donde se encuentra las instrucciones de los controles 
    public void ScenePlay(){
     
        SceneManager.LoadScene(6);
    }
// Carga la escena donde se encuentra el menu de opciones
    public void SceneOption(){
         
        SceneManager.LoadScene(5);
      
    }
// Sale del video juego
     public void ScenEXIT(){
         
        Application.Quit();
       
    }
}
