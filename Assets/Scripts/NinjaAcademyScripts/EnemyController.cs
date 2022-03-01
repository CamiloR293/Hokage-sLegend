using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed;
    public Transform leftPoint, rightPoint;
    public bool movingright;
    private bool Atack;

    public float moveTime, waitTime;
    private float moveCount, waitCount;

    public float Life;

    public GameObject player;
    public float distancia;

    private Collider2D Collider2D;
    private Rigidbody2D theRB;
    public SpriteRenderer theSR;
    private Animator Anim;


    private float lastShoot;
    // Start is called before the first frame update
    void Start()
    {

        theRB = GetComponent<Rigidbody2D>();
        Anim = GetComponent<Animator>();

        leftPoint.parent = null;
        rightPoint.parent = null;

        movingright = true;

        moveCount = moveTime;
    }


    // Update is called once per frame
    void Update()
    {

        float distance = Mathf.Abs(player.transform.position.x - transform.position.x);

        //Hit
        if (distance < distancia && Time.time > lastShoot + 2.77f && !Anim.GetCurrentAnimatorStateInfo(0).IsName("Fat_Death"))
        {            
            Anim.SetTrigger("Hit");
            lastShoot = Time.time;
        }
        if (!Anim.GetCurrentAnimatorStateInfo(0).IsName("Fat_hit") && !Anim.GetCurrentAnimatorStateInfo(0).IsName("Fat_Death"))
        {
            //Movimiento y pausas
            if (moveCount > 0)
            {
                moveCount -= Time.deltaTime;



                if (movingright)
                {
                    theRB.velocity = new Vector2(moveSpeed, theRB.velocity.y);

                    theSR.flipX = true;

                    if (transform.position.x > rightPoint.position.x)
                    {
                        movingright = false;
                    }
                }
                else
                {
                    theRB.velocity = new Vector2(-moveSpeed, theRB.velocity.y);

                    theSR.flipX = false;

                    if (transform.position.x < leftPoint.position.x)
                    {
                        movingright = true;
                    }
                }

                if (moveCount <= 0)
                {
                    waitCount = Random.Range(waitTime * 0.75f, waitTime * 1.25f);

                }

                Anim.SetBool("IsMoving", true);
            }
            else if (waitCount > 0)
            {
                waitCount -= Time.deltaTime;
                theRB.velocity = new Vector2(0f, theRB.velocity.y);

                if (waitCount <= 0)
                {
                    moveCount = Random.Range(moveTime * 0.75f, waitTime * 1.25f);
                }
                Anim.SetBool("IsMoving", false);
            }
        }
        else
        {
            theRB.velocity = Vector3.zero;
        }
    }
    public void Destroy()
    {
        Destroy(gameObject);
    }
    //Recibir da�o
    private void OnTriggerEnter2D(Collider2D ColDa�o)
    {
        if (ColDa�o.CompareTag("Player"))
        {
            if (!Anim.GetCurrentAnimatorStateInfo(0).IsName("Fat_Death"))
            {

                Life -= player.GetComponent<NarutoMovement>().hitDamage;
                Anim.SetTrigger("Hurt");;
                if (Life < 0)
                {
                    Anim.SetBool("Die", true);
                    moveSpeed = 0;
                }
            }
            
        } 
    }
}
