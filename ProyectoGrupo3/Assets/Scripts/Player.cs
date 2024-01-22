using System;
using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Timeline.Actions;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject meshNinja;
    [SerializeField] private int playerLayerMaskNum;
    private Material ninjaMat;
    [SerializeField] private Material shadowMat;
    [SerializeField] private int shadowLayerMaskNum;
    [SerializeField] private Transform ragdollPrefab;
    [SerializeField] private Transform originalRootBoneTransform;

    // Start is called before the first frame update
    void Start()
    {

        ShadowBehaviour.OnAnyShadowTriggerInside += ShadowBehaviour_OnAnyShadowTriggerInside;
        ShadowBehaviour.OnAnyShadowTriggerOutside += ShadowBehaviour_OnAnyShadowTriggerOutside;
        
        Enemy.OnAnyEnemyAttackSuccess += Enemy_OnAnyEnemyAttackSuccess;

        ninjaMat = meshNinja.GetComponent<Renderer>().material;

        gameObject.layer = playerLayerMaskNum;
    }

    private void Enemy_OnAnyEnemyAttackSuccess(object sender, EventArgs e)
    {
        Debug.Log("Ragdoll Activate");
        Transform ragdollTransform = Instantiate(ragdollPrefab, transform.position, transform.rotation);
        PlayerRagdoll playerRagdoll = ragdollTransform.GetComponent<PlayerRagdoll>();
        playerRagdoll.Setup(originalRootBoneTransform);

        //Como destruiremos este player tambien le sacamos los Eventos asociados
        Enemy.OnAnyEnemyAttackSuccess = null; 
        ShadowBehaviour.OnAnyShadowTriggerInside = null;
        ShadowBehaviour.OnAnyShadowTriggerOutside = null;

        Destroy(gameObject);
    }

    private void ShadowBehaviour_OnAnyShadowTriggerOutside(object sender, EventArgs e)
    {
        Debug.Log("Fuera de Shadow");
        meshNinja.GetComponent<Renderer>().material = ninjaMat;
        gameObject.layer = playerLayerMaskNum;
    }

    private void ShadowBehaviour_OnAnyShadowTriggerInside(object sender, EventArgs e)
    {
        Debug.Log("Dentro de Shadow");
        meshNinja.GetComponent<Renderer>().material = shadowMat;
        gameObject.layer = shadowLayerMaskNum;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
