using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject pause;
    
    [SerializeField] private GameObject Pausemenu;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Pause()
    {
        Time.timeScale =0f;
        pause.SetActive(false);
        Pausemenu.SetActive(true);

    }

    public void Resume(){
        Time.timeScale = 1f;
         pause.SetActive(true);
        Pausemenu.SetActive(false);

    }

    public void Reset(){
         Time.timeScale = 1f;
         SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }

    public void Quit(){
        Application.Quit();
    }



}
