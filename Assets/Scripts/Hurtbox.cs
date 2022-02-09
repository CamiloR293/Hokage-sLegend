using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hurtbox : MonoBehaviour
{
    public string tagEnemigo = "Enemy";
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag.Equals(tagEnemigo))
        {
            Debug.Log(("Hit"));
        }
    }
}
