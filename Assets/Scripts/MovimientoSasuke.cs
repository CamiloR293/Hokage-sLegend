using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoSasuke : MonoBehaviour
{
    private Rigidbody2D Rigidbody2D;
    public GameObject shurikenPrefab;

    private float lastShoot;

    private float Horizontal= 1f;
    public float JumpForce;
    public float Speed;
    private bool attacking;
    public float distancia;
    private Animator Animator;

    public Vector2 direccion;
    public Transform Player;

    //private bool Grounded;
    private float cont = 1f;

    public GameObject player;

    public float Life;
    public float Cooldown;
    public float TimeDestroy;
    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
        
    }

  
    // Update is called once per frame
    void Update()
    {

        float distance = Mathf.Abs(player.transform.position.x - transform.position.x);
        if (Life > 0)
        {
            if (Horizontal != 0f)
            {
                if (Player.position.x < Rigidbody2D.position.x)
                {
                    transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                    this.direccion.x = -1;
                }
                else
                {
                    transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
                    this.direccion.x = 1;
                }
                if (distance <= 0.8F)
                {
                    if (Time.time > lastShoot + Cooldown && cont == 1f)
                    {
                        Animator.SetTrigger("Golpe4");
                        lastShoot = Time.time;
                        cont = 4f;
                    }

                    if (Time.time > lastShoot + Cooldown && cont == 4f)
                    {
                        Animator.SetTrigger("Golpe1");
                        lastShoot = Time.time;
                        cont = 2f;
                    }
                    if (Time.time > lastShoot + Cooldown && cont == 0f)
                    {
                        Animator.SetTrigger("Golpe2");
                        lastShoot = Time.time;
                        cont = 1f;
                    }
                    if (Time.time > lastShoot + Cooldown && cont == 2f)
                    {
                        Animator.SetTrigger("Golpe3");
                        lastShoot = Time.time;
                        cont = 3f;
                    }
                    if (Time.time > lastShoot + Cooldown && cont == 3f)
                    {
                        Animator.SetTrigger("BolaF");
                        lastShoot = Time.time;
                        cont = 0f;
                    }

                }
            }
        }


        if (Animator.GetCurrentAnimatorStateInfo(0).IsName("SasukeDeath")&& Life <=0)
        {
            TimeDestroy -= Time.deltaTime;
            if (TimeDestroy <= 0) Destroy(gameObject);
        }


    
        //        //shuriken
        //    bool flag = false;
        //    if (Input.GetKey(KeyCode.Space) && Time.time > lastShoot + 0.6f)
        //    {

        //        Shoot();
        //        lastShoot = Time.time;
        //        flag = true;

        //    }
        //    Animator.SetBool("lanzar", flag != false && Input.GetKey(KeyCode.Space));
        //    //animaciones
        //   if(!Animator.GetCurrentAnimatorStateInfo(0).IsName("Sasuke_Golpe")
        //        && !Animator.GetCurrentAnimatorStateInfo(0).IsName("Guard")
        //        && !Animator.GetCurrentAnimatorStateInfo(0).IsName("SasukeHurt")
        //        && !Animator.GetCurrentAnimatorStateInfo(0).IsName("SasukeDeath"))
        //    {
        //        //movimiento
        //        Horizontal = Input.GetAxisRaw("Horizontal");

        //        if (Horizontal < 0.0f)
        //        {
        //            transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);

        //        }
        //        else if (Horizontal > 0.0f)
        //        {
        //            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        //        }
        //        Animator.SetBool("running", Horizontal != 0.0f);

        //        //Salto
        //Debug.DrawRay(transform.position, Vector3.down * 0.30f, Color.red);
        //if (Physics2D.Raycast(transform.position, Vector3.down, 0.30f))
        //{
        //    Grounded = true;
        //}
        //else Grounded = false;
        //        if (Input.GetKeyDown(KeyCode.W) && Grounded)
        //        {
        //            Jump();
        //        }
        //    }
        //    else
        //    {
        //        Rigidbody2D.velocity = Vector3.zero;
        //    }
    }


    //private void Shoot()
    //{

    //    Vector3 direction;
    //    if (transform.localScale.x == 1.0f) direction = Vector3.right;
    //    else direction = Vector3.left;

    //   GameObject Shuriken = Instantiate(shurikenPrefab, transform.position + direction *0.3f, Quaternion.identity);
    //    Shuriken.GetComponent<ShurikenScrip>().SetDirection(direction);

    //}
    //private void Jump()
    //{
    //    Rigidbody2D.AddForce(Vector2.up * JumpForce);
    //}
    //private void FixedUpdate()
    //{
    //    Rigidbody2D.velocity = new Vector2(Horizontal, Rigidbody2D.velocity.y);
    //}

    public void Attacking()
    {
        attacking = true;
    }
    public void NoAttacking()
    {
        attacking = false;
    }

    //Recibir daño. (Cuando guardia)
    private void OnTriggerEnter2D(Collider2D collision)
    {
       
        if (collision.CompareTag("Player") && !attacking)
        {
            if (!Animator.GetCurrentAnimatorStateInfo(0).IsName("SasukeDeath"))
            {
                Debug.Log(("Golpe"));
                Life -= player.GetComponent<NarutoMovement>().hitDamage;
                Animator.SetTrigger("GetGolpe");
            }
            if(Life <= 0)
            {
                Animator.SetBool("Die", true);

            }


        }




            //    if (ColDaño.tag.Equals(tagPlayer))
            //{
            //    Debug.Log(("Golpe -3"));
            //    Life -= 3;
            //    Animator.SetTrigger("GetGolpe");
            //    Rigidbody2D.AddForce(Vector2.right *
            //        (GetComponentInParent<Transform>().localScale.x * -1) * 5, ForceMode2D.Impulse);

            //    if (Life <= 0)
            //    {
            //        Animator.SetBool("Die", true);
            //        Rigidbody2D.velocity = Vector3.zero;
            //        Horizontal = 0.0f;
            //    }
            //else 
            //{
            //    Debug.Log(("Golpe -3"));
            //    Life -= 3;
            //    Animator.SetTrigger("GetGolpe");
            //    Rigidbody2D.AddForce(Vector2.right *
            //        (GetComponentInParent<Transform>().localScale.x * -1) * 5, ForceMode2D.Impulse);
            //}


        
    }
}
