using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KakashiMovement : MonoBehaviour
{
    
    private float playerDistance;
    protected float health = 200;
    private float timerRun = 3;
    private float timeAproach = 3;
    private float timeRaikiri = 1;
    private float chargeRaikiri = 3;
    private float timeHit = 1f;
    private float nextShuriken = 3;
    private int opcion;
    private int i = 0;
    private bool damage;
    private bool knockBack;
    private float jumpForce = 170;
    private bool combo;
    private bool hit;
    public bool raikiri;
    
    Vector3 direccion;


    [Header("Components")]
    public BoxCollider2D raikiriCollider;
    private Rigidbody2D rigidbody2D;
    public Transform playerPosition;
    private Animator animator;
    public GameObject Naruto;


    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        raikiriCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if (health > 0) {
            if (!Naruto.GetComponent<NarutoMovement>().Animator.GetBool("Death"))
            {
                playerDistance = (transform.position.x - playerPosition.position.x);
                if (!animator.GetBool("Combo") && !animator.GetBool("Raikiri"))
                {
                    if (playerDistance < 0)
                    {
                        transform.localScale = new Vector3(1, 1, 1);
                        direccion = new Vector3(1.0f, 0.0f, 0.0f);
                    }
                    else
                    {
                        direccion = new Vector3(-1.0f, 0.0f, 0.0f);
                        transform.localScale = new Vector3(-1, 1, 1);
                    }
                }

                Combo();
                PunchTranslate();
                Damage();
                Raikiri();
                NextShuriken();
                if (!animator.GetBool("Animating"))
                {
                    timerRun = TimerRun();

                    if (playerDistance > 2) chargeRaikiri = RaikiriTimer();

                    if (chargeRaikiri <= 0)
                    {
                        animator.SetBool("Animating", true);
                        ResetTimerRaikiri();
                    }

                    if (!animator.GetBool("Animating"))
                    {
                        if (timerRun <= 0)
                        {
                            Acercarse();
                            if (!animator.GetBool("Run")) ResetTimerRun();
                        }
                    }
                }
            }
            else
            {
                animator.SetBool("StartAnim", true);
            }
        }
        else
        {
            if (i == 0)
            {
                i++;
                animator.SetTrigger("Damage");
            }
            
            animator.SetBool("Death", true);
        }
    }

    
    //=====================================================================================
    //                                  CRONOMETROS
    //=====================================================================================

    void ResetTimerRun()
    {
        timerRun = 3;
    }
    float TimerRun()
    {
        timerRun -= Time.deltaTime;
        return timerRun;
    }

    float RaikiriTimer()
    {
        chargeRaikiri -= Time.deltaTime;
        if(chargeRaikiri <= 0)
        {
            animator.SetBool("Raikiri", true);
        }
        return chargeRaikiri;
    }
    void ResetTimerRaikiri()
    {
        chargeRaikiri = 3;
    }

    void NextShuriken()
    {
        if(playerDistance > 4)
        {
            nextShuriken -= Time.deltaTime;
        }
        if (nextShuriken <= 0)
        {
            animator.SetTrigger("Throw");
            nextShuriken = 3;
        }
    }

    //=====================================================================================

    //=====================================================================================
    //                             FUNCIONES DE ENEMIGO
    //=====================================================================================

    void Jump()
    {
        if (!animator.GetBool("Animating"))
        {
            animator.SetBool("Jump", true);
            rigidbody2D.AddForce(Vector2.up * jumpForce);
        }
    }

    void Combo()
    {
        timeHit -= Time.deltaTime;
        if ((Mathf.Abs(playerDistance) < 0.6f) && timeHit <= 0)
        {
            animator.SetBool("Combo", true);
            animator.SetBool("Animating", false);
            combo = true;
        }
        else
        {
            animator.SetBool("Combo", false);
            animator.SetBool("Animating", false);
        }
    }
    
    void PunchTranslate()
    {
        if (hit) // Se llama in-game
        {
            transform.Translate(direccion * 0.2f * Time.deltaTime, Space.World);
        }
    }

    void Acercarse()
    {
        if (!animator.GetBool("Animating") && !animator.GetBool("Raikiri"))
        {
            timeAproach -= Time.deltaTime;
            if (timeAproach >= 0)
            {
                animator.SetBool("Run", true);
                transform.Translate(direccion * 2f * Time.deltaTime, Space.World);
                if (Mathf.Abs(playerDistance) < 0.6) timeAproach = 0;
            }
            else
            {
                animator.SetBool("Run", false);
                timeAproach = 3;
            }
        }
        else
        {
            animator.SetBool("Run", false);
            timeAproach = 3;
        }
    }

    void Raikiri()
    {
        if (raikiri)
        {
            transform.Translate(direccion * 5f * Time.deltaTime, Space.World);
        }
    }

    void Damage()
    {
        if (damage && Naruto.GetComponent<NarutoMovement>().KnockBackHit)
        {
            transform.Translate(direccion * -1.5f * Time.deltaTime, Space.World);
            transform.Translate(Vector2.up * 1.5f * Time.deltaTime, Space.World);
            
        }else if (damage)
        {
            transform.Translate(direccion * -0.3f * Time.deltaTime, Space.World);
        }
    }

    void Destruir()
    {
        
        Destroy(gameObject);
    }
    //=====================================================================================
    //                              LLAMADAS IN-GAME
    //=====================================================================================

    void Hit()
    {
        hit = true;
    }
    void NoHit()
    {
        hit = false;
    }

    void OnDamage()
    {
        damage = true;
    }
    void OffDamage()
    {
        damage = false;
    }
    

    void RaikiriOn()
    {
        raikiri = true;
    }
    void RaikiriOff()
    {
        animator.SetBool("Animating", false);
        animator.SetBool("Raikiri", false);
        ResetTimerRaikiri();
        raikiri = false;
    }

    //=====================================================================================

    //=====================================================================================
    //                                  COLISIONES 
    //=====================================================================================

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            animator.SetBool("Jump", false);
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

    //=====================================================================================


    //=====================================================================================
    //                                  TRIGGERS
    //=====================================================================================

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            animator.SetBool("Raikiri", false);
            animator.SetTrigger("Damage");
            OnDamage();
            timeAproach = 3;
            timeHit = 1;
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
        if (collision.CompareTag("SpecialHit"))
        {
            animator.SetBool("Raikiri", false);
            animator.SetBool("Run", false);
            animator.SetTrigger("Damage");
            OnDamage();
            timeAproach = 3;
            timeHit = 1;
            if (Naruto.GetComponent<NarutoMovement>().KnockBackHit)
            {
                animator.SetBool("KnockBack", true);
                timeAproach = 3;
            }
            health -= Naruto.GetComponent<NarutoMovement>().hitDamage;
        }
    }

    
}
