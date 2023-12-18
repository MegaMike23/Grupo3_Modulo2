using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (AudioManager.Instance == null)
        {
            Debug.Log("No hay INSTANCE de audio manager previamente creado!");
        }
        else
        {
            AudioManager.Instance.PlayMusic("Background");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
