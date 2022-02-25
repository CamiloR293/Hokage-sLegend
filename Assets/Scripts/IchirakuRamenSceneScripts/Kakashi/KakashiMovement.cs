using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KakashiMovement : MonoBehaviour
{
    
    private float playerDistance;
    private float timer = 3;
    private float timeAproach = 3;
    private int opcion;
    private float jumpForce = 170;
    private bool combo;
    Vector3 direccion;


    [Header("Components")]
    private Rigidbody2D rigidbody2D;
    public Transform playerPosition;
    private Animator animator;


    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        playerDistance = (transform.position.x - playerPosition.position.x);
        if (playerDistance < 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
            direccion = new Vector3(1.0f, 0.0f, 0.0f);
        }
        else
        {
            direccion = new Vector3(-1.0f, 0.0f, 0.0f);
            transform.localScale = new Vector3(-1, 1, 1);
        }
        Debug.Log(playerDistance);
        Combo();

        timer = Timer();
        if(timer <= 0)
        {
            Acercarse();
            if(!animator.GetBool("Run")) ResetTimer();
        }




        
        
    }

    
    //=====================================================================================
    //                                  CRONOMETRO
    //=====================================================================================

    void ResetTimer()
    {
        timer = 3;
    }
    float Timer()
    {
        timer -= Time.deltaTime;
        return timer;
    }

    //=====================================================================================

    //=====================================================================================
    //                             FUNCIONES DE ENEMIGO
    //=====================================================================================

    void Jump()
    {
        animator.SetBool("Jump", true);
        rigidbody2D.AddForce(Vector2.up * jumpForce);
    }

    void Combo()
    {
        if (Mathf.Abs(playerDistance) < 0.6f )
        {
            animator.SetBool("Combo", true);
            combo = true;
        }
        else animator.SetBool("Combo", false);
    }
    void ComboUnlock()
    {
        animator.SetBool("Combo", false);
    }


    void Acercarse()
    {
        timeAproach -= Time.deltaTime;
        if (timeAproach >= 0)
        {
            animator.SetBool("Run", true);
            transform.Translate(direccion * 1f * Time.deltaTime,Space.World);
            if ((playerPosition.position.y - 1f) > transform.position.y)
            {
                if (Physics2D.Raycast(transform.position, Vector2.down, 0.28f))
                {
                    Jump();
                }
            }
        }
        else
        {
            animator.SetBool("Run", false);
            timeAproach = 3;
        }
    }



    //=====================================================================================

    //=====================================================================================
    //                                  COLISIONES 
    //=====================================================================================

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            animator.SetBool("Jump", false);
            animator.SetBool("Grounded", true);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            
            animator.SetBool("Grounded", false);
        }
    }

    //=====================================================================================
}
