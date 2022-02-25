using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShurikenScrip : MonoBehaviour
{
    private Rigidbody2D Rigidbody2D;
    private Vector2 DIrection;


    public float Speed;
    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        Rigidbody2D.velocity = DIrection * Speed;
    }
    public void SetDirection(Vector2 direction)
    {
        DIrection = direction;
    }
    public void DestroyShuriken()
    {
        Destroy(gameObject);
    }
}
