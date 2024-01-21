using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowState : State
{
    public GameObject player;
    public NavMeshAgent agent;
    public Animator animator;
    public override void DoAction()
    {
        agent.SetDestination(player.transform.position);
        animator.SetBool("Patrol", false);
        animator.SetBool("Follow", true);
    }
}
