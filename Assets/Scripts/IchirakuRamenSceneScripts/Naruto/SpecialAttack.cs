using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAttack : MonoBehaviour
{
    float Cronometro = 1;
    //============================================================
    //                         RASENGAN
    //============================================================
    
    public void RasenganCharge(Animator animator, NarutoMovement player)
    {
        
        animator.SetBool("Animating_Something", true);
        animator.SetBool("Rasengan", true);
        Cronometro -= Time.deltaTime;
        if (Cronometro <= 0) Rasengan(animator, player);
    }

    public void Rasengan(Animator animator, NarutoMovement player)
    {
        Debug.Log("Rasengan jia");
        EndRasengan(animator, player);
    }

    


    public void EndRasengan(Animator animator, NarutoMovement player)
    {
        animator.SetBool("Rasengan", false);
        player.Rasengan = false;
        Cronometro = 1;
    }

}
