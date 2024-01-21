using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private NavMeshAgent agent;
    private StateMachineFlexible stateMachineEnemy;

    public Animator animator;
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
        patrol.animator = animator; 

        FollowState follow = new FollowState();
        follow.agent = agent;
        follow.player = player;
        follow.animator = animator;

        AttackState attack = new AttackState();
        attack.player = player;
        attack.character = gameObject;
        attack.animator = animator;

        LessDistanceCondition conditionLessDistance = new LessDistanceCondition();
        conditionLessDistance.character = gameObject;
        conditionLessDistance.player = player;
        conditionLessDistance.minDistancePlayer = minDistancePlayer;

        LessDistanceCondition conditionMoreDistance = new LessDistanceCondition();
        conditionMoreDistance.inverted = true;
        conditionMoreDistance.character = gameObject;
        conditionMoreDistance.player = player;
        conditionMoreDistance.minDistancePlayer = minDistancePlayer;

        CanAttackCondition conditionCanAttack = new CanAttackCondition();
        conditionCanAttack.character = gameObject;
        conditionCanAttack.player = player;

        CanAttackCondition finishConditionCanAttack = new CanAttackCondition();
        finishConditionCanAttack.inverted = true;
        finishConditionCanAttack.character = gameObject;
        finishConditionCanAttack.player = player;

        Transition patrolToFollow = new Transition();
        patrolToFollow.condition = conditionLessDistance;
        patrolToFollow.destinationState = follow;

        Transition followToPatrol = new Transition();
        followToPatrol.condition = conditionMoreDistance;
        followToPatrol.destinationState = patrol;

        Transition followToAttack = new Transition();
        followToAttack.condition = conditionCanAttack;
        followToAttack.destinationState = attack;

        Transition attackToFollow = new Transition();
        attackToFollow.condition = finishConditionCanAttack;
        attackToFollow.destinationState = follow;


        patrol.AddTransition(patrolToFollow);
        follow.AddTransition(followToPatrol);
        follow.AddTransition(followToAttack);
        attack.AddTransition(attackToFollow);
    }

}
