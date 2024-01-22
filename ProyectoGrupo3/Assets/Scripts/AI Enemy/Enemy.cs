using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using MoreMountains;
using MoreMountains.Feedbacks;

public class Enemy : MonoBehaviour
{
    private NavMeshAgent agent;
    private StateMachineFlexible stateMachineEnemy;

    public Animator animator;
    public List<Transform> targetsPatrol;

    public GameObject player;
    public float minDistancePlayer;

    public bool isAttacking = false;
    private SphereCollider sphereAttack;
    [SerializeField] private MMF_Player attackSuccessFeedback;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        stateMachineEnemy = GetComponent<StateMachineFlexible>();
        player = GameObject.FindWithTag("Player");
        sphereAttack = GetComponent<SphereCollider>();

        PatrolState patrol = new PatrolState();
        stateMachineEnemy.actualState = patrol;
        patrol.targetsPatrol = targetsPatrol;
        patrol.character = gameObject;
        patrol.animator = animator; 

        FollowState follow = new FollowState();
        follow.agent = agent;
        follow.player = player;
        follow.animator = animator;
        follow.character = gameObject;

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("DAÑO A JUGADOR");
            StartCoroutine("DamagePlayer");
            //attackSuccessFeedback.PlayFeedbacks();
        }
    }

    IEnumerator DamagePlayer()
    {
        Time.timeScale = 0.25f;

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(1);

        Time.timeScale = 1.0f;

        GameManager.Instance.ChangeLives(1);
    }


    bool AnimatorIsPlaying()
    {
        return animator.GetCurrentAnimatorStateInfo(0).length >
               animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
    }

    public bool AnimatorIsPlaying(string stateName)
    {
        return AnimatorIsPlaying() && animator.GetCurrentAnimatorStateInfo(0).IsName(stateName);
    }

}
