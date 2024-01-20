using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transition
{
    public Condition condition;
    public State destinationState;

    public bool CheckCondition()
    {
        return condition.Check();
    }
}
