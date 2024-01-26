using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanAttackCondition : Condition
{
    public GameObject character, player;
    public float minDistancePlayer = 2.1f;

    public override bool Check()
    {
        if (player == null) return false;

        return (Vector3.Distance(character.transform.position, player.transform.position) <= minDistancePlayer 
            && !character.GetComponent<Enemy>().isAttacking);
    }
}
