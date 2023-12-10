using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvokeScript : MonoBehaviour
{

    [SerializeField] private GameObject stick;
    // Start is called before the first frame update
    void Start()
    {
        Invoke(nameof(Spawn), 2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Spawn()
    {
        Instantiate(stick, new Vector3(0,1,0), Quaternion.identity);
    }
}
