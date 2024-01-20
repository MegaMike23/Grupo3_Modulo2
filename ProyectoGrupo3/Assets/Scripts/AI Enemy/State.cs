using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    public List<Transition> transitions = new List<Transition>();
    public abstract void DoAction();

    public void AddTransition(Transition transition)
    {
        transitions.Add(transition);
    }
}
