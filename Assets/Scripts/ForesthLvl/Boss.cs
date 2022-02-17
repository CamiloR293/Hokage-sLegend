using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
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

    [SerializeField] private float AttackDamage;

    void Start()
    {

       healthHUD = GetComponent<HealthHUD>();
       animator = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
      
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

    }


    private void Update()
    {
        float Distance = Vector2.Distance(transform.position, Player.position);
        animator.SetFloat("Distance", Distance);
        if(Distance < 4 )
        {
        HealtH.SetActive(true);
        }
        else {
            HealtH.SetActive(false);
        }
       
    }
    // Recibir da�o Boss
    public void Damage(float damage)
    {
        healthHUD.Health -= damage;
    
        if (healthHUD.Health <= 0)

        {
            DeadSound.Play();
            animator.SetTrigger("Dead");
             Dead();

        }
    }
   
    // Fin recibir da�o

    // Muerte Boss
    private void Dead()
    {
       
        Destroy(gameObject);
        
    }
    // Fin muerte Boss

    public void SearchPlayer()
    {
        if ((Player.position.x < transform.position.x && !Rigth)
            || (Player.position.x > transform.position.x && Rigth))
        {
            Rigth = !Rigth;
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 180, 0);
        }
    }

    public void Attack()
    {
        Collider2D[] Objects = Physics2D.OverlapCircleAll(ControllerAttack.position, RangeAttack);
        foreach (Collider2D colision in Objects)
        {
            if (colision.CompareTag("Player") )
            {
                //DAÑO AL JUGADOR
                colision.GetComponent<KnockBackHit>(); ;
                HitSound.Play();
                //.Damage(AttackDamage);
            }
        }


    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(ControllerAttack.position, RangeAttack);
    }

}