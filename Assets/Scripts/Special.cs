using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Special : StateMachineBehaviour
{

    [SerializeField] private GameObject ByujiDama;

     public Transform LauncPoint;


    private Boss gaara;

    private Transform Player;

   
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        gaara = animator.GetComponent<Boss>();
        Player = gaara.Player;

        gaara.SearchPlayer();
   
      Instantiate(ByujiDama, LauncPoint.position, LauncPoint.rotation);

       

       // Vector2 position = new Vector2(LauncPoint.position.x, LauncPoint.position.y);

      // Instantiate(ByujiDama, position, Quaternion.identity);

      
    }

  

  

 
}
