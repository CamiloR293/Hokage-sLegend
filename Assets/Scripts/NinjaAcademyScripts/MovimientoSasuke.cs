using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoSasuke : MonoBehaviour
{
    private Rigidbody2D Rigidbody2D;
    public GameObject BolaFuegoPrefab;
    public GameObject player;
    
    private float lastShoot;

    //private float Horizontal = 1f;
    //public float JumpForce;
    public float Speed;
    //private bool attacking;
    public float distancia;
    private Animator Animator;

    public Vector2 direccion;
    public Transform Player;

    //private bool Grounded;
    private float cont = 1f;

    public Transform sensor;

    [SerializeField] public float Life;
    [SerializeField] public float maxLife =100;
    //[SerializeField] private HealthBar healthB;
    [SerializeField]private GameObject HealtHUD;

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
    public GameObject Defeat;
    public GameObject YouWin;
    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();

        Life = maxLife;
        //healthB.LifeInit(Life);
        sensor.parent = null;
    }


    // Update is called once per frame
    void Update()
    {

        float NarutoLife = player.GetComponent<PlayerHealthController>().MinHealth;

        float distance = Mathf.Abs(player.transform.position.x - transform.position.x);
        if (Life > 0 && NarutoLife > 0)
        {
            if (distance < 5.5) HealtHUD.SetActive(true);
            else HealtHUD.SetActive(false);

            if (distance < 5 && distance > 0.1) {


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
            if (distance <= 0.359)
            {
                if (Time.time > lastShoot + Cooldown && cont == 1f)
                {
                    //Sound, Ready
                    Animator.SetTrigger("Golpe4");
                    Animator.SetBool("Run", false);
                    transform.Translate(direccion * 0f * Time.deltaTime, Space.World);
                    lastShoot = Time.time;
                    cont = 2f;
                }

                if (Time.time > lastShoot + Cooldown && cont == 4f)
                {
                    Instantiate(Hit1);
                    Animator.SetTrigger("Golpe1");
                    Animator.SetBool("Run", false);
                    transform.Translate(direccion * 0f * Time.deltaTime, Space.World);
                    lastShoot = Time.time;
                    cont = 5f;
                }
                if (Time.time > lastShoot + Cooldown && cont == 0f)
                {
                    Instantiate(UpperKick);
                    Animator.SetTrigger("Golpe2");
                    Animator.SetBool("Run", false);
                    transform.Translate(direccion * 0f * Time.deltaTime, Space.World);
                    lastShoot = Time.time;
                    cont = 1f;
                }
                if (Time.time > lastShoot + Cooldown && cont == 2f)
                {

                    Instantiate(Hit1);
                    Animator.SetTrigger("Golpe3");
                    Animator.SetBool("Run", false);
                    transform.Translate(direccion * 0f * Time.deltaTime, Space.World);
                    lastShoot = Time.time;
                    cont = 3f;
                }
                if (Time.time > lastShoot + Cooldown && cont == 3f)
                {
                    Instantiate(BolaFuego);
                    Animator.SetTrigger("BolaF");
                    Animator.SetBool("Run", false);
                    transform.Translate(direccion * 0f * Time.deltaTime, Space.World);
                    Shoot();
                    lastShoot = Time.time;
                    cont = 4f;
                }
                if (Time.time > lastShoot + Cooldown && cont == 5f)
                {
                    Instantiate(Chidori);
                    Animator.SetTrigger("Chidori");
                    Animator.SetBool("Run", false);
                    transform.Translate(direccion * 0f * Time.deltaTime, Space.World);
                    lastShoot = Time.time;
                    cont = 0f;



                }
            }
        }

        if (NarutoLife <= 0 && Life > 0) Animator.SetBool("Win", true);
        
        if (Animator.GetCurrentAnimatorStateInfo(0).IsName("SasukeDeath") && Life <= 0)
        {

            Animator.SetBool("Die", true);
            Animator.SetBool("Run", false);
            TimeDestroy -= Time.deltaTime;
            if (TimeDestroy <= 0) Destroy(gameObject);

        }
    }
    public void InitialLife(float life)
    {
    //    healthB.ChangeActLife(life);
    }
    public void YOUWIN()
    {
        Instantiate(YouWin);

    }
    public void DEFEAT()
    {
        Instantiate(Defeat);
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
            if (!Animator.GetCurrentAnimatorStateInfo(0).IsName("SasukeDeath")
                && !Animator.GetCurrentAnimatorStateInfo(0).IsName("Chidori")
                && !Animator.GetCurrentAnimatorStateInfo(0).IsName("Chidori2"))
            {
                AudioGetHit();
                Life -= player.GetComponent<NarutoMovement>().hitDamage;
        //        healthB.ChangeActLife(Life);
                Animator.SetTrigger("GetGolpe");
            }
            if (Life <= 0)
            {
                //Sound, Ready
                Animator.SetBool("Die", true);

            }
        }


    }

    private int randomN;
    public void AudioGetHit()
    {
        randomN = Random.Range(1, 3);
        if (randomN == 1)
        {
            Instantiate(GetHit);
            Debug.Log(("Golpe"));

        }
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("SpecialHit"))
        {
            if (!Animator.GetCurrentAnimatorStateInfo(0).IsName("SasukeDeath"))
            {
                //Animar daño y caida
                AudioGetSpecialHit();
                Animator.SetTrigger("GetSpecialHit");
                Life -= Player.GetComponent<NarutoMovement>().hitDamage;
                //healthB.ChangeActLife(Life);
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
    private void AudioGetSpecialHit()
    {
        randomN = Random.Range(1, 8);
        if (randomN == 1)
        {
            Debug.Log("*****************Recibe Rasengan***********");
            Instantiate(GetSpecialHit);
        }
    }
}

