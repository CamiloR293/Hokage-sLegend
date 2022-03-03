using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactRasengan : MonoBehaviour
{
    private GameObject Naruto;
    
    private void Start()
    {
        Naruto = GameObject.Find("Naruto");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Naruto.GetComponent<NarutoMovement>().Animator.SetBool("HitRasengan", true);
            Naruto.GetComponent<NarutoMovement>().NoMoveRasengan();
        }
    }
}
