using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallChainNinjaScript : MonoBehaviour
{


    public float rangeOfVision;
    public float rangeOfAttack;
    public float Health = 50.0f;

    public Transform player;

    private Rigidbody2D Rigidbody2D;
    private Animator Animator;
    public GameObject Player;
    private float playerDistance;
    private bool Locked;
    public bool KnockBack;
    bool Hit;
    void Start()
    {
        Animator = GetComponent<Animator>();
        Rigidbody2D = GetComponent<Rigidbody2D>();
        
    }
    
    //Start Update
    void Update()
    {
        if (Health > 0)
        {
            playerDistance = Vector2.Distance(player.position, Rigidbody2D.position);


            Animator.SetBool("PreparingAttack", playerDistance <= rangeOfVision);
            Animator.SetBool("Attack", playerDistance <= rangeOfAttack);

            if (!Locked)
            {
                if (player.position.x < Rigidbody2D.position.x) transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                else transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
            }
        }
    }
    //End Update

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Player"))
        {

            if (!Player.GetComponent<NarutoMovement>().KnockBackHit) 
            { 
                Animator.SetTrigger("Damage");
                Hit = true;
            }
            else
            {
                Animator.SetTrigger("KnockBack");
                Hit = false;
            }
            Damage();
            Health -= Player.GetComponent<NarutoMovement>().hitDamage;
        }
    }
    public void Lock()
    {
        Locked = true;
    }
    public void Unlock()
    {
        Locked = false;
    }

    public void Damage()
    {
        int KnockBack = 0;
        if (Health > 0)
        {
            if (transform.position.x > player.position.x)
            {
                KnockBack = -4;
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else
            {
                KnockBack = 4;
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }
        if (Hit)
        {
            transform.Translate(Vector3.right * KnockBack * Time.deltaTime, Space.World);

        }
    }

    public void FinishDamage()
    {
        Hit = false;
    }


    //Debugger code
    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.blue;
    //    Gizmos.DrawWireSphere(transform.position, rangeOfVision);
    //    Gizmos.DrawWireSphere(transform.position, rangeOfAttack);
    //}
    
}
