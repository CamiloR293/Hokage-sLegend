using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damager : MonoBehaviour
{
    // Start is called before the first frame update
    public NarutoMovement Naruto = new NarutoMovement();
    [SerializeField] private float Health;
    public GameObject Naruto1;
    public Transform Player;

   

    public NinjaEnemy Ninja = new NinjaEnemy();
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

// En este script es para que el enemigo reciba daño al ser golpeado por el player, este primer metodo funciona para los puños
     public void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {
            Health -= Player.GetComponent<NarutoMovement>().hitDamage;
            Ninja.GetComponent<NinjaEnemy>().Dead(Health);
        }
    }
// Y este con la habilidad 
    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("SpecialHit"))
        {
            Health -= Player.GetComponent<NarutoMovement>().hitDamage;
            Ninja.GetComponent<NinjaEnemy>().Dead(Health);
            
        }
    }



    

    
    

}
