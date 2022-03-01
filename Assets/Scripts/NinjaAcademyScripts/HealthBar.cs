using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Slider slider;
    private void Start()
    {
        slider = GetComponent<Slider>();
    }
    public void ChangeMaxLife(float MaxLife)
    {
        slider.maxValue = MaxLife;
    }
    public void ChangeActLife(float CantLife)
    {
        slider.value = CantLife;
    }
    public void LifeInit(float CantLife)
    {
        ChangeMaxLife(CantLife);
        ChangeActLife(CantLife);
    }
}
