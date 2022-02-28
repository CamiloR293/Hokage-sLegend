using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSasuke : MonoBehaviour
{
    public GameObject StamceEnd;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Instantiate(StamceEnd);
            Destroy(gameObject);
        }
    }
}
