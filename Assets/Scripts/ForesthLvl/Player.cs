using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour

    
{
    [SerializeField] private float Health ;

    [SerializeField] private Transform Control ;

    [SerializeField] private float Range;

    [SerializeField] private float Damager;
    // Start is called before the first frame update
  

    // Update is called once per frame
    void Update()
    {
       if(Input.GetButtonDown("Fire1"))
       {
              Attack();
       } 
    }

 public void Attack()
    {
        Collider2D[] Objects = Physics2D.OverlapCircleAll(Control.position, Range);
        foreach (Collider2D colision in Objects)
        {
            if (colision.CompareTag("Enemy"))
            {
                colision.GetComponent<NinjaEnemy>().Damage(Damager); ;
             
                //.Damage(AttackDamage);

            }
        }


    }
    public void Damage(float damage)
    {
        Health -= damage;
        //  HealthHUD.ChangeHealt(Health);
        if (Health <= 0)

        {
           Destroy(gameObject);
        }
    }

       private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(Control.position, Range);
    }
}
