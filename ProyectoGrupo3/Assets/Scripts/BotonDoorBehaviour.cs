using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains;
using MoreMountains.Feedbacks;

public class BotonDoorBehaviour : MonoBehaviour
{
    [SerializeField] private DoorBehaviour door;
    private MMF_Player feedbackBoton;
    private bool isActivated = false;

    // Start is called before the first frame update
    void Start()
    {
        feedbackBoton = GetComponentInChildren<MMF_Player>();
        door.AddToList(this);
    }

    
    public void ActivateButton()
    {
        isActivated = true;
        feedbackBoton.PlayFeedbacks();
        door.CheckDoorBotonList();

    }

    public bool GetIsActivated()
    {
        return isActivated;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>() && !isActivated)
        {
            ActivateButton();
        }
    }
}
