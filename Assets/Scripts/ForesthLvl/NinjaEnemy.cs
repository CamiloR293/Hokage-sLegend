
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class NinjaEnemy : MonoBehaviour
{

    
    public NarutoMovement Naruto = new NarutoMovement();
    private Animator animator;

    public Rigidbody2D body;

    public AudioSource Clip;

    public AudioSource Clip1;

    public Transform Player;

    public Transform pos;

    private bool SeeRigth = true;

    public GameObject Collectible;

    public GameObject Naruto1;

    [SerializeField] private float Health;

    [Header("Attack")]

    [SerializeField] private Transform ControllerAttack;

    [SerializeField] private float RangeAttack;

    [SerializeField] private float AttackDamage;

    void Start()
    {

       
        animator = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

    }


    private void Update()
    {
       // Medir distancia a la que se encuentra el jugador
        float Distance = Vector2.Distance(transform.position, Player.position);
        animator.SetFloat("Distance", Distance);
    }
    // Recibir daño enemigo
 
    // Fin recibir da�o
    


  


    // Muerte enemigo
   public void Dead(float HealthUdapte)
    {
       // Al morir deja una pocion 
       if (HealthUdapte<= 0)

        {
            Clip1.Play();
            animator.SetTrigger("Die Animation Ninja");
        }
        Instantiate(Collectible, pos.transform.position, pos.transform.rotation);
        Destroy(gameObject);
        
    }
    // Fin muerte enemgo

    public void SearchPlayer()
    {

    
         // Esta funcion hace que el enemigo mire a la posicion que se encuentra el jugador 
        if ((Player.position.x < transform.position.x && !SeeRigth)
            || (Player.position.x > transform.position.x && SeeRigth))
        {
            SeeRigth = !SeeRigth;
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 180, 0);

        } 
    }

   public void Attack()
    {
        // Hacer daño al jugador
        Collider2D[] Objects = Physics2D.OverlapCircleAll(ControllerAttack.position, RangeAttack);
        foreach (Collider2D colision in Objects)
        {
          if (colision.CompareTag("Player"))
        {
           
            Clip.Play();
            if (Naruto1.GetComponent<NarutoMovement>().KnockBackHit)
            {
             animator.SetBool("KnockBack", true);
            }
            else
            {
               animator.SetBool("KnockBack", false);
            }
            

            
        }
        

        }
    }

  





  

}
