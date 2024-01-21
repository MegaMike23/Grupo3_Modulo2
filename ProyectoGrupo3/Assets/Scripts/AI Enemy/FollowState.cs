using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowState : State
{
    public GameObject player;
    public NavMeshAgent agent;
    public override void DoAction()
    {
        agent.SetDestination(player.transform.position);
    }
}
