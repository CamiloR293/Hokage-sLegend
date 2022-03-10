using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeTime : MonoBehaviour
{
    public float LT;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, LT);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
