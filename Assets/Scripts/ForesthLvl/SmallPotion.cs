using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallPotion : MonoBehaviour
{
    public bool issmallHeal;
    private bool isCollected;

    public bool isPotion;

    public bool isKunai;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("PlayerHitBox") && !isCollected )
        {
            if(issmallHeal)
            {
                
                 isCollected = true;
                 Destroy(gameObject); 
                 
            }
            if(isPotion)
            {
                
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
