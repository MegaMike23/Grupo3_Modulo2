using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private NavMeshAgent agent;

    private StateMachineFlexible stateMachineEnemy;
    public List<Transform> targetsPatrol;

    public GameObject player;
    public float minDistancePlayer;

    public bool isAttacking = false;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        stateMachineEnemy = GetComponent<StateMachineFlexible>();
        player = GameObject.FindWithTag("Player");

        PatrolState patrol = new PatrolState();
        stateMachineEnemy.actualState = patrol;
        patrol.targetsPatrol = targetsPatrol;
        patrol.character = gameObject;

        FollowState follow = new FollowState();
        follow.agent = agent;
        follow.player = player;

        LessDistanceCondition conditionLessDistance = new LessDistanceCondition();
        conditionLessDistance.character = gameObject;
        conditionLessDistance.player = player;
        conditionLessDistance.minDistancePlayer = minDistancePlayer;

        LessDistanceCondition conditionMoreDistance = new LessDistanceCondition();
        conditionMoreDistance.inverted = true;
        conditionMoreDistance.character = gameObject;
        conditionMoreDistance.player = player;
        conditionMoreDistance.minDistancePlayer = minDistancePlayer;

        Transition patrolToFollow = new Transition();
        patrolToFollow.condition = conditionLessDistance;
        patrolToFollow.destinationState = follow;

        Transition followToPatrol = new Transition();
        followToPatrol.condition = conditionMoreDistance;
        followToPatrol.destinationState = patrol;

        patrol.AddTransition(patrolToFollow);
        follow.AddTransition(followToPatrol);
    }

}
