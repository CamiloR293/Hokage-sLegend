using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoSasuke : MonoBehaviour
{
    private Rigidbody2D Rigidbody2D;

    public GameObject shurikenPrefab;

    private float lastShoot;

    private float Horizontal;
    public float JumpForce;
    public float Speed;

    private Animator Animator;


    private bool Grounded;


    public string tagPlayer = "GolpeEnemigo";

    public float Life = 9f;
    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();

        Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {


        //golpes

        //GOLPE 1
        if (Input.GetKeyDown(KeyCode.Z)) Animator.SetTrigger("Golpe");


        //Guardia
        Animator.SetBool("Guard", Input.GetKey(KeyCode.X));
        if (Input.GetKey(KeyCode.X))
        {
            Horizontal = 0f;
            Rigidbody2D.gravityScale = 13f;
        }
        else
        {
            Rigidbody2D.gravityScale = 1f;
        }
            //shuriken
        bool flag = false;
        if (Input.GetKey(KeyCode.Space) && Time.time > lastShoot + 0.6f)
        {

            Shoot();
            lastShoot = Time.time;
            flag = true;

        }
        Animator.SetBool("lanzar", flag != false && Input.GetKey(KeyCode.Space));
        //animaciones
       if(!Animator.GetCurrentAnimatorStateInfo(0).IsName("Sasuke_Golpe")
            && !Animator.GetCurrentAnimatorStateInfo(0).IsName("Guard")
            && !Animator.GetCurrentAnimatorStateInfo(0).IsName("SasukeHurt"))
        {
            //movimiento
            Horizontal = Input.GetAxisRaw("Horizontal");

            if (Horizontal < 0.0f)
            {
                transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);

            }
            else if (Horizontal > 0.0f)
            {
                transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            }
            Animator.SetBool("running", Horizontal != 0.0f);
            
            //Salto
            Debug.DrawRay(transform.position, Vector3.down * 0.30f, Color.red);
            if (Physics2D.Raycast(transform.position, Vector3.down, 0.30f))
            {
                Grounded = true;
            }
            else Grounded = false;
            if (Input.GetKeyDown(KeyCode.W) && Grounded)
            {
                Jump();
            }
        }
        else
        {
            Rigidbody2D.velocity = Vector3.zero;
        }
    }


    private void Shoot()
    {
        
        Vector3 direction;
        if (transform.localScale.x == 1.0f) direction = Vector3.right;
        else direction = Vector3.left;

       GameObject Shuriken = Instantiate(shurikenPrefab, transform.position + direction *0.3f, Quaternion.identity);
        Shuriken.GetComponent<ShurikenScrip>().SetDirection(direction);
        
    }
    private void Jump()
    {
        Rigidbody2D.AddForce(Vector2.up * JumpForce);
    }
    private void FixedUpdate()
    {
        Rigidbody2D.velocity = new Vector2(Horizontal, Rigidbody2D.velocity.y);
    }
    public void OnTriggerEnter2D(Collider2D ColDaño)
    {
        if (ColDaño.tag.Equals(tagPlayer))
        {
           

            if (Life <= 0)
            {
                Animator.SetBool("Die", true);
                Rigidbody2D.velocity = Vector3.zero;
                Horizontal = 0.0f;
            }
            else 
            {
                Debug.Log(("Golpe -3"));
                Life -= 3;
                Animator.SetTrigger("GetGolpe");
                Rigidbody2D.AddForce(Vector2.right *
                    (GetComponentInParent<Transform>().localScale.x * -1) * 5, ForceMode2D.Impulse);
            }
            

        }
    }
}
