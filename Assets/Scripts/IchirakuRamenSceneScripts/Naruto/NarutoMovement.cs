using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class NarutoMovement : MonoBehaviour
{
    [Header("Components")]
    public PlayerHealthController HealthController = new PlayerHealthController(); 
    public GameObject ShurikenPrefab;
    public SpecialAttack SpecialAttack = new SpecialAttack();
    public NarutoMovement player;
    public GameObject RasenganCollider;
    public GameObject RasenganColliderAttack;
    
    private NarutoSoundController SoundController;
    private Rigidbody2D Rigidbody2D;
    private float Horizontal;
    private float timeRestart = 4;
    private bool Grounded;
    public Animator Animator;

    [Header("Movement")]
    public float SpeedRunning;
    public float SpeedWalking;
    public float JumpForce;
    public float hitDamage;
    private float hitTranslate;
    private bool JumpAgain;
    private bool isCrouch;
    private int Combo;
    private bool Attacking;
    public bool KnockBackHit;
    private float UpForce;
    private int direccion;
    private bool Moving;
    protected bool Flag;
    public bool Rasengan;
    public bool MoveRasengan = false;
    public float timeRasengan;
    int i = 0;

    [Header("Particles")]
    public GameObject spawnEffect;


    void Start()
    {
        
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
        HealthController = GetComponent<PlayerHealthController>();
        SpecialAttack = GetComponent<SpecialAttack>();
        player = GetComponent<NarutoMovement>();
        SoundController = GetComponent<NarutoSoundController>();
        direccion = 1;
        timeRasengan = 0.19f;
    }

    //Start Update
    void Update()
    {
        
        Health();
        if (HealthController.MinHealth > 0)
        {
            HealthController.Damage();
            if (!HealthController.Damage_)
            {
                ComboCount();
                Horizontal = Input.GetAxisRaw("Horizontal");
                if (!Animator.GetBool("Animating_Something"))
                {
                    if (Horizontal < 0.0f)
                    {
                        transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
                        direccion = -1;
                    }
                    else if (Horizontal > 0.0f)
                    {
                        transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                        direccion = 1;
                    }
                }

                if (!Animator.GetBool("Uppercut")) Animator.SetBool("Crouch", Input.GetKey(KeyCode.S));

                PunchTranslate();
                //===========================================================================================
                //                                      RASENGAN
                //===========================================================================================


                RasenganTranslate();

                if (Animator.GetBool("Crouch") && Input.GetKeyDown(KeyCode.L) && !Attacking && !Animator.GetBool("Jumping"))
                {
                    SoundController.Clone.Play();
                    timeRasengan = 0.19f;
                    hitDamage = 3;
                    Rasengan = true;
                    Attacking = true;
                }
                

                if (Rasengan) SpecialAttack.RasenganCharge(Animator, player);

                //===========================================================================================

                if (!Animator.GetBool("Animating_Something"))
                {
                    if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) && !isCrouch)
                    {
                        if (Grounded)
                        {
                            Jump();
                        }
                        else if (JumpAgain)
                        {
                            Jump();
                            JumpAgain = false;
                        }
                    }
                }

                if (Input.GetKeyDown(KeyCode.L) && !Animator.GetBool("Crouch") && !Animator.GetBool("Animating_Something"))
                {
                    Animator.SetTrigger("Throw");
                    Throw();
                }
            }
        }
        else
        {   
            Animator.SetBool("Death", true);
            timeRestart -= Time.deltaTime;
            if (i == 0)
            {
                i = 1;
                Animator.SetTrigger("KnockBack");
            }
            if(timeRestart <= 0)
            {
                PauseManager restartLevel = new PauseManager();
                restartLevel.Reset();
            }
        }
    }
    //End Update

    //Start FixedUpdate
    void FixedUpdate()
    {
        if (HealthController.MinHealth > 0)
        {
            if (!HealthController.Damage_)
            {
                //Run Function
                if (Animator.GetBool("Crouch") && !Animator.GetBool("Jumping")) Horizontal = 0; //When Naruto is crouch can't move

                if (Animator.GetBool("Animating_Something")) Horizontal = 0; //When Naruto is hitting can't move with the axis

                else
                {
                    Animator.SetBool("Walking", Horizontal != 0.0f);
                    Animator.SetBool("Running", Input.GetKey(KeyCode.LeftShift));
                }

                if (Input.GetKey(KeyCode.LeftShift))
                {
                    Rigidbody2D.velocity = new Vector2(Horizontal * SpeedRunning, Rigidbody2D.velocity.y);
                }
                else
                {
                    Rigidbody2D.velocity = new Vector2(Horizontal * SpeedWalking, Rigidbody2D.velocity.y);
                }

            }
        }
        
        //End Run Function
    }
    //End FixedUpdate

    ///=============================================
    ///     SECCION DE FUNCIONES DEL JUGADOR
    ///=============================================


    //==============================================
    //             COMBOS DE GOLPES
    //==============================================
    public void ComboCount()
    {
        
        if(Input.GetKeyDown(KeyCode.K) && !Attacking  && !Animator.GetBool("Jumping") && !Animator.GetBool("Flag") && !Animator.GetBool("Crouch"))
        {
            Attacking = true;
            Animator.SetTrigger(""+Combo);
            UpForce = 0.02f;
        }
        else if(Input.GetKeyDown(KeyCode.K) && !Attacking && Animator.GetBool("Jumping") && !Animator.GetBool("Flag") && !Animator.GetBool("Crouch"))
        {
            Attacking = true;
            Animator.SetTrigger("AirPunch"+Combo);
            UpForce = 0.02f;
        }
        else if (Animator.GetBool("Crouch") && Input.GetKeyDown(KeyCode.K) && !Attacking && !Animator.GetBool("Jumping"))
        {
            Attacking = true;
            Animator.SetBool("Uppercut", true);
            Animator.SetBool("Crouch", false);
            Uppercut();
        }

        
    }

    ///============================================================
    ///                 CONTADOR DE GOLPES
    ///============================================================


    public void Start_Combo()
    {
        Attacking = false;
        if(Combo < 3)
        {
            Combo++;
        }
    }

    public void Finish_Ani()
    {
        Attacking = false;
        Combo = 0;
        
    }


    ///============================================================
    ///                Bandera para siguiente golpe
    ///============================================================
    //Bandera para siguiente golpe
    //Si es verdadero, el siguiente golpe podr? animarse
    public void flag()
    {
        Animator.SetBool("Flag", true);
    }

    public void UnFlag()
    {
        Animator.SetBool("Flag", false);
    }
    //Fin bandera

   


    //Funcion salto
    private void Jump()
    {
        SoundController.Jump.Play();
        Rigidbody2D.AddForce(Vector2.up * JumpForce);
    }
    //Fin funcion salto


    ///=====================================================================
    ///                             GOLPES
    ///=====================================================================
    //Desplazamiento por cada golpe
    //hitTranslate es la un numero el cual se multiplica por el vector de movimiento,
    //Lo que hace es desplazar a Naruto en la direccion hacia la que apunta cuando lanza un golpe
    //Naruto por cada golpe deber? avanzar para no dejar ir a su enemigo
    //UpForce siendo la fuerza con la que Naruto envia a sus enemigos al aire
    public void AirPunch1()
    {
        SoundController.Hit1.Play();
        hitDamage = 3;
    }
    public void AirPunch2()
    {
        SoundController.Hit2.Play();
        hitDamage = 5;
    }
    public void Punch1()
    {
        SoundController.Hit1.Play();
        hitTranslate = 1.2f;
        Moving = true;
        hitDamage = 5;
    }
    public void Punch2()
    {
        SoundController.Hit2.Play();
        hitTranslate = 1f;
        Moving = true;
        hitDamage = 5;
    }
    public void Punch3()
    {
        SoundController.Hit3.Play();
        KnockBackHit = true;
        hitTranslate = 1.2f;
        Moving = true;
        hitDamage = 7;
    }

    public void Uppercut()
    {
        SoundController.Uppercut.Play();
        KnockBackHit = true;
        hitDamage = 10;
        hitTranslate = 4;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Moving = false;
        }
    }

    public void EndUppercut()
    {
        Animator.SetBool("Uppercut", false);
    }

    //Funcion que mueve al personaje cuando golpea
    public void PunchTranslate()
    {
       
        if (Moving)
        {
            Vector3 direccion = new Vector3(this.direccion, 0, 0);
            transform.Translate(direccion * hitTranslate * Time.deltaTime, Space.World);
        }
    }


    //Fin desplazamiento por golpe
    public void StartMoving()
    {
        Moving = true;
    }
    public void FinishMoving()
    {
        Moving = false;
    }
    //----------------------------------------------
    
    //Controla la barra verde de vida
    public void Health()
    {
        HealthController.Bar.fillAmount = HealthController.MinHealth / HealthController.MaxHealth;
    }
    //Fin control de vida
    
    


    //Bloqueo de movimiento
    public void LockAnimation()
    {
        Animator.SetBool("Animating_Something", true);
    }

    public void UnlockAnimation()
    {
        Animator.SetBool("Animating_Something", false);
        KnockBackHit =false;
    }
    //Fin bloqueo de movimineto


    //Aparicion de prefab de lanzamiento
    private void Throw()
    {
        hitDamage = 1;
        SoundController.Throw.Play();
        Vector3 direccion;
        if (transform.localScale.x == 1.0f) direccion = Vector3.right;
        else direccion = Vector3.left;
        GameObject Shuriken = Instantiate(ShurikenPrefab, transform.position + direccion * 0.2f, Quaternion.identity);
        Shuriken.GetComponent<ShurikenScript>().SetDirection(direccion);
    }
    //Fin lanzamiento



    //Dennota si toca el suelo o una superficie con etiqueta "ground"
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            Grounded = false;
            Animator.SetBool("Jumping", true);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.tag == "Ground")
        {
            if (Physics2D.Raycast(transform.position, Vector3.down, 0.28f))
            {
                Grounded = true;
                JumpAgain = true;
                Animator.SetBool("Jumping", false);
            }
        }
    }
    //Fin de colisiones

    

    //Los muertos no se mueven, tonces dejamos inmovil a Naruto
    public void DeathTranslate()
    {
        Rigidbody2D.velocity = new Vector2(0, 0);
    }




    //=====================================================================================
    //                                      RASENGAN
    //=====================================================================================

    public void MovingRasengan()
    {
        SoundController.Rasengan3.Play();
        MoveRasengan = true;
    }
    public void NoMoveRasengan()
    {
        MoveRasengan = false;
        Animator.SetFloat("Time", 0.19f);
    }
    public void RasenganTranslate()
    {
        if (MoveRasengan)
        {
            this.Animator.SetFloat("Time", Timer());
            Vector3 direccion = new Vector3(this.direccion, 0, 0);
            transform.Translate(direccion * 10 * Time.deltaTime, Space.World);
        }
    }
    
    public void UnHitRasengan()
    {
        Animator.SetBool("HitRasengan", false);
    }

    public void RasenganHit()
    {
        SoundController.Rasengan1.Play();
        KnockBackHit = true;
        Moving = true;
        hitTranslate = 15;
        UpForce = 0.2f;
    }

    public void SpawnEffect()
    {
        GameObject Spawn;
        Vector3 spawnPos;
        if (direccion > 0)
        {
            spawnPos = new Vector3(transform.position.x - 0.33f, transform.position.y, transform.position.z);
        }
        else
        {
            spawnPos = new Vector3(transform.position.x + 0.33f, transform.position.y, transform.position.z);
        }
        Spawn = Instantiate(spawnEffect, spawnPos, Quaternion.identity );
        Spawn.SetActive(true);
    }

    public float Timer()
    {
        this.timeRasengan -= Time.deltaTime;
        
        return timeRasengan;
    }
    //===============================================================

}
