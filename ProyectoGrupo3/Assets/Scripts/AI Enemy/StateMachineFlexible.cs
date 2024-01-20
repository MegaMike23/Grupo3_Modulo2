using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachineFlexible : MonoBehaviour
{
    public State actualState;


    // Update is called once per frame
    void Update()
    {

        foreach (Transition transition in actualState.transitions)
        {
            if (transition.CheckCondition())
            {
                actualState = transition.destinationState;
            }
        }

        actualState.DoAction();
    }
}
