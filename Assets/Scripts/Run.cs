
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Run : StateMachineBehaviour
{

    private Boss gaara;
    Transform Player;
    private Rigidbody2D body;

    [SerializeField] public float SpeedMov;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateinfo, int layerindex)
    {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        body = animator.GetComponent<Rigidbody2D>();
        animator.SetInteger("Random", Random.Range(0, 4));
        /*gaara = animator.GetComponent<Gaara>();
        body = gaara.body;

        gaara.SearchPlayer();
        */
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        animator.GetComponent<Boss>().SearchPlayer();
        //body.velocity = new Vector2(SpeedMov, body.velocity.y) * animator.transform.right;
        Vector2 target = new Vector2(Player.position.x, body.position.y);

        Vector2 newPos = Vector2.MoveTowards(body.position, target, SpeedMov * Time.fixedDeltaTime);
        body.MovePosition(newPos);

    }
}