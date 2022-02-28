using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
// Este prefab en forma de kunai(go) se utiliza para cambiar de niveles , solo es agregar el prefab
// al nivel y en loadscene cambiar el numero dependiendo la escena
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerHitBox")
        {
            SceneManager.LoadScene(4);
        }
    }
    
}  

