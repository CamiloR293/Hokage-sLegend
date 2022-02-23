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

    public float distancia;
    private Animator Animator;

    public Vector2 direccion;
    public Transform Player;

    private bool Grounded;
    private float cont = 1f;

    public string tagPlayer = "GolpePlayer";
    public GameObject player;

    public float Life = 9f;

    public float TimeDestroy = 0.1f;
    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();

        Animator = GetComponent<Animator>();
    }

  
    // Update is called once per frame
    void Update()
    {

        float distance = Mathf.Abs(player.transform.position.x - transform.position.x);

        Debug.DrawRay(transform.position, Vector3.down * 0.30f, Color.red);
        if (Physics2D.Raycast(transform.position, Vector3.down, 0.30f))
        {
            Grounded = true;
        }
        else Grounded = false;

        //if (!Animator.GetCurrentAnimatorStateInfo(0).IsName("Fat_Death")&& Horizontal !=0f)
        //{
        //    if(distance < 0.5f && Time.time > lastShoot + 1.7f)
        //    {
        //        Animator.SetTrigger("Golpe1");
        //        lastShoot = Time.time;
        //    }
        //    if(distance > 0.4f && distance < 0.6 && Time.time > lastShoot + 1.5f)
        //    {
        //        Animator.SetTrigger("Golpe2");
        //        lastShoot = Time.time;
        //    }
        //}
        
        if ( Horizontal != 0f)
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
            if (distance <= 0.8F )
            {
                
                if (Time.time > lastShoot+1.5f && cont == 1f)
                {
                    Animator.SetTrigger("Golpe1");
                    lastShoot = Time.time;
                    cont = 2f;
                }
                if ( Time.time > lastShoot + 1.5f && cont ==0f)
                {
                    Animator.SetTrigger("Golpe2");
                    lastShoot = Time.time;
                    cont = 1f;
                }
                if (Time.time > lastShoot + 1.5f && cont == 2f)
                {
                    Animator.SetTrigger("Golpe3");
                    lastShoot = Time.time;
                    cont = 3f;
                }
                if (Time.time > lastShoot + 1.5f && cont == 3f)
                {
                    Animator.SetTrigger("BolaF");
                    lastShoot = Time.time;
                    cont = 0f;
                }

            }
        }
        if (Animator.GetCurrentAnimatorStateInfo(0).IsName("SasukeDeath")&& Life <=0)
        {
            TimeDestroy -= Time.deltaTime;
            if (TimeDestroy <= 0) Destroy(gameObject);
        }


        //    //golpes

        //    //GOLPE 1
        //    if (Input.GetKeyDown(KeyCode.Z)) Animator.SetTrigger("Golpe");


        //    //Guardia
        //    Animator.SetBool("Guard", Input.GetKey(KeyCode.X));
        //    if (Input.GetKey(KeyCode.X))
        //    {
        //        Horizontal = 0f;
        //        Rigidbody2D.gravityScale = 13f;
        //    }
        //    else
        //    {
        //        Rigidbody2D.gravityScale = 1f;
        //    }
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


    //Recibir daño. (Cuando guardia)
    public void OnTriggerEnter2D(Collider2D ColDaño)
    {
        if (!Animator.GetCurrentAnimatorStateInfo(0).IsName("SasukeDeath"))
        {
            if (ColDaño.tag.Equals(tagPlayer))
            {
                Debug.Log(("Golpe -3"));
                Life -= 3;
                Animator.SetTrigger("GetGolpe");

                if (Life <= 0)
                {
                    Animator.SetBool("Die", true);
                    Rigidbody2D.velocity = Vector3.zero;
                    Horizontal = 0.0f;
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
}
