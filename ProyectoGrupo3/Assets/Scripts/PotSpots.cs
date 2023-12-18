using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotSpots : MonoBehaviour
{
    public GameObject pots;

    void Start()
    {
        Vector3 pos1 = new Vector3(-1, 1, -6);
        Vector3 pos2 = new Vector3(4, 1, -7);
        Vector3 pos3 = new Vector3(4, 1, -2);

        Instantiate(pots, pos1, Quaternion.identity);
        Instantiate(pots, pos2, Quaternion.identity);
        Instantiate(pots, pos3, Quaternion.identity);
    }
    
}
