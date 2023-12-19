using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraSystem : MonoBehaviour
{
    private Transform player;
    [SerializeField] private float playerHeight = 1f;
    private Vector3 offsetVector;
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    [SerializeField] private bool useEdgeRotation = false;
    private float rotateDir = 0f;
    [SerializeField] private float rotateSpeed = 100f;
    [SerializeField] private float FOVMax = 60;
    [SerializeField] private float FOVMin = 10;
    private float targetFOV;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        targetFOV = virtualCamera.m_Lens.FieldOfView;
        offsetVector = new Vector3(0, playerHeight, 0);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.position + offsetVector;
        HandleCameraRotation();
        HandleCameraZoom();
    }

    private void HandleCameraRotation()
    {
        if (useEdgeRotation)
        {
            int edgeRotationSize = 20;

            if (Input.mousePosition.x > Screen.width - edgeRotationSize)
            {
                rotateDir = -1f;
            }
            else if (Input.mousePosition.x < edgeRotationSize)
            {
                rotateDir = +1f;
            }
            else
            {
                rotateDir = 0f;
            }
        }
        else
        {
            //Cogemos el input
            if (Input.GetKey(KeyCode.Q))
            {
                rotateDir = +1f;
            }
            else if (Input.GetKey(KeyCode.E))
            {
                rotateDir = -1f;
            }
            else { rotateDir = 0f; }
        }

        //Cambiamos la rotación del objeto
        transform.eulerAngles += new Vector3(0, rotateDir * rotateSpeed * Time.deltaTime, 0);

    }

    private void HandleCameraZoom()
    {
        if(Input.mouseScrollDelta.y > 0)
        {
            targetFOV -= 5;
        }
        if (Input.mouseScrollDelta.y < 0)
        {
            targetFOV += 5;
        }

        targetFOV = Mathf.Clamp(targetFOV, FOVMin, FOVMax);

       virtualCamera.m_Lens.FieldOfView = targetFOV;
    }
}
