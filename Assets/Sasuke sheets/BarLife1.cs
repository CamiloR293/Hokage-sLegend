using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarLife1 : MonoBehaviour
{
    public Image greenBar;

    public GameObject sasuke;

    private float actualLife;
    
    private float MaxLife;

    // Update is called once per frame
    void Update()
    {
        
        MaxLife = sasuke.GetComponent<MovimientoSasuke>().maxLife;
        greenBar.fillAmount = actualLife / MaxLife;
        
    }
}
