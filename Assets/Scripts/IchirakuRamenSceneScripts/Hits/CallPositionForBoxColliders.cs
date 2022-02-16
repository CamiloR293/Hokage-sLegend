using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallPositionForBoxColliders : MonoBehaviour
{

    public GameObject Target;
    //public Rigidbody2D Rigidbody;
    void Start()
    {
        //Rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        transform.position = Target.transform.position;
    }
}
