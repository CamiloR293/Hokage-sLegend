
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class NinjaEnemy : MonoBehaviour
{

    // SIRVE PARA TODOS LOS ENEMIGOS
    private Animator animator;

    public Rigidbody2D body;

    public AudioSource Clip;

    public AudioSource Clip1;

    public Transform Player;

    public Transform pos;

    private bool SeeRigth = true;

    public GameObject Collectible;

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
        float Distance = Vector2.Distance(transform.position, Player.position);
        animator.SetFloat("Distance", Distance);
    }
    // Recibir daño enemigo
    public void Damage(float damage)
    {
        Health -= damage;
   
        if (Health <= 0)

        {
            Clip1.Play();
            animator.SetTrigger("Die Animation Ninja");
            Dead();
        }
    }
    // Fin recibir da�o

    // Muerte Boss
    private void Dead()
    {
       
        Instantiate(Collectible, pos.transform.position, pos.transform.rotation);
        Destroy(gameObject);
        
    }
    // Fin muerte Boss

    public void SearchPlayer()
    {

    

        if ((Player.position.x < transform.position.x && !SeeRigth)
            || (Player.position.x > transform.position.x && SeeRigth))
        {
            SeeRigth = !SeeRigth;
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 180, 0);

        } 
    }

    public void Attack()
    {
        Collider2D[] Objects = Physics2D.OverlapCircleAll(ControllerAttack.position, RangeAttack);
        foreach (Collider2D colision in Objects)
        {
            if (colision.CompareTag("Player"))
            {
                colision.GetComponent<KnockBackHit>(); ;
                Clip.Play();
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
//SIRVE