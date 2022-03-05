using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarLife1 : MonoBehaviour
{
    public Image greenBar;

    public GameObject sasuke;

    public float actualLife;
    
    public float MaxLife;

    // Update is called once per frame
    void Update()
    {
        actualLife = sasuke.GetComponent<MovimientoSasuke>().Life;        
        MaxLife = sasuke.GetComponent<MovimientoSasuke>().maxLife;
        greenBar.fillAmount = actualLife / MaxLife;
        
    }
}
