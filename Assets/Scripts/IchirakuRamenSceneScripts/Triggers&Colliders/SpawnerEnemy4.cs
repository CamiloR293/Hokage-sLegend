using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerEnemy4 : MonoBehaviour
{
    public GameObject FatEnemy;
    public GameObject position1;
    public GameObject position2;
    public GameObject position3;
    public GameObject position4;
    public GameObject position5;
    public GameObject position6;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Vector3 positionSpawn1 = position1.transform.position;
        Vector3 positionSpawn2 = position2.transform.position;
        Vector3 positionSpawn3 = position3.transform.position;
        Vector3 positionSpawn4 = position4.transform.position;
        Vector3 positionSpawn5 = position5.transform.position;
        Vector3 positionSpawn6 = position6.transform.position;

        if (collision.CompareTag("PlayerHitBox"))
        {
            GameObject NinjaEnemy = Instantiate(FatEnemy, positionSpawn1, Quaternion.identity);
            NinjaEnemy.SetActive(true);
            GameObject SoundNinja = Instantiate(FatEnemy, positionSpawn2, Quaternion.identity);
            SoundNinja.SetActive(true);
            GameObject SoundNinja1 = Instantiate(FatEnemy, positionSpawn3, Quaternion.identity);
            SoundNinja1.SetActive(true);
            GameObject NinjaEnemy1 = Instantiate(FatEnemy, positionSpawn6, Quaternion.identity);
            NinjaEnemy1.SetActive(true);
            GameObject NinjaEnemy2 = Instantiate(FatEnemy, positionSpawn5, Quaternion.identity);
            NinjaEnemy2.SetActive(true);
            GameObject SoundNinja3 = Instantiate(FatEnemy, positionSpawn4, Quaternion.identity);
            SoundNinja3.SetActive(true);


            Destroy(gameObject);
        }
    }
}
