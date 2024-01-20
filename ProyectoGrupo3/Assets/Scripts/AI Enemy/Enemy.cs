using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    NavMeshAgent agent;

    public StateMachineFlexible stateMachineEnemy;
    public List<Transform> targetsPatrol;

    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        /*agent = GetComponent<NavMeshAgent>();
        stateMachineEnemy = GetComponent<StateMachineFlexible>();

        PatrolState patrol = new PatrolState();
        stateMachineEnemy.actualState = patrol;
        patrol.targetsPatrol = targetsPatrol;
        patrol.character = gameObject;

        FollowState follow = new FollowState();
        follow.agent = agent;
        follow.player = player;

        LessDistancePlayerCondition conditionLessDistance = new LessDistancePlayerCondition();
        conditionLessDistance.character = gameObject;
        conditionLessDistance.player = player;

        LessDistancePlayerCondition conditionMoreDistance = new LessDistancePlayerCondition();
        conditionMoreDistance.inverted = true;
        conditionMoreDistance.character = gameObject;
        conditionMoreDistance.player = player;

        Transition patrolToFollow = new Transition();
        patrolToFollow.condition = conditionLessDistance;
        patrolToFollow.destinationState = follow;

        Transition followToPatrol = new Transition();
        followToPatrol.condition = conditionMoreDistance;
        followToPatrol.destinationState = patrol;

        patrol.AddTransition(patrolToFollow);
        follow.AddTransition(followToPatrol);*/
    }

}
