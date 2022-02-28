using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopRaikiri : MonoBehaviour
{
    public GameObject Kakashi;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerHitBox"))
        {
            Kakashi.GetComponent<KakashiMovement>().raikiri = false;
        }
    }
}
