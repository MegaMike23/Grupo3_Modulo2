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

    private Vector3 followOffset;
    [SerializeField] private float followOffsetMinY = 1f;
    [SerializeField] private float followOffsetMaxY = 20f;

    private void Awake()
    {
        followOffset = virtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset;
    }

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
        float zoomAmount = 5f;

        if(Input.mouseScrollDelta.y > 0)
        {
            targetFOV -= zoomAmount;
            followOffset.y -= zoomAmount;
        }
        if (Input.mouseScrollDelta.y < 0)
        {
            targetFOV += zoomAmount;
            followOffset.y += zoomAmount;
        }

        targetFOV = Mathf.Clamp(targetFOV, FOVMin, FOVMax);
        followOffset.y = Mathf.Clamp(followOffset.y, followOffsetMinY, followOffsetMaxY);

        
        float zoomSpeed = 10f;

        virtualCamera.m_Lens.FieldOfView = //targetFOV;
            Mathf.Lerp(virtualCamera.m_Lens.FieldOfView, targetFOV, Time.deltaTime * zoomSpeed);

        virtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset =
            Vector3.Lerp(virtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset, followOffset, Time.deltaTime*zoomSpeed);
    }
}
