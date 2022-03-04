using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallChainNinjaScript : MonoBehaviour
{
    

    public float rangeOfVision;
    public float rangeOfAttack;
    public float Health = 50.0f;
    public float KnockBackTranslate;
    private float Cronometro = 3;
    private float playerDistance;
    private float upTranslate;
    
    public bool damage;
    public bool KnockBack;
    public bool Hit;
    public bool TranslateBool;
    private bool Locked;

    public NarutoMovement Naruto;
    private Rigidbody2D Rigidbody2D;
    private Animator Animator;
    public GameObject Player;
    public Vector2 direccion;
    public Transform player;


    void Start()
    {
        Animator = GetComponent<Animator>();
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Player = GameObject.Find("Naruto");
        player = Player.transform;
        Naruto = Player.GetComponent<NarutoMovement>();
    }
    
    //Start Update
    void Update()
    {
        Translate();
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
                    this.direccion.x = 1;
                }
                else
                {
                    transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
                    this.direccion.x = -1;
                }
            }
        }
        else if (Animator.GetBool("Grounded"))
        {
            Animator.SetBool("Death", true);
            Rigidbody2D.velocity = new Vector3(0, 0, 0);
            Cronometro -= Time.deltaTime;
            if (Cronometro <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
    //End Update

    //=====================================================================================
    //                                FUNCIONES ENEMIGO
    //=====================================================================================
    public void Translate()
    {
        if (damage && Naruto.GetComponent<NarutoMovement>().KnockBackHit)
        {
            transform.Translate(direccion * 1.5f * Time.deltaTime, Space.World);
            transform.Translate(Vector2.up * upTranslate * Time.deltaTime, Space.World);

        }
        else if (damage)
        {
            transform.Translate(direccion * 0.3f * Time.deltaTime, Space.World);
        }
    }
    //=====================================================================================
    //                             TRIGGERS / COLISIONES
    //=====================================================================================



    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (Player.GetComponent<NarutoMovement>().KnockBackHit)
            {
                upTranslate = 1.5f;
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
            if (Player.GetComponent<NarutoMovement>().KnockBackHit)
            {
                upTranslate = 2f;
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

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            Animator.SetBool("Grounded", true);
            Animator.SetBool("Falling", false);
        }
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground") Animator.SetBool("Grounded", false);
        
    }

    //=====================================================================================
    //                                LLAMADAS IN-GAME
    //=====================================================================================
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

    //Activa/Apaga el daño para activar el retroceso por golpe
    public void OnDamage()
    {
        damage = true;       
    }
    public void OffDamage()
    {
        damage = false;
    }
    //Fin Daño
}
