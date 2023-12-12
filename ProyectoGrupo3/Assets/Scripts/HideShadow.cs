using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideShadow : MonoBehaviour
{
    private List<Light> lights;

    private void Awake()
    {
        GameObject l = GameObject.FindGameObjectWithTag("Lights");
        lights = new List<Light>(l.GetComponentsInChildren<Light>());
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bool hide = CanHide();
    }

    private bool CanHide()
    {

        bool canHide = true;

        foreach (Light l in lights)
        {
            if ((l.type == LightType.Point) || (l.type == LightType.Spot))
            {
                float distance = Vector3.Distance(transform.position, l.transform.position);
                Vector3 lDirection = l.transform.position - transform.position;
                RaycastHit hitInfo;
                bool raycastHit = Physics.Raycast(transform.position, lDirection, out hitInfo, distance);
                Color rayColor = Color.green;

                bool far = distance >= l.range;


                if ((!far && !raycastHit))
                {
                    canHide = false;
                    rayColor = Color.red;
                    Debug.DrawRay(transform.position, lDirection, rayColor);
                    break;
                }
                Debug.DrawRay(transform.position, lDirection, rayColor);

            }
        }

        return canHide;
    }
}
