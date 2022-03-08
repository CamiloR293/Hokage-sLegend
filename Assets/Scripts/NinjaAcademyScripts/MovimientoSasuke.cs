using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoSasuke : MonoBehaviour
{
    private Rigidbody2D Rigidbody2D;
    public GameObject BolaFuegoPrefab;
    public GameObject player;    
    private float lastShoot;
    public float Speed;    
    public float distancia;
    private Animator Animator;
    private Vector2 direccion;
    public Transform Player;    
    private float cont = 1f;
    public Transform sensor;
    [SerializeField] public float Life;
    [SerializeField] public float maxLife;    
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
    private int randomN;
    private int i = 0;
    private int j = 0;
    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
        Life = maxLife;        
        sensor.parent = null;
    }
    void Update()
    {
        float NarutoLife = player.GetComponent<PlayerHealthController>().MinHealth;
        float distance = Mathf.Abs(player.transform.position.x - transform.position.x);

        if (Life > 0 && NarutoLife > 0)
        {
            //Activar/Desactivar Barra de vida
            if (distance < 5.5) HealtHUD.SetActive(true);
            else HealtHUD.SetActive(false);
            //Acercarse al Jugador
            if (distance < 5.5 && distance > 0.15) 
            {
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
            //Mirar al Jugador
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
            //Variacion de Ataques
            if (distance <= 0.359 && !Animator.GetCurrentAnimatorStateInfo(0).IsName("Winner"))
            {
                if (Time.time > lastShoot + Cooldown && cont == 1f)
                {
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
        //Animacion Jefe Gana
        if (NarutoLife <= 0 && Life > 0)
        {
            Animator.SetBool("Win", true);
            Animator.SetBool("Run", false);
        }
            //Acciones Player gana
        if (Animator.GetCurrentAnimatorStateInfo(0).IsName("SasukeDeath") && Life <= 0)
        {
            Animator.SetBool("Die", true);
            Animator.SetBool("Run", false);
            TimeDestroy -= Time.deltaTime;
            if (TimeDestroy <= 0) Destroy(gameObject);
        }
    }
    //public void InitialLife(float life)
    //{        
    //}
    //Sonidos
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
    }
    public void ChidoriHitSound()
    {
        Instantiate(ChidoriHit);
    }
    public void Helicopter()
    {
        Instantiate(Hit1);
    }
    public void SoundDie()
    {
        if (i % 3 == 0 || i == 0)
        {
            Instantiate(Death);
            i++;
        }
    }
    //Metdo Bola de fuego
    private void Shoot()
    {        
        Vector3 direction;
        if (transform.localScale.x == 1.0f) direction = Vector3.left;
        else direction = Vector3.right;
       GameObject Fuego = Instantiate(BolaFuegoPrefab, transform.position + direction * 0.2f, Quaternion.identity);
        Fuego.GetComponent<ShurikenScript>().SetDirection(direction);
    }
    //Metodos Recibir daño. 
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
                Animator.SetTrigger("GetGolpe");
            }
            if (Life <= 0)
            {
                Animator.SetBool("Die", true);
                SoundDie();
            }
        }
    }    
    public void AudioGetHit()
    {
        randomN = Random.Range(1, 3);
        if (randomN == 1)
        {
            Instantiate(GetHit);
        }
    }
    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("SpecialHit"))
        {
            if (!Animator.GetCurrentAnimatorStateInfo(0).IsName("SasukeDeath"))
            {
                SettingsGetSpecialHit();
                Life -= Player.GetComponent<NarutoMovement>().hitDamage;
                if (transform.position.x > collision.transform.position.x)
                {
                    transform.Translate(Vector3.right * 10 * Time.deltaTime,Space.World);
                }
                else
                {
                    transform.Translate(Vector3.right * -10 * Time.deltaTime, Space.World);
                }
                if (Life <= 0)
                {
                    Animator.SetBool("Die", true);
                    SoundDie();
                }
            }
        }           
    }
    
    private void SettingsGetSpecialHit()
    {
        randomN = Random.Range(1, 8);
        if (randomN == 1)
        {
            Instantiate(GetSpecialHit);
        }
        
        if (j==4 )
        {
            Debug.Log("animacion Get special hit");
            Animator.SetTrigger("GetSpecialHit");
            j=0;
        }
        else
        {
            j++;
        }
    }
}

