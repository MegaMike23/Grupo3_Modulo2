using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PausaManager : MonoBehaviour
{
    public GameObject cuadroPausa;//Menu de pausa en el canvas

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Pausa o Continuar
        if (Input.GetKeyDown((KeyCode)'p'))
        {
            if (Time.timeScale == 1)
            {
                Time.timeScale = 0;//Pausar el tiempo
                Pausar();
            }
            else if (Time.timeScale == 0)
            {
                Time.timeScale = 1;//Continuar el tiempo
                Continuar();
            }
        }
    }

    //Activar el menú de pausa
    private void Pausar()
    {
        cuadroPausa.SetActive(true);
    }

    //Desactivar el menú de pausa
    private void Continuar()
    {
        cuadroPausa.SetActive(false);
    }
}
