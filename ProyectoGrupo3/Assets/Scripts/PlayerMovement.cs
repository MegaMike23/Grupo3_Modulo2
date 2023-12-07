using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController characterController;
    private PlayerInput playerInput;
    [SerializeField] private Transform cameraPlayer;        //Camara que mira al jugador


   [SerializeField] private float sneekySpeed = 2.0f;        //Velocidad del personaje
    [SerializeField] private float runSpeed = 8.0f;
    [SerializeField] private float turnTime = 0.1f;     //Tiempo para girar de manera progresiva
    private float turnVelocity;                         //variable privatda para la velocidad del giro - no es necesario tocarla

    private bool isStaticPos = false; //Para aquellas acciones que debemos mantenernos estaticos

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>(); 
    }

    // Update is called once per frame
    void Update()
    {
        //Captamos el INPUT por el mapa de acciones
        Vector2 movementInput = playerInput.actions["Move"].ReadValue<Vector2>();

        Vector3 direction = new Vector3(movementInput.x, 0.0f, movementInput.y).normalized;

        if (playerInput.actions["Dance"].WasPressedThisFrame())
        {
            Debug.Log("DANCE");
            isStaticPos = true;
        }

        if (playerInput.actions["Dance"].WasReleasedThisFrame())
        {
            Debug.Log("FINISH DANCE");
            isStaticPos = false;
        }

        //CHARACTER CONTROLLER & MOVIMIENTO DEL JUGADOR

        if (direction.magnitude >= 0.01f && !isStaticPos)
        {
            //Rotación del personaje
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cameraPlayer.eulerAngles.y; //Angulo de rotación respecto a la camara
            float angleSmooth = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnVelocity, turnTime); //Rotación smooth mediante angulo a rotar
            transform.rotation = Quaternion.Euler(0.0f, angleSmooth, 0.0f);

            Vector3 moveDirection = Quaternion.Euler(0.0f, targetAngle, 0.0f) * Vector3.forward; //Movemos hacia delante según la vista de la camara (targetAngle y vector forward)

            //Velocidad del personaje
            float characterSpeed;

            if (playerInput.actions["Run"].IsPressed())
            {
                Debug.Log("Running");
                characterSpeed = runSpeed;
            }
            else
            {
                Debug.Log("Sneaky");
                characterSpeed = sneekySpeed;
            }

            characterController.Move(moveDirection * characterSpeed * Time.deltaTime);
        }

    }
}
