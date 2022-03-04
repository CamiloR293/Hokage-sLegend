using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundNinja : MonoBehaviour
{
    
    public float rangeOfVision = 1.7f;
    private float timeAproach = 3;
    private float timeRun = 3;
    private float timeBreak = 1f;
    private float timeAttack = 0;
    private float timeWalk = 1.4f;
    public float health = 50;
    private int opcion;
    private int i;
    private bool walking = false;
    private bool attack;
    private bool damage;
    private bool lockAnim = false;
    Vector3 direccion;

    public GameObject Naruto;
    private Rigidbody2D Rigidbody2D;
    private Animator animator;
    private float playerDistance;



    void Start()
    {
        animator = GetComponent<Animator>();
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Naruto = GameObject.Find("Naruto");
    }

    void Update()
    {
        if (health > 0)
        {
            playerDistance = Naruto.GetComponent<Transform>().position.x - transform.position.x;
            if (!lockAnim)
            {
                //=============================================== CUANDO SE ENCUENTRA EL JUGADOR EN RANGO DE VISION==========================================
                if ((Mathf.Abs(playerDistance) > 0.3) && (Mathf.Abs(playerDistance) <= rangeOfVision) && !animator.GetBool("Combo"))
                {
                    if (playerDistance < 0)
                    {
                        transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                        direccion = new Vector3(1.0f, 0.0f, 0.0f);
                    }
                    else
                    {
                        transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
                        direccion = new Vector3(-1.0f, 0.0f, 0.0f);
                    }
                    animator.SetBool("Walk", false);
                    ResetTimerWalk();
                    Acercarse();
                    ResetTimerRun();
                }
                else
                {
                    //======================== CAMINAR RANDOM =========================

                    animator.SetBool("Run", false);
                    TimerRun();
                    if (timeRun <= 0 && (Mathf.Abs(playerDistance) > 0.5))
                    {
                        if (!walking) opcion = Random.Range(1, 3);
                        if (timeWalk > 0)
                        {
                            switch (opcion)
                            {
                                case 1: //Caminar hacia delante
                                    TimerWalk();
                                    
                                    animator.SetBool("Walk", true);
                                    transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                                    direccion = new Vector3(-1.0f, 0.0f, 0.0f);
                                    if (timeWalk > 0) transform.Translate(direccion * 0.2f * Time.deltaTime, Space.World);
                                    break;
                                case 2: //Caminar hacia atras
                                    TimerWalk();
                                    
                                    animator.SetBool("Walk", true);
                                    transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
                                    direccion = new Vector3(1.0f, 0.0f, 0.0f);
                                    if (timeWalk > 0) transform.Translate(direccion * 0.2f * Time.deltaTime, Space.World);
                                    break;
                            }
                        }
                        if (timeWalk <= 0)
                        {
                            animator.SetBool("Walk", false);
                            ResetTimerRun();
                            ResetTimerWalk();
                            walking = false;
                        }
                    }
                }

                if (Mathf.Abs(playerDistance) <= 0.5)
                {
                    timeAttack -= Time.deltaTime;
                    if (timeAttack < 0) animator.SetBool("Combo", true);
                }
                else
                {
                    animator.SetBool("Combo", false);
                }
            }

            DamageTranslate();
            PunchTranslate();
        }
        else
        {
            if (i == 0)
            {
                i++;
                animator.SetTrigger("Death");
            }
        }
    }



    //=====================================================================================
    //                             FUNCIONES DE ENEMIGO
    //=====================================================================================
    void Acercarse()
    {
        timeBreak -= Time.deltaTime;
        
        if (timeBreak <= 0)
        {
            animator.SetBool("Run", true);
            transform.Translate(-direccion * 1f * Time.deltaTime, Space.World);
            
        }
        
    }

    void PunchTranslate()
    {
        if (attack) transform.Translate(-direccion * 0.5f * Time.deltaTime, Space.World);
    }

    void DamageTranslate()
    {
        if (damage && Naruto.GetComponent<NarutoMovement>().KnockBackHit)
        {
            transform.Translate(direccion * 1.5f * Time.deltaTime, Space.World);
            transform.Translate(Vector2.up * 1.5f * Time.deltaTime, Space.World);

        }
        else if (damage)
        {
            transform.Translate(direccion * 0.3f * Time.deltaTime, Space.World);
        }
    }
    //=====================================================================================
    //                                LLAMADAS IN-GAME
    //=====================================================================================

    public void OnDamage()
    {
        timeBreak = 1f;
        damage = true;
        lockAnim = true;
        NoAttack();
    }
    public void OffDamage()
    {
        damage = false;
    }
    public void UnlockAnimation()
    {
        lockAnim = false;
    }
    public void Attack()
    {
        attack = true;
        timeBreak = 0.5f;
    }
    public void NoAttack()
    {
        attack = false;
    }

    public void Destruir()
    {
        Destroy(gameObject);
    }
    //=====================================================================================
    //                                  CRONOMETROS
    //=====================================================================================

    void ResetTimerAttack()
    {
        timeAttack = 1;
        animator.SetBool("Combo", false);
    }

    void ResetTimerRun()
    {
        timeRun = 3;
    }
    void TimerRun()
    {
        timeRun -= Time.deltaTime;
    }
    void ResetTimerWalk()
    {
        timeWalk = 1.4f;
        walking = false;
    }
    void TimerWalk()
    {
        timeWalk -= Time.deltaTime;
        walking = true;
    }

    //=====================================================================================
    //                               COLLISIONES/TRIGGERS
    //=====================================================================================

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            animator.SetBool("Grounded", true);
            animator.SetBool("KnockBack", false);
        }
    }
    
    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            animator.SetBool("Grounded", false);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && health > 0)
        {
            animator.SetBool("Run", false);
            animator.SetTrigger("Damage");
            OnDamage();
            timeAproach = 3;
            if (Naruto.GetComponent<NarutoMovement>().KnockBackHit)
            {
                animator.SetBool("KnockBack", true);
                timeAproach = 3;
            }
            health -= Naruto.GetComponent<NarutoMovement>().hitDamage;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("SpecialHit") && health > 0)
        {
            animator.SetBool("Run", false);
            animator.SetTrigger("Damage");
            OnDamage();
            timeAproach = 3;
            if (Naruto.GetComponent<NarutoMovement>().KnockBackHit)
            {
                animator.SetBool("KnockBack", true);
                timeAproach = 3;
            }
            health -= Naruto.GetComponent<NarutoMovement>().hitDamage;
        }
    }
}
