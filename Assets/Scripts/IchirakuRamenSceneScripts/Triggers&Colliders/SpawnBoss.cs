using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBoss : MonoBehaviour
{
    public GameObject Kakashi;
    public Transform position;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Vector3 positionS = position.transform.position;
        if (collision.CompareTag("PlayerHitBox"))
        {
            GameObject KakashiS = Instantiate(Kakashi, positionS, Quaternion.identity);
            KakashiS.SetActive(true);
            Destroy(gameObject);
        }
    }
}
