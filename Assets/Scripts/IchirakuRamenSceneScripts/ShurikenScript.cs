using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShurikenScript : MonoBehaviour
{
    private Rigidbody2D Rigidbody2D;

    public float Speed;
    private Vector2 Direction;


    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();        
    }

    
    private void FixedUpdate()
    {
        Rigidbody2D.velocity = Direction * Speed;
    }

    public void SetDirection(Vector2 direction)
    {
        Direction = direction; 
    }

    public void DestroyShuriken()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            DestroyShuriken();
        }
    }
}
