using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartManager : MonoBehaviour
{

    public float currentHealth = 100f;//Vida actual del usuario
    public float maxHealth = 100f;//Vida máxima del usuario
    public Image heart;//Imagen del corazon
   

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Actualizar corazones
    public void UpdateInterface()
    {
        heart.fillAmount = currentHealth/maxHealth;
    }

}
