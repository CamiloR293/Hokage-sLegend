using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hability : MonoBehaviour
{
  private Rigidbody2D Rigidbody2D;
[SerializeField] private Transform Position;
[SerializeField] private Vector2 BoxDimesion;
    public float time = 0.5f;
    public float Speed;
    private Vector2 Direction;


    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>(); 
        Destroy(gameObject,time);      
    }

    
    private void FixedUpdate()
    {
        Rigidbody2D.velocity = Direction * Speed;
    }

    public void SetDirection(Vector2 direction)
    {
        Direction = direction; 
    }

 public void Attack()
    {
        
        
        Collider2D[] Objects = Physics2D.OverlapBoxAll(Position.position, BoxDimesion, 0f);
        foreach (Collider2D colision in Objects)
        {
            if (colision.CompareTag("Player") )
            {
                colision.GetComponent<KnockBackHit>();
                //.DealDamage(AttackDamage);
                
            }
        }


    }
 
}


 



/*  public float speed = 2;

public float time = 0.5f;

public bool left;

[SerializeField] private Vector2 BoxDimesion;



private void Start()
{
    Destroy(gameObject,time);


}

private void Update()
{
    if(left)
    {
           transform.Translate(Vector2.left*speed*Time.deltaTime);
    }   
          
    else{
             transform.Translate(Vector2.right*speed*Time.deltaTime);
      }



}





*/
//-------------------------------------------------------------------------------------------------


