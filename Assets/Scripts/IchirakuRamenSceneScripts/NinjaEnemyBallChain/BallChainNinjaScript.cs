using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallChainNinjaScript : MonoBehaviour
{


    public float rangeOfVision;
    public float rangeOfAttack;
    public float Health = 50.0f;
    public Vector2 direccion;
    public Transform player;
    public int KnockBackTranslate;
    public float UpForce;
    private Rigidbody2D Rigidbody2D;
    private Animator Animator;
    public GameObject Player;
    private float playerDistance;
    private bool Locked;
    public bool KnockBack;
    public bool Hit;
    public bool TranslateBool;


    void Start()
    {
        Animator = GetComponent<Animator>();
        Rigidbody2D = GetComponent<Rigidbody2D>();
        
    }
    
    //Start Update
    void Update()
    {
        Damage();
        Translate();
        UpTranslate();
        HealthEnemy();


        if (Health > 0)
        {
            playerDistance = Vector2.Distance(player.position, Rigidbody2D.position);


            Animator.SetBool("PreparingAttack", playerDistance <= rangeOfVision);
            Animator.SetBool("Attack", playerDistance <= rangeOfAttack);

            if (!Locked)
            {
                if (player.position.x < Rigidbody2D.position.x)
                {
                    transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                    this.direccion.x = -1;
                }
                else
                {
                    transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
                    this.direccion.x = 1;
                }
            }
        }
        else
        {
            Rigidbody2D.velocity = new Vector3(0, 0, 0);
        }
    }
    //End Update

    

    //Bloqueo al atacar
    public void Lock()
    {
        Locked = true;
    }
    public void Unlock()
    {
        Locked = false;
    }
    //===================



    public void HealthEnemy()
    {
        if (Health <= 0) Animator.SetBool("Death", true);
    }

    public void Damage()
    {
        
        if (Health > 0)
        {
            //if (transform.position.x > player.position.x)
            //{
                KnockBackTranslate = -1;
            //}
            //else
            //{
                //KnockBackTranslate = 1;
            //}
        }
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {
            Hit = true;
            if (Player.GetComponent<NarutoMovement>().KnockBackHit)
            {
                Animator.SetTrigger("KnockBack");
                UpForce = 0.001f;
            }
            else
            {
                Animator.SetTrigger("Damage");
            }

            Health -= Player.GetComponent<NarutoMovement>().hitDamage;
        }
    }

    public void Translate()
    {
        Vector3 direccion = new Vector3(this.direccion.x, 0, 0);
        if (Hit)
        {
            transform.Translate(direccion * KnockBackTranslate * Time.deltaTime, Space.World);
        }
    }

    public void TrueTranslate()
    {
        TranslateBool = true;
    }

    public void EndTranslate()
    {
        TranslateBool = false;
    }

    public void UpTranslate()
    {
        if(TranslateBool) transform.Translate(Vector3.up * UpForce, Space.World);
    }

    

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground") Animator.SetBool("Grounded", true);
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground") Animator.SetBool("Grounded", false);
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
