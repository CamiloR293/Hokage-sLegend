using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerEnemyBallChain : MonoBehaviour
{
    public GameObject NinjaEnemyBallChainPrefab;
    private void Start()
    {
        NinjaEnemyBallChainPrefab = GameObject.Find("NinjaEnemyBallChain");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Vector3 position = new Vector3(-1.609f, -2.287f, 0);
        if(collision.CompareTag("PlayerHitBox"))
        {
            GameObject NinjaEnemy = Instantiate(NinjaEnemyBallChainPrefab, position, Quaternion.identity);
        }
    }
}
