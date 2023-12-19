using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotSpots : MonoBehaviour
{
    [SerializeField] private Transform[] points; 
    public GameObject pots;

    void Start()
    {
        for (int i = 0; i < points.Length; i++)
        {
            Instantiate(pots, points[i].position, Quaternion.identity);
        }
    }
    
}
