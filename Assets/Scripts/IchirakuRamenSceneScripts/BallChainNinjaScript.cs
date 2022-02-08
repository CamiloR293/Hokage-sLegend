using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallChainNinjaScript : MonoBehaviour
{


    public float rangeOfVision;
    public float rangeOfAttack;

    public Transform player;

    private Rigidbody2D Rigidbody2D;
    private Animator Animator;
    private float playerDistance;
    private bool Locked;


    void Start()
    {
        Animator = GetComponent<Animator>();
        Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    //Start Update
    void Update()
    {
        
        playerDistance = Vector2.Distance(player.position, Rigidbody2D.position);

        
        Animator.SetBool("PreparingAttack", playerDistance <= rangeOfVision);
        Animator.SetBool("Attack", playerDistance <= rangeOfAttack);

        if (!Locked)
        {
            if (player.position.x < Rigidbody2D.position.x) transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            else transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        }
    }
    //End Update


    public void Lock()
    {
        Locked = true;
    }
    public void Unlock()
    {
        Locked = false;
    }

    //Debugger code
    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.blue;
    //    Gizmos.DrawWireSphere(transform.position, rangeOfVision);
    //    Gizmos.DrawWireSphere(transform.position, rangeOfAttack);
    //}
    
}
