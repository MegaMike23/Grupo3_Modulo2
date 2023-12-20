using System.Collections;
using System.Collections.Generic;
//using TreeEditor;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float rangoAlerta;//Rango de detección
    public LayerMask capaJugador;//Capa que detecta al jugador
    private Transform jugador;//Posición del jugador
    public float velocidad;//Velocidad de movimiento del enemigo
    bool estarAlerta;//Detecta si el jugador entró en el rango de detección

    // Start is called before the first frame update
    void Start()
    {
        jugador = GameObject.FindGameObjectsWithTag("Player")[0].transform;
    }

    // Update is called once per frame
    void Update()
    {
      
        //Detectar la presencia del jugador
        estarAlerta = Physics.CheckSphere(transform.position, rangoAlerta, capaJugador);

        //Si detecta al jugador
        if (estarAlerta)
        {
            //Mirar al jugador
            transform.LookAt(new Vector3(jugador.position.x,transform.position.y,jugador.position.z));
            //Que se mueva hacia el jugador
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(jugador.position.x, transform.position.y, jugador.position.z), velocidad * Time.deltaTime);
        }

        /*Vector3 newPosition = new Vector3((targetObj.position.x)+1.0f, targetObj.position.y, (targetObj.position.z)+1.0f);
        transform.position = Vector3.MoveTowards(this.transform.position, newPosition, 5 * Time.deltaTime);*/
    }

    //Dibujar rango esfera para medir radio de detección en modo dev
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, rangoAlerta);
    }

    //Cuando el enemigo colisione con el jugador
    private void OnCollisionEnter(Collision collision)
    {
        PlayerMovement player = collision.gameObject.GetComponent<PlayerMovement>();
        if (player != null)
        {
            GameManager.Instance.ChangeLives(1); //Actualiza el Fill Amount del corazón en la interfaz
        }

        Debug.Log("Colisiona");
    }
}
