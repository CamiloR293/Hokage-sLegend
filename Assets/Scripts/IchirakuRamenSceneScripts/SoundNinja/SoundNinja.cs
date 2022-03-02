using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundNinja : MonoBehaviour
{
    
    public float rangeOfVision = 1.7f;
    private float timeAproach = 3;
    private float timeRun = 3;
    private float timeAttack = 0;
    Vector3 direccion;

    public GameObject Naruto;
    private Rigidbody2D Rigidbody2D;
    private Animator animator;
    private float playerDistance;



    void Start()
    {
        animator = GetComponent<Animator>();
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Naruto = GameObject.Find("Naruto");
    }

    void Update()
    {
        playerDistance = Naruto.GetComponent<Transform>().position.x - transform.position.x;
        if (playerDistance < 0)
        {
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            direccion = new Vector3(1.0f, 0.0f, 0.0f);
        }
        else
        {
            transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
            direccion = new Vector3(-1.0f, 0.0f, 0.0f);
        }
        
        if((Mathf.Abs(playerDistance) > 0.5) && (Mathf.Abs(playerDistance) <= rangeOfVision) && !animator.GetBool("Combo"))
        {
        Acercarse();
        }
        
        if (Mathf.Abs(playerDistance) <= 0.5)
        {
            timeAttack -= Time.deltaTime;
            if (timeAttack < 0) animator.SetBool("Combo", true);
            
        }
        else
        {
            animator.SetBool("Combo", false);
        }
    }



    //=====================================================================================
    //                             FUNCIONES DE ENEMIGO
    //=====================================================================================
    void Acercarse()
    {
        
        timeAproach -= Time.deltaTime;
        if (timeAproach >= 0)
        {
            animator.SetBool("Run", true);
            transform.Translate(-direccion * 1f * Time.deltaTime, Space.World);
            if (Mathf.Abs(playerDistance) < 0.6) timeAproach = 0;
        }
        else
        {
            animator.SetBool("Run", false);
            timeAproach = 3;
        }
    }

    //=====================================================================================
    //                                  CRONOMETROS
    //=====================================================================================

    void ResetTimerAttack()
    {
        timeAttack = 1;
        animator.SetBool("Combo", false);
    }

    void ResetTimerRun()
    {
        timeRun = 3;
    }
    void TimerRun()
    {
        timeRun -= Time.deltaTime;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, rangeOfVision);

    }
}
