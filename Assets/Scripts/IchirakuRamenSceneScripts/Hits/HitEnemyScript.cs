using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEnemyScript : MonoBehaviour
{

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<NarutoMovement>().Animator.SetTrigger("Damaging");
            collision.GetComponent<NarutoMovement>().Animator.SetInteger("Damage", Random.Range(1, 2));
            collision.GetComponent<NarutoMovement>().HealthController.Damage_ = true;

            if (transform.position.x > collision.transform.position.x)
            {
                collision.GetComponent<NarutoMovement>().HealthController.KnockBack = -1;
                collision.transform.rotation = Quaternion.Euler(0,0,0);
            }
            else
            {
                collision.GetComponent<NarutoMovement>().HealthController.KnockBack = -1;
                collision.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }
    }
       
}
