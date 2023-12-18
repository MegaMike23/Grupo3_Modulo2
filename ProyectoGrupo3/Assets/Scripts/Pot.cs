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

        if (ninjaCharacter != null) 
        {
            ninjaCharacter.pots += num;
            Destroy(gameObject);
        }

        if(ninjaCharacter.pots == 4) 
        {
            SceneManager.LoadScene("Object");
        }
    }
}
