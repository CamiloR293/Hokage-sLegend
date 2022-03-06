using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform Naruto;
    public Transform middleBackground, farBackground;
    public float minHeight, maxHeight;
    public bool boss = false;
    


    private Vector2 lastPos;

    private void Start()
    {
        lastPos = transform.position;
    }

    void Update()
    {
        
        if(!boss) FollowPlayer();
        
        
    }

    void FollowPlayer()
    {
        transform.position = new Vector3(Naruto.position.x + 1f, Mathf.Clamp(Naruto.position.y, minHeight, maxHeight), transform.position.z);
        Vector2 amountMove = new Vector2(transform.position.x - lastPos.x, transform.position.y - lastPos.y);


        farBackground.position += new Vector3(amountMove.x, amountMove.y, 0.0f);
        middleBackground.position += new Vector3(amountMove.x, 0.0f, 0.0f) * 0.9f;

        lastPos = transform.position;
    }

    
}
