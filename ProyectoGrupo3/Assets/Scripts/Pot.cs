using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pot : MonoBehaviour
{
    public int num;
    private void OnTriggerEnter(Collider other)
    {
        PlayerMovement ninjaCharacter = other.gameObject.GetComponent<PlayerMovement>();

        Debug.Log("Colision con " + other.gameObject.name);

        if (ninjaCharacter != null && GameManager.Instance != null)
        {
            GameManager.Instance.AddPot(); //Activamos funcion en el game manager
            Destroy(gameObject);
        }

        if (GameManager.Instance != null && GameManager.Instance.GetPot() == 3)
        {
            SceneManager.LoadScene("INTRO");
        }
    }
}
