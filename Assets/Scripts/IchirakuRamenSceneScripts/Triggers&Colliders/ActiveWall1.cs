using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveWall1 : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerHitBox"))
        {
            Debug.Log("Activar");
        }
    }
}
