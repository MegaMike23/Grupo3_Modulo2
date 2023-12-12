using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using UnityEngine;

public class Player : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        ShadowBehaviour.OnAnyShadowTriggerInside += ShadowBehaviour_OnAnyShadowTriggerInside;
        ShadowBehaviour.OnAnyShadowTriggerOutside += ShadowBehaviour_OnAnyShadowTriggerOutside;
    }

    private void ShadowBehaviour_OnAnyShadowTriggerOutside(object sender, EventArgs e)
    {
        Debug.Log("Fuera de Shadow");
    }

    private void ShadowBehaviour_OnAnyShadowTriggerInside(object sender, EventArgs e)
    {
        Debug.Log("Dentro de Shadow");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
