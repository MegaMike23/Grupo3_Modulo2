using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject meshNinja;
    public Material ninjaMat;
    [SerializeField] private Material shadowMat;

    // Start is called before the first frame update
    void Start()
    {
        ShadowBehaviour.OnAnyShadowTriggerInside += ShadowBehaviour_OnAnyShadowTriggerInside;
        ShadowBehaviour.OnAnyShadowTriggerOutside += ShadowBehaviour_OnAnyShadowTriggerOutside;

        ninjaMat = meshNinja.GetComponent<Renderer>().material;
    }

    private void ShadowBehaviour_OnAnyShadowTriggerOutside(object sender, EventArgs e)
    {
        Debug.Log("Fuera de Shadow");
        meshNinja.GetComponent<Renderer>().material = ninjaMat;
    }

    private void ShadowBehaviour_OnAnyShadowTriggerInside(object sender, EventArgs e)
    {
        Debug.Log("Dentro de Shadow");
        meshNinja.GetComponent<Renderer>().material = shadowMat;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}