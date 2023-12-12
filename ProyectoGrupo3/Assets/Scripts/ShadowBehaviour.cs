using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowBehaviour : MonoBehaviour
{
    public static EventHandler OnAnyShadowTriggerInside; //Evento statico para quien escucha no necesite saber de que objeto especifico viene
    public static EventHandler OnAnyShadowTriggerOutside;

    private bool isInsideTrigger;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isInsideTrigger)
        {
            OnAnyShadowTriggerInside?.Invoke(this, EventArgs.Empty);
            isInsideTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && isInsideTrigger)
        {
            OnAnyShadowTriggerOutside?.Invoke(this, EventArgs.Empty);
            isInsideTrigger = false;
        }
    }

}
