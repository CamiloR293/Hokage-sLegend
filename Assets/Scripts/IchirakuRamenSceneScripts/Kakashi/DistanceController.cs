using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceController : MonoBehaviour
{
    
    public Transform PlayerPosition;
    protected float PlayerDistance;

    void Start()
    {
        PlayerPosition = GetComponent<Transform>();
    }

    
    public float DistanceUpdate()
    {
        
        PlayerDistance = Mathf.Abs(transform.position.x - PlayerPosition.position.x);
        return PlayerDistance;
    }
}
