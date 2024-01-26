using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    public GameObject player, character;

    public Animator animator;
    public override void DoAction()
    {
        character.GetComponent<Enemy>().isAttacking = true;

        

        /*if (character.GetComponentInChildren<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Attack") &&
            character.GetComponentInChildren<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f) 
        {
            Debug.Log("HEY1");
            character.GetComponent<Enemy>().isAttacking = false;
            animator.SetBool("Attack", false);
        }*/

        if (!character.GetComponent<Enemy>().AnimatorIsPlaying("Attack"))
        {
            animator.SetBool("Attack", true);
        }
        
    }

    

}
