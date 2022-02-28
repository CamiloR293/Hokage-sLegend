using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoSasuke : MonoBehaviour
{
    private Rigidbody2D Rigidbody2D;
    public GameObject BolaFuegoPrefab;
    private float lastShoot;

    private float Horizontal = 1f;
    public float JumpForce;
    public float Speed;
    //private bool attacking;
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
    public float TimeAtack;

    [Header("Sounds")]
    public GameObject Chidori;
    public GameObject ChidoriHit;
    public GameObject GetSpecialHit;
    public GameObject Hit1;
    public GameObject UpperKick;
    public GameObject BolaFuego;
    public GameObject Death;
    public GameObject GetHit;
    public GameObject Stamce;
    public GameObject Stamce1;
    public GameObject StamceEnd;
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
            if (distance < 5 && distance > 0.355) {

                TimeAtack -= Time.deltaTime;
                if (TimeAtack <= 0
                    && !Animator.GetCurrentAnimatorStateInfo(0).IsName("Sasuke_Golpe")
                    && !Animator.GetCurrentAnimatorStateInfo(0).IsName("y down")
                    && !Animator.GetCurrentAnimatorStateInfo(0).IsName("SasukeBolaFuego")
                    && !Animator.GetCurrentAnimatorStateInfo(0).IsName("SasukeDeath")
                    && !Animator.GetCurrentAnimatorStateInfo(0).IsName("Sasuke_Golpe")
                    && !Animator.GetCurrentAnimatorStateInfo(0).IsName("GetSpecialAttack")
                    && !Animator.GetCurrentAnimatorStateInfo(0).IsName("Xattack")
                    && !Animator.GetCurrentAnimatorStateInfo(0).IsName("Y + jump")
                    && !Animator.GetCurrentAnimatorStateInfo(0).IsName("FastGet")
                    && !Animator.GetCurrentAnimatorStateInfo(0).IsName("Chidori"))
                {
                    if (Animator.GetCurrentAnimatorStateInfo(0).IsName("Chidori2"))
                    {

                        transform.Translate(direccion * 5.6f * Time.deltaTime, Space.World);
                    }
                    else
                    {
                        Animator.SetBool("Run", true);
                        transform.Translate(direccion * 1.6f * Time.deltaTime, Space.World);
                    }
                }

            }

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
            if (distance <= 0.4)
            {
                Animator.SetBool("Run", false);
                transform.Translate(direccion * 0f * Time.deltaTime, Space.World);
                if (Time.time > lastShoot + Cooldown && cont == 1f)
                {
                    //Sound, Ready
                    Animator.SetTrigger("Golpe4");
                    lastShoot = Time.time;
                    cont = 2f;
                }

                if (Time.time > lastShoot + Cooldown && cont == 4f)
                {
                    Instantiate(Hit1);
                    Animator.SetTrigger("Golpe1");
                    lastShoot = Time.time;
                    cont = 5f;
                }
                if (Time.time > lastShoot + Cooldown && cont == 0f)
                {
                    Instantiate(UpperKick);
                    Animator.SetTrigger("Golpe2");
                    lastShoot = Time.time;
                    cont = 1f;
                }
                if (Time.time > lastShoot + Cooldown && cont == 2f)
                {

                    Instantiate(Hit1);
                    Animator.SetTrigger("Golpe3");
                    lastShoot = Time.time;
                    cont = 3f;
                }
                if (Time.time > lastShoot + Cooldown && cont == 3f)
                {
                    Instantiate(BolaFuego);
                    Animator.SetTrigger("BolaF");
                    Shoot();
                    lastShoot = Time.time;
                    cont = 4f;
                }
                if (Time.time > lastShoot + Cooldown && cont == 5f)
                {
                    Instantiate(Chidori);
                    Animator.SetTrigger("Chidori");
                    lastShoot = Time.time;
                    cont = 0f;



                }
            }
        }


        if (Animator.GetCurrentAnimatorStateInfo(0).IsName("SasukeDeath") && Life <= 0)
        {
            TimeDestroy -= Time.deltaTime;
            if (TimeDestroy <= 0) Destroy(gameObject);
        }
    }
    
    public void Die()
    {
        Instantiate(Death);
    }
    public void ChidoriHitSound()
    {
        Instantiate(ChidoriHit);
    }
    public void Helicopter()
    {
        Instantiate(Hit1);
    }
    
    private void Shoot()
    {
        
        Vector3 direction;
        if (transform.localScale.x == 1.0f) direction = Vector3.left;
        else direction = Vector3.right;

       GameObject Fuego = Instantiate(BolaFuegoPrefab, transform.position + direction *0.2f, Quaternion.identity);
        Fuego.GetComponent<ShurikenScript>().SetDirection(direction);

    }
    
    
    //Recibir daño. (Cuando guardia)
    private void OnTriggerEnter2D(Collider2D collision)
    {
       
        if (collision.CompareTag("Player"))
        {
            if (!Animator.GetCurrentAnimatorStateInfo(0).IsName("SasukeDeath"))
            {
                Instantiate(GetHit);
                Debug.Log(("Golpe"));
                Life -= player.GetComponent<NarutoMovement>().hitDamage;
                Animator.SetTrigger("GetGolpe");
            }
            if(Life <= 0)
            {
                //Sound, Ready
                Animator.SetBool("Die", true);

            }
        }

    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("SpecialHit"))
        {
            if (!Animator.GetCurrentAnimatorStateInfo(0).IsName("SasukeDeath"))
            {
                //Animar daño y caida
                Animator.SetTrigger("GetSpecialHit");
                Life -= Player.GetComponent<NarutoMovement>().hitDamage;
                Debug.Log("*****************Recibe Rasengan***********");
                Instantiate(GetSpecialHit);
                if (transform.position.x > collision.transform.position.x)
                {
                    transform.Translate(Vector3.right * 10 * Time.deltaTime,Space.World);
                }
                else
                {
                    transform.Translate(Vector3.right * -10 * Time.deltaTime, Space.World);
                }
                
            }
        }

           
    }
}

