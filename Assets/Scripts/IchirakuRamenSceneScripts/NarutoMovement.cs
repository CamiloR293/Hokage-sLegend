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
    private bool JumpAgain;
    private bool isCrouch;
    private int Combo;
    public bool Attacking;
    

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
                    if (Horizontal < 0.0f) transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
                    else if (Horizontal > 0.0f) transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                }
                Animator.SetBool("Crouch", Input.GetKey(KeyCode.S));

                if (Physics2D.Raycast(transform.position, Vector3.down, 0.28f))
                {
                    Grounded = true;
                    JumpAgain = true;
                }
                else Grounded = false;

                if (!Animator.GetBool("Animating_Something"))
                {
                    if (Input.GetKeyDown(KeyCode.W) && !isCrouch)
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

                Animator.SetBool("Jumping", !Grounded);
            }
        }
        else
        {
            switch (HealthController.Death)
            {
                case 0:
                    Animator.SetTrigger("Death");
                    HealthController.Death++;
                    break;
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
                if (Animator.GetBool("Crouch")) Horizontal = 0; //When Naruto is crouch can't move

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
    /// SECCION DE FUNCIONES DEL JUGADOR
    ///---------------------------------------------


    //----------------------------------------------
    //             COMBOS DE GOLPES
    //----------------------------------------------
    public void ComboCount()
    {
        
        if(Input.GetKeyDown(KeyCode.K) && !Attacking)
        {
            
            Attacking = true;
            Animator.SetTrigger(""+Combo);
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
    //----------------------------------------------
    //----------------------------------------------
    //----------------------------------------------

    public void Health()
    {
        HealthController.Bar.fillAmount = HealthController.MinHealth / HealthController.MaxHealth;
    }


    //Bloqueo de movimiento
    public void LockAnimation()
    {
        Animator.SetBool("Animating_Something", true);

    }

    public void UnlockAnimation()
    {
        Animator.SetBool("Animating_Something", false);
    }
    //Fin bloqueo de movimineto
    private void Throw()
    {
        Animator.SetBool("Throwing", Input.GetKeyDown(KeyCode.F));
        Vector3 direccion;
        if (transform.localScale.x == 1.0f) direccion = Vector3.right;
        else direccion = Vector3.left;

        GameObject Shuriken = Instantiate(ShurikenPrefab, transform.position + direccion * 0.2f, Quaternion.identity);
        Shuriken.GetComponent<ShurikenScript>().SetDirection(direccion);

    }



    private void Jump()
    {
        Rigidbody2D.AddForce(Vector2.up * JumpForce);
    }
    
}
