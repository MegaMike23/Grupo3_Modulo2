using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using MoreMountains;
using MoreMountains.Feedbacks;
using System;

public class Enemy : MonoBehaviour
{
    public static EventHandler OnAnyEnemyAttackSuccess;

    private NavMeshAgent agent;
    private StateMachineFlexible stateMachineEnemy;

    public Animator animator;
    public List<Transform> targetsPatrol;

    public GameObject player;
    public float minDistancePlayer;

    public bool isAttacking = false;
    private bool isPlayerDetected = false;

    public VisionCone visionConeEnemy;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        stateMachineEnemy = GetComponent<StateMachineFlexible>();
        player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            Debug.Log("PLAYER IS NOT NULL");
        }

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

        DetectedCondition conditionDetectPlayer = new DetectedCondition();
        conditionDetectPlayer.character = gameObject;

        DetectedCondition conditionUNDETECTPlayer = new DetectedCondition();
        conditionUNDETECTPlayer.inverted = true;
        conditionUNDETECTPlayer.character = gameObject;

        CanAttackCondition conditionCanAttack = new CanAttackCondition();
        conditionCanAttack.character = gameObject;
        conditionCanAttack.player = player;

        CanAttackCondition finishConditionCanAttack = new CanAttackCondition();
        finishConditionCanAttack.inverted = true;
        finishConditionCanAttack.character = gameObject;
        finishConditionCanAttack.player = player;

        Transition patrolToFollow = new Transition();
        patrolToFollow.condition = conditionDetectPlayer;
        patrolToFollow.destinationState = follow;

        Transition followToPatrol = new Transition();
        followToPatrol.condition = conditionUNDETECTPlayer;
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
            Debug.Log("DA�O A JUGADOR");
            StartCoroutine("DamagePlayer");
        }
    }

    IEnumerator DamagePlayer()
    {
        OnAnyEnemyAttackSuccess?.Invoke(this, EventArgs.Empty);

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

    public bool GetIsPlayerDetected()
    {
        return isPlayerDetected;
    }

    public void SetIsPlayerDetected(bool playerDetected)
    {
        isPlayerDetected = playerDetected;
    }

}
