using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hability : MonoBehaviour
{
    

    [SerializeField] private Vector2 BoxDimesion;

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
            if (colision.CompareTag("Player"))
            {
                colision.GetComponent<Player>().Damage(AttackDamage);
                //.DealDamage(AttackDamage);
            }
        }


    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireCube(Position.position, BoxDimesion);
    }
}
