using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains;
using MoreMountains.Feedbacks;

public class AttackController : MonoBehaviour
{
    [SerializeField] private MMF_Player initAttackFeedback;
    [SerializeField] private MMF_Player finishAttackFeedback;
    public void InitAttackCollider()
    {
        initAttackFeedback.PlayFeedbacks();
    }

    public void FinishAttackCollider()
    {
        finishAttackFeedback.PlayFeedbacks();
    }
}
