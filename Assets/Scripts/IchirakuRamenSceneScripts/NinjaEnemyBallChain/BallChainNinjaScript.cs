using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallChainNinjaScript : MonoBehaviour
{
    public NarutoMovement Naruto = new NarutoMovement();

    public float rangeOfVision;
    public float rangeOfAttack;
    public float Health = 50.0f;
    public Vector2 direccion;
    public Transform player;
    public float KnockBackTranslate;
    float Cronometro = 3;

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
        else if (Animator.GetBool("Grounded"))
        {
            if (Animator.GetBool("Death"))
            {
                Rigidbody2D.velocity = new Vector3(0, 0, 0);
                Cronometro -= Time.deltaTime;
                if (Cronometro <= 0)
                {
                    Destroy(gameObject);
                }
            }
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
        if (Health > 0) KnockBackTranslate = -1;    
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {
            Hit = true;
            
            if (Player.GetComponent<NarutoMovement>().KnockBackHit)
            {
                KnockBackTranslate = -1.5f;
                Animator.SetTrigger("KnockBack");
                Animator.SetBool("Falling", true);
            }
            else
            {
                Animator.SetTrigger("Damage");
            }

            Health -= Player.GetComponent<NarutoMovement>().hitDamage;
        }
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("SpecialHit"))
        {
            Hit = true;
            KnockBackTranslate = -0.2f;
            if (Player.GetComponent<NarutoMovement>().KnockBackHit)
            {
                KnockBackTranslate = -2;
                Animator.SetTrigger("KnockBack");
                Animator.SetBool("Falling", true);
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
        if (Hit)
        {
            Vector3 direccion = new Vector3(this.direccion.x, 0, 0);
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
        if(TranslateBool) transform.Translate(Vector3.up * Naruto.UpForce, Space.World);
    }
    
    public void Bounce()
    {
        Naruto.UpForce = 0.02f;
    }


    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground") Animator.SetBool("Grounded", true);
        Animator.SetBool("Falling", false);
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground") Animator.SetBool("Grounded", false);
        
    }

    public void FinishDamage()
    {
        
        Hit = false;
    }



}
