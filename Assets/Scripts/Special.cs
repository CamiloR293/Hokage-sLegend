using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Special : StateMachineBehaviour
{

    [SerializeField] private GameObject ByujiDama;

     public Transform Boss;
   // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       /* gaara = animator.GetComponent<Boss>();
        Player = gaara.Player;

        gaara.SearchPlayer();
   
      //Instantiate(ByujiDama, Boss.position, Quaternion.identity);

       

       // Vector2 position = new Vector2(Boss.position.x, Boss.position.y);

       //Instantiate(ByujiDama, position, Quaternion.identity);
*/
         
        Vector3 direccion;
        if (Boss.localScale.x == 1.0f) direccion = Vector3.right;
        else direccion = Vector3.left;

        GameObject HabilitySpecial = Instantiate(ByujiDama, Boss.position, Quaternion.identity);
        HabilitySpecial.GetComponent<Hability>().SetDirection(direccion);

      
    }

  

  

 
}
