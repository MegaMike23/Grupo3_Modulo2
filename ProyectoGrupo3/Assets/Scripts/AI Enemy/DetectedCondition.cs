using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectedCondition : Condition
{
    public GameObject character;

    public override bool Check()
    {
        return character.GetComponent<Enemy>().GetIsPlayerDetected();

    }
}
