using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject pause;
    
    [SerializeField] private GameObject Pausemenu;

    public AudioSource audioSource;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
// El metodo Pause , esta asignado el boton Pause del video juego, este hara que detenga y activara el menu de opciones
    public void Pause()
    {
        Time.timeScale =0f;
        pause.SetActive(false);
        Pausemenu.SetActive(true);
        audioSource.Pause();

    }
// El siguiente metodo vuelve hacer que el juego continue
    public void Resume(){
        Time.timeScale = 1f;
         pause.SetActive(true);
        Pausemenu.SetActive(false);
         audioSource.Play();

    }
// Reinicia el nivel en el cual se esta jugando
    public void Reset(){
         Time.timeScale = 1f;
         SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }
// Sale del video juego
    public void Quit(){
        Application.Quit();
    }



}
