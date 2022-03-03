using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerEnemyBallChain : MonoBehaviour
{
    public GameObject NinjaEnemyBallChain;
    public GameObject SoundNinja;
    public GameObject position1;
    public GameObject position2;
    public GameObject position3;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Vector3 positionSpawn1 = position1.transform.position;
        Vector3 positionSpawn2 = position2.transform.position;
        Vector3 positionSpawn3 = position3.transform.position;
        
        
        if(collision.CompareTag("PlayerHitBox"))
        {
            GameObject NinjaEnemy = Instantiate(NinjaEnemyBallChain, positionSpawn1, Quaternion.identity);
            NinjaEnemy.SetActive(true);
            GameObject SoundNinja = Instantiate(this.SoundNinja, positionSpawn2, Quaternion.identity);
            SoundNinja.SetActive(true);
            GameObject SoundNinja1 = Instantiate(this.SoundNinja, positionSpawn3, Quaternion.identity);
            SoundNinja1.SetActive(true);
            Destroy(gameObject);
        }
    }
}
