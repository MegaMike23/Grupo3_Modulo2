using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoreDistanceCondition : Condition
{
    public GameObject character, player;
    public float nonDetectPlayerDistance = 10f;

    public override bool Check()
    {
        if (player != null
            && character.GetComponent<Enemy>().GetIsPlayerDetected()
            && Vector3.Distance(character.transform.position, player.transform.position) > nonDetectPlayerDistance 
            || player != null && player.GetComponent<Player>().GetIsShadow())
        {
            character.GetComponent<Enemy>().SetIsPlayerDetected(false);
            character.GetComponent<Enemy>().visionConeEnemy.DetectMaterialVisualCone(false);
            return true;
        }
        return false;

    }
}
