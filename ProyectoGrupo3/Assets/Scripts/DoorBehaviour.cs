using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;

public class DoorBehaviour : MonoBehaviour
{
    [SerializeField] private MMF_Player feedbackOpenDoor;

    private List<BotonDoorBehaviour> botonDoorBehavioursList;

    private void Awake()
    {
        botonDoorBehavioursList = new List<BotonDoorBehaviour>();
    }

    public void CheckDoorBotonList()
    {
        foreach (BotonDoorBehaviour botonColor in botonDoorBehavioursList)
        {
            if (!botonColor.GetIsActivated()) return; 
        }

        feedbackOpenDoor.PlayFeedbacks();
    }

    public void AddToList(BotonDoorBehaviour botonDoorBehaviour)
    {
        botonDoorBehavioursList.Add(botonDoorBehaviour);
    }
}
