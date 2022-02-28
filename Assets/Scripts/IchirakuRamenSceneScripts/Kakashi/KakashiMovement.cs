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
    private int opcion;
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
        if (!Naruto.GetComponent<NarutoMovement>().Animator.GetBool("Death"))
        {
            playerDistance = (transform.position.x - playerPosition.position.x);
            if (!animator.GetBool("Combo"))
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
                Raikiri();
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
        if (Mathf.Abs(playerDistance) < 0.6f)
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

    void JumpToPlatform()
    {

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
            if (Naruto.GetComponent<NarutoMovement>().KnockBackHit)
            {
                animator.SetBool("KnockBack", true);
            }
            else animator.SetBool("KnockBack", false);

            health -= Naruto.GetComponent<NarutoMovement>().hitDamage;

        }
    }

    


}
