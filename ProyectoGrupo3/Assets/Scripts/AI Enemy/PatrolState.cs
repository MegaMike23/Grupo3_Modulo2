using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolState : State
{
    public List<Transform> targetsPatrol = new List<Transform>();
    public int indexNextPoint;
    private float minDistancePoint = 1.5f;

    private NavMeshAgent agent;
    public GameObject character;

    public Animator animator;

    public override void DoAction()
    {
        if (agent == null)
        {
            agent = character.GetComponent<NavMeshAgent>();
        }

        if (Vector3.Distance(character.transform.position, targetsPatrol[indexNextPoint].position) < minDistancePoint)
        {
            indexNextPoint++;
            if (indexNextPoint >= targetsPatrol.Count)
            {
                indexNextPoint = 0;
            }
        }

        agent.SetDestination(targetsPatrol[indexNextPoint].position);
        
        animator.SetBool("Follow", false);
        animator.SetBool("Patrol", true);

    }
}
