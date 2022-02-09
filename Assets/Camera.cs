using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{

    public Transform target;

    public Transform farBackground, middleBackground;

    private float lastxPosition;

    

    private void Start()
    {
        lastxPosition = transform.position.x; 
    }

    void Update()
    {
        transform.position = new Vector3(target.position.x, transform.position.y, transform.position.z);

        if (target.position.x < 0f ) transform.position = new Vector3(0f, -2.5f, transform.position.z);

        if (target.position.y > -2.5 && target.position.x > 0f) transform.position = new Vector3(target.position.x, target.position.y, transform.position.z); ;


        float amountToMoveX = transform.position.x - lastxPosition;

        farBackground.position = farBackground.position + new Vector3(amountToMoveX * -0.002f, 0f, 0f);
        middleBackground.position += new Vector3(amountToMoveX * 0.6f,0f, 0f);

        lastxPosition = transform.position.x;
        
    }
}
