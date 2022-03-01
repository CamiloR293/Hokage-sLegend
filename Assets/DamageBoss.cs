using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageBoss : MonoBehaviour
{
    // Start is called before the first frame update

      public NarutoMovement Naruto = new NarutoMovement();
    [SerializeField] private float Health;
    public GameObject Naruto1;
    public Transform Player;

   

    public Boss boss= new Boss();

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

 public void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {
            Health -= Player.GetComponent<NarutoMovement>().hitDamage;
            boss.GetComponent<Boss>().Damage(Health);
        }
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("SpecialHit"))
        {
            Health -= Player.GetComponent<NarutoMovement>().hitDamage;
            boss.GetComponent<Boss>().Damage(Health);
            
        }
    }
}
