using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallPotion : MonoBehaviour
{
    GameObject Naruto;
    public bool isSmallHeal;
    private bool isCollected;
    public bool isPotion;
    public bool isKunai;
    private void Awake()
    {
        Naruto = GameObject.Find("Naruto");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("PlayerHitBox") && !isCollected )
        {
            if(isSmallHeal)
            {
                Naruto.GetComponent<NarutoMovement>().HealthController.MinHealth += 5;
                 isCollected = true;
                 Destroy(gameObject); 
                 
            }
            if(isPotion)
            {
                Naruto.GetComponent<NarutoMovement>().HealthController.MinHealth += 15;
                isCollected = true;
                 Destroy(gameObject); 
                 
            }

            if(isKunai)
            {
                
                 isCollected = true;
                 Destroy(gameObject); 
                 
            }
        }
    }
}
