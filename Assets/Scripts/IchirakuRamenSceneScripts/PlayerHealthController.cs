using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public bool Damage_;
    public float MaxHealth = 100f;
    public float MinHealth = 1f;
    public Image Bar;
    public int Death = -1;
    public float KnockBack;

    void Start()
    {
        
        
    }


    void Update()
    {

    }

    public void Damage()
    {
        if (Damage_)
        {
            transform.Translate(Vector3.right * KnockBack * Time.deltaTime, Space.World);   
        }
    }

    public void FinishDamage()
    {
        Damage_ = false;
    }
}
