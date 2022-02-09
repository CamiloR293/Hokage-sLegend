using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed;
    public Transform leftPoint, rightPoint;
    public bool movingright;
    public bool Atack;

    public float moveTime, waitTime;
    private float moveCount, waitCount;

    public string tagPlayer = "GolpePlayer";
    public float Life = 9f;

    public GameObject player;
    public float distancia;

    private Collider2D Collider2D;
    private Rigidbody2D theRB;
    public SpriteRenderer theSR;
    private Animator Anim;


    private float lastShoot;
    ////-0.51,  4.17 limits x---- modo guardia
    //static Vector2 LimitsX = new Vector2(-0.51f, 4.17f);

    //enum States{patrol, pursuit}
    //[SerializeField]
    //States state = States.patrol;
    //[SerializeField]
    //float SearchRange = 1;

    //[SerializeField]
    //float stoppingDistance = 0.3f;

    //Transform player;
    //Vector3 target;
    // Start is called before the first frame update
    void Start()
    {

        //Busqueda player
        //player = GameObject.FindGameObjectWithTag("Principal").transform;
        //------------------------------
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

        if (distance < distancia && Time.time > lastShoot + 3.12f && !Anim.GetCurrentAnimatorStateInfo(0).IsName("Fat_Death"))
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

    //Golpe
    //public void OnTriggerEnter2D(Collider ColGolpe)
    //{
    //    if (ColGolpe.tag.Equals("Principal")) Anim.SetTrigger("Hit");
    //}
    //Recibe daño
    public void OnTriggerEnter2D(Collider2D ColDaño)
    {
        if (ColDaño.tag.Equals(tagPlayer))
        {
            if (!Anim.GetCurrentAnimatorStateInfo(0).IsName("Fat_Death"))
            {
                Life -= 3;
                Anim.SetTrigger("Hurt");
                theRB.AddForce(Vector2.right *
                    (GetComponentInParent<Transform>().localScale.x * -1) * 5, ForceMode2D.Impulse);
                if (Life < 0)
                {
                    Anim.SetBool("Die", true);
                    moveSpeed = 0;
                }
            }
            
        } 
    }
}
