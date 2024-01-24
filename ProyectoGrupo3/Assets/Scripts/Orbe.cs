using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Orbe : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerMovement>()!= null)
        {
            SceneManager.LoadScene("INTRO");
        }
        
    }
}
