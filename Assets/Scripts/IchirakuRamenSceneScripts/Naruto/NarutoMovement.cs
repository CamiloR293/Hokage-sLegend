using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class NarutoMovement : MonoBehaviour
{
    public PlayerHealthController HealthController = new PlayerHealthController(); 
    public GameObject ShurikenPrefab;

    [Header("Movement")]
    public float SpeedRunning;
    public float SpeedWalking;
    public float JumpForce;
    public float hitDamage;
    public float hitTranslate;
    private bool JumpAgain;
    private bool isCrouch;
    private int Combo;
    public bool Attacking;
    public bool KnockBackHit;
    public int direccion;
    public bool Moving;
    protected bool Flag;

    [Header("Components")]
    private Rigidbody2D Rigidbody2D; 
    private float Horizontal;       
    private bool Grounded;         
    public Animator Animator;
    

    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
        HealthController = GetComponent<PlayerHealthController>();
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
                Animator.SetBool("Crouch", Input.GetKey(KeyCode.S));

                if (!Animator.GetBool("Animating_Something"))
                {
                    if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) && !isCrouch)
                    {
                        if (Grounded)
                        {
                            Jump();
                        }
                        else
                        {
                            if (JumpAgain)
                            {
                                Jump();
                                JumpAgain = false;
                            }
                        }
                    }
                }

                if (Input.GetKeyDown(KeyCode.F))
                {
                    Throw();
                }

            }
        }
        else
        {
            Animator.SetBool("Death", true);
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

    ///---------------------------------------------
    ///     SECCION DE FUNCIONES DEL JUGADOR
    ///---------------------------------------------


    //----------------------------------------------
    //             COMBOS DE GOLPES
    //----------------------------------------------
    public void ComboCount()
    {
        
        if(Input.GetKeyDown(KeyCode.K) && !Attacking && !Animator.GetBool("Jumping") && !Animator.GetBool("Flag"))
        {
            Attacking = true;
            Animator.SetTrigger(""+Combo);
        }

        if(Input.GetKeyDown(KeyCode.K) && !Attacking && Animator.GetBool("Jumping") && Animator.GetBool("Flag"))
        {
            Attacking = true;
            Animator.SetTrigger("AirPunch" + Combo);
        }
    }

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

    //Bandera para siguiente golpe
        //Si es verdadero, el siguiente golpe podrá animarse
    public void flag()
    {
        Animator.SetBool("Flag", true);
    }

    public void UnFlag()
    {
        Animator.SetBool("Flag", false);
    }
    //Fin bandera

    //----------------------------------------------
    //----------------------------------------------
    //----------------------------------------------


    //Funcion salto
    private void Jump()
    {
        Rigidbody2D.AddForce(Vector2.up * JumpForce);
    }
    //Fin funcion salto

    //Desplazamiento por cada golpe
        //Naruto por cada golpe deberá avanzar para no dejar ir a su enemigo
    public void Punch1()
    {
        hitTranslate = 1.5f;
        Moving = true;
        PunchTranslate();
    }
    public void Punch2()
    {
        hitTranslate = 1.5f;
        Moving = true;
        PunchTranslate();
    }
    public void Punch3()
    {
        KnockBackHit = true;
        Moving = true;
        hitTranslate = 4;
        PunchTranslate();
    }

    public void PunchTranslate()
    {
        if (Moving)
        {
            Vector3 direccion = new Vector3(this.direccion, 0, 0);
            transform.Translate(direccion * hitTranslate * Time.deltaTime, Space.World);
        }
    }

    

    //Fin desplazamiento por golpe


    public void FinishMoving()
    {
        Moving = false;
    }
    //----------------------------------------------
    //----------------------------------------------
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
        Animator.SetBool("Throwing", Input.GetKeyDown(KeyCode.F));
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

    

}
