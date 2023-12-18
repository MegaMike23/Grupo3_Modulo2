using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroManager : MonoBehaviour
{

    void Start()
    {
        if (AudioManager.Instance == null)
        {
            Debug.Log("No hay INSTANCE de audio manager previamente creado!");
        }
        else
        {
            AudioManager.Instance.PlayMusic("Intro");
        }      
    }


}