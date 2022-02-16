using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundNinja : MonoBehaviour
{
    public float moveSpeed;
    public float rangeOfVision;
    public float rangeOfAttack;

    public Transform player;

    private Rigidbody2D Rigidbody2D;
    private Animator Animator;
    private float playerDistance;



    void Start()
    {
        Animator = GetComponent<Animator>();
        Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        playerDistance = Vector2.Distance(player.position, Rigidbody2D.position);
        if (player.position.x < Rigidbody2D.position.x) transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        else transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);

        if (playerDistance >= rangeOfVision) moveSpeed = 0;
        else moveSpeed = 3;

        Vector2 Objetivo = new Vector2(player.position.x, -2.37012f);
        Vector2 newPosition = Vector2.MoveTowards(Rigidbody2D.position, Objetivo, moveSpeed * Time.deltaTime);
        Rigidbody2D.MovePosition(newPosition);

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, rangeOfVision);

    }
}
