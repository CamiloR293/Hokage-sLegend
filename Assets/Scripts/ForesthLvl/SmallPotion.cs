using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallPotion : MonoBehaviour
{
    public bool issmallHeal;
    private bool isCollected;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("PlayerHitBox") && !isCollected )
        {
            if(issmallHeal)
            {
                
               /*  if(PlayerHealtController.insance.currenthHealt != PlayerHealtController.insace.maxHealt)
                 {
                 PlayerHealtController.insance.HealPlayer();
                 isCollected = true;
                 Destroy(gameObject);
                 }
                 */
                 
                 
            }
        }
    }
}
