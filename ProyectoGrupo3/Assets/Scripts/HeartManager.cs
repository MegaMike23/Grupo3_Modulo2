using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartManager : MonoBehaviour
{
    public static HeartManager Instance { get; private set; }//instancia del Heart Manager
    public GameObject[] hearts;//Arreglo con el número de corazones/vidas
    private int life = 5;//vida actual

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.Log("Más de un Heart Manager en la escena");
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Desactiva el corazón en el HUD
    public void TakeDamage(int d)
    {
        hearts[d].SetActive(false);
    }

    //Pierde una vida
    public void LoseLife()
    {
        life -= 1;
        TakeDamage(life);
    }

}
