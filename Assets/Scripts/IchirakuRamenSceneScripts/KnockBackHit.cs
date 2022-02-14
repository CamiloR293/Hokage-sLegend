using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class KnockBackHit : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (collision.GetComponent<NarutoMovement>().HealthController.MinHealth > 0)
            {
                collision.GetComponent<NarutoMovement>().Animator.SetTrigger("KnockBack");
                collision.GetComponent<NarutoMovement>().HealthController.Damage_ = true;

                if (transform.position.x > collision.transform.position.x)
                {
                    collision.GetComponent<NarutoMovement>().HealthController.KnockBack = -4;
                    collision.transform.rotation = Quaternion.Euler(0, 0, 0);
                }
                else
                {
                    collision.GetComponent<NarutoMovement>().HealthController.KnockBack = 4;
                    collision.transform.rotation = Quaternion.Euler(0, 0, 0);
                }

                collision.GetComponent<NarutoMovement>().HealthController.MinHealth -= 10;
            }
        }
    }
}
