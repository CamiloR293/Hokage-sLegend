using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCamera : MonoBehaviour
{
    public GameObject MainCamera;
    public GameObject CameraBoss;
    public GameObject LifeBoss;
    public GameObject wall;
    private void Start()
    {
        MainCamera.SetActive(true);
        CameraBoss.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerHitBox"))
        {
            LifeBoss.SetActive(true);
            MainCamera.SetActive(false);
            CameraBoss.SetActive(true);
            MainCamera.GetComponent<CameraScript>().boss = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerHitBox"))
        {
            wall.SetActive(true);
        }
    }
}
