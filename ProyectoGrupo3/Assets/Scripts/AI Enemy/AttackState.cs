using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    public GameObject player, character;
    public override void DoAction()
    {
        character.GetComponent<Enemy>().isAttacking = true;

        if (character.GetComponentInChildren<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Attack") &&
            character.GetComponentInChildren<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f) //Ha finalizado Attack
        {
            character.GetComponent<Enemy>().isAttacking = false;
        }
    }

    

}
