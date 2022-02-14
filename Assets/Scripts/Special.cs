using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Special : StateMachineBehaviour
{

    [SerializeField] private GameObject ByujiDama;


    private Boss gaara;

    private Transform Player;

   
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        gaara = animator.GetComponent<Boss>();
        Player = gaara.Player;

        gaara.SearchPlayer();

        Vector2 position = new Vector2(Player.position.x, Player.position.y);

        Instantiate(ByujiDama, position, Quaternion.identity);
    }

 
}
