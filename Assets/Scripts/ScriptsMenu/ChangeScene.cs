using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
// Este prefab en forma de kunai(go) se utiliza para cambiar de niveles , solo es agregar el prefab
// al nivel y activar el bool pediendo del nivel
    public bool isLevel1;
    public bool isLevel2;
    public bool isLevel3;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerHitBox" && isLevel3)
        {
            SceneManager.LoadScene(4);
        }
        if (collision.gameObject.tag == "PlayerHitBox" && isLevel1)
        {
            SceneManager.LoadScene(2);
        }
        if (collision.gameObject.tag == "PlayerHitBox" && isLevel2)
        {
            SceneManager.LoadScene(3);
        }
    }
    
}  

