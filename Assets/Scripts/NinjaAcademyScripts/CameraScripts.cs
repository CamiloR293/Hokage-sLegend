using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScripts : MonoBehaviour
{
    public Transform target;

    //public Transform farBackground, middleBackground;

    //private float lastxPosition;

    //public GameObject Sasuke;

    private void Start()
    {
       //    lastxPosition = transform.position.x;
    }

    void Update()
    {
        transform.position = new Vector3(target.position.x, target.position.y, target.position.z);
        /*
        Vector3 position = transform.position;
        position.x = Sasuke.transform.position.x;
        position.y = Sasuke.transform.position.y + 0.65f;
        
        if (position.y <-2.5f) position.y = -2.5f;

        transform.position = position;

        float amountToMoveX = transform.position.x - lastxPosition;

        farBackground.po ition = farBackground.position + new Vector3(amountToMoveX, 0f, 0f);
        middleBackground.position += new Vector3(amountToMoveX * 0.1f, 0f, 0f);

        lastxPosition = transform.position.x;
        */
    }
}
