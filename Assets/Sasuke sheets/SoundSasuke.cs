using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSasuke : MonoBehaviour
{
    public GameObject Sasuke;
    public GameObject StamceEnd;
    public float InitLife;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Instantiate(StamceEnd);
            Destroy(gameObject);
            Sasuke.GetComponent<MovimientoSasuke>().InitialLife(InitLife);
        }
    }
}
