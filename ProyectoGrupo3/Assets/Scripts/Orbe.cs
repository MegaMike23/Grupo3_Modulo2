using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Orbe : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        PlayerMovement ninjaCharacter = gameObject.GetComponent<PlayerMovement>();

        if (ninjaCharacter != null && GameManager.Instance != null)
        {
            SceneManager.LoadScene("INTRO");
        }
    }
}
