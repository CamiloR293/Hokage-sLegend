using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Instructions : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
// Incia el video juego, en el primer nivel
      public void SceneLvl(){
     
        SceneManager.LoadScene(1);
    }
}
