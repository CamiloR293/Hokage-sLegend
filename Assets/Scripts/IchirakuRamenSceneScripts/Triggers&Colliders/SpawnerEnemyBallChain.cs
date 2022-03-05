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
    public GameObject SpawnEffect;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Vector3 positionSpawn1 = position1.transform.position;
        Vector3 positionSpawn2 = position2.transform.position;
        Vector3 positionSpawn3 = position3.transform.position;
        
        
        if(collision.CompareTag("PlayerHitBox"))
        {
            GameObject NinjaEnemy = Instantiate(NinjaEnemyBallChain, positionSpawn1, Quaternion.identity);
            NinjaEnemy.SetActive(true);
            GameObject Spawn1 = Instantiate(SpawnEffect, positionSpawn1, Quaternion.identity);
            Spawn1.SetActive(true);
            GameObject SoundNinja = Instantiate(this.SoundNinja, positionSpawn2, Quaternion.identity);
            SoundNinja.SetActive(true);
            GameObject Spawn2 = Instantiate(SpawnEffect, positionSpawn2, Quaternion.identity);
            Spawn2.SetActive(true);
            GameObject SoundNinja1 = Instantiate(this.SoundNinja, positionSpawn3, Quaternion.identity);
            SoundNinja1.SetActive(true);
            GameObject Spawn3 = Instantiate(SpawnEffect, positionSpawn3, Quaternion.identity);
            Spawn3.SetActive(true);

            Destroy(gameObject);
        }
    }
}
