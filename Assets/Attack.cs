using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{

    EnemyController ColliderGolpeM;
    private void Start()
    {
        ColliderGolpeM = GetComponent<EnemyController>();
    }

    void Update()
    {
        

        if(ColliderGolpeM.movingright)
        {
            Debug.Log(("Moving Right true")); ;
            //transform.localPosition = transform.position;
        }
        //else
        //{
        //    transform.position = new Vector3(transform.position.x - 2, transform
        //        .position.y, transform.position.z);
        //}
    }
}
