using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public NarutoMovement Naruto = new NarutoMovement();

     public GameObject Naruto1;
     public HealthHUD healthHUD;

     public GameObject HealtH;

    [SerializeField] private AudioSource HitSound;

    [SerializeField] public AudioSource DeadSound;

    private Animator animator;

    public Rigidbody2D body;

    public Transform Player;

    private bool Rigth = true;

   

 
    [Header("Attack")]

    [SerializeField] private Transform ControllerAttack;

    [SerializeField] private float RangeAttack;

   

    void Start()
    {

       healthHUD = GetComponent<HealthHUD>();
       animator = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
      
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

    }


    private void Update()
    {
       // Mide la distancia en la cual se encuentra el jugador y entra en accion 
       //cuando se encuentre cerca
       float Distance = Vector2.Distance(transform.position, Player.position);
        animator.SetFloat("Distance", Distance);
        // Activa del HUD de la barra de vida
        if(Distance < 4 )
        {
        HealtH.SetActive(true);
        }
        else {
            HealtH.SetActive(false);
        }
              
              
      
    }
    // Recibir daño Boss
    public void Damage(float damage)
    {
        healthHUD.Health = damage;
        DeadSound.Play();
        animator.SetTrigger("Dead");
        if (healthHUD.Health <= 0)

        {
            DeadSound.Play();
            animator.SetTrigger("Dead");
            Destroy(gameObject);
            Destroy(healthHUD);
            HealtH.SetActive(false);

        }
    }
   
    // Fin recibir daño

  

    public void SearchPlayer()
    {
         // Esta funcion hace que el enemigo mire a la posicion que se encuentra el jugador 
        if ((Player.position.x < transform.position.x && !Rigth)
            || (Player.position.x > transform.position.x && Rigth))
        {
            Rigth = !Rigth;
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 180, 0);
        }
    }

    public void Attack()
    {
       // Hacer daño al jugador
        Collider2D[] Objects = Physics2D.OverlapCircleAll(ControllerAttack.position, RangeAttack);
        foreach (Collider2D colision in Objects)
        {
            HitSound.Play();
            if (colision.CompareTag("Player") )
            {
                //DAÑO AL JUGADOR
                
            if (Naruto1.GetComponent<NarutoMovement>().KnockBackHit)
            {
             animator.SetBool("KnockBack", true);
             HitSound.Play();
            }
            else
            {
               animator.SetBool("KnockBack", false);
               HitSound.Play();
            }
             
                
            }
        }


    }

// Dibuja el rango de ataque y vision del enemigo
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(ControllerAttack.position, RangeAttack);
    }

}