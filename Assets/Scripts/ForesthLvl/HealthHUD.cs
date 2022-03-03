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


// Se cambiara la barra de vida a medida que el enemigo pierda vida
    void Update()
    {
        HealtHUD.fillAmount = Health / MaxHealth;
        
    }

   


   

}