using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.TextCore.Text;

public class FollowState : State
{
    public GameObject player, character;
    public NavMeshAgent agent;
    public Animator animator;

    private float stopDistance = 2.0f; //Debe ser menor a la distancia de algun otro estado siguiente como ataque
    public override void DoAction()
    {
        if (player != null)
        {
            if (Vector3.Distance(character.transform.position, player.transform.position) <= stopDistance)
            {
                agent.isStopped = true;
            }
            else
            {
                agent.isStopped = false;
                agent.SetDestination(player.transform.position);
            }
        }

        

        if (character.GetComponent<Enemy>().isAttacking)
        {
            character.GetComponent<Enemy>().isAttacking = false;
        }

        animator.SetBool("Attack", false);
        animator.SetBool("Patrol", false);
        animator.SetBool("Follow", true);
    }
}
