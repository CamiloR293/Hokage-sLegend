using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public bool Damage_;
    public float MaxHealth = 100f;
    public float MinHealth;
    public Image Bar;
    public int Death = -1;
    public float KnockBack;
    public NarutoMovement player;

    private void Start()
    {
        player = GetComponent<NarutoMovement>();
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
        
        transform.rotation = Quaternion.Euler(0, 0, 0);
        Damage_ = false;
    }
}
