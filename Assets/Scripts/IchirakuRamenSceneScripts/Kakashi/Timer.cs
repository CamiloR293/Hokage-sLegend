using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    float Cronometro;
    public void Update()
    {
        Returner();
    }

    float Returner()
    {
        Cronometro += Time.deltaTime;
        return Cronometro;
    }
}
