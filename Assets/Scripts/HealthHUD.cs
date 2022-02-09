using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthHUD : MonoBehaviour
{
    private Boss gara;
    public float Health;

    public float MaxHealth;

    public Image HealtHUD;

    void Update()
    {
        HealtHUD.fillAmount = Health / MaxHealth;
        
    }

    //sirve


   

}