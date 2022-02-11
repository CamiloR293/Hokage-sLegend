using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hability : MonoBehaviour
{
    

    [SerializeField] private Vector2 BoxDimesion;

    private float lastShoot;

    [SerializeField] private float AttackDamage;

    [SerializeField] private Transform Position;

    [SerializeField] private float Timev;

    void Start()
    {
       Destroy(gameObject, Timev); 
    }

    // Update is called once per frame

    public void Attack()
    {
        
        
        Collider2D[] Objects = Physics2D.OverlapBoxAll(Position.position, BoxDimesion, 0f);
        foreach (Collider2D colision in Objects)
        {
            if (colision.CompareTag("Player") && Time.time > lastShoot + 5f)
            {
                colision.GetComponent<KnockBackHit>();
                //.DealDamage(AttackDamage);
                lastShoot = Time.time;
            }
        }


    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireCube(Position.position, BoxDimesion);
    }
}
