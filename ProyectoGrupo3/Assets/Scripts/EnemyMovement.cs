using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform targetObj;//Ninja
    public float rangoAlerta;//Rango de detección
    public LayerMask capaJugador;//Capa que detecta al jugador
    bool estarAlerta;//Detecta si el jugador entró en el rango de detección
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Detectar la presencia del jugador
        estarAlerta = Physics.CheckSphere(transform.position, rangoAlerta, capaJugador);

        Vector3 newPosition = new Vector3((targetObj.position.x)+1.0f, targetObj.position.y, (targetObj.position.z)+1.0f);
        transform.position = Vector3.MoveTowards(this.transform.position, newPosition, 5 * Time.deltaTime);
    }

    private void onDrawGizmos()
    {

    }
}
