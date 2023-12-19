using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvokeScript : MonoBehaviour
{
    [SerializeField] private GameObject Activo;
    [SerializeField] private GameObject stick;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating(nameof(Spawn), 2, 4);
    }

    // Update is called once per frame
    void Update()
    {
        

    }

    private void Spawn()
    {
        float x = Random.Range(-4.0f,16.0f);
        float z = Random.Range(-4.0f,16.0f);
        //Instantiate(stick, new Vector3(x,1,z), Quaternion.identity);
    }

    
}
