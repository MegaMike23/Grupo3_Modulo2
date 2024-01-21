using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LessDistanceCondition : Condition
{
    public GameObject character, player;
    public float minDistancePlayer = 10f;

    public override bool Check()
    {
       return (Vector3.Distance(character.transform.position, player.transform.position) <= minDistancePlayer);

    }
}
