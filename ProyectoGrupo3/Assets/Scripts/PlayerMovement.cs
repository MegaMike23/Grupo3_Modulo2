using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController characterController;
    [SerializeField] private Animator animator;
    private PlayerInput playerInput;
    [SerializeField] private Transform cameraPlayer;        //Camara que mira al jugador

    private float characterSpeed;                                    //Velocidad del personaje
    [SerializeField] private float sneekySpeedMax = 2.0f;       
    [SerializeField] private float runSpeedMax = 8.0f;
    [SerializeField] private float turnTime = 0.1f;     //Tiempo para girar de manera progresiva
    private float turnVelocity;                         //variable privatda para la velocidad del giro - no es necesario tocarla
    [SerializeField] private float gravity = 30;
    private float fallVelocity;
    private bool isJumping;
    [SerializeField] private float jumpForce = 8f;
    private Vector3 moveDirection;

    private float velocityInAnimation = 0.0f;
    private float acelerationInAnimation = 2.0f;
    private float deacelerationInAnimation = 3.0f;

    private bool isStaticPos = false; //Para aquellas acciones que debemos mantenernos estaticos

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();
        characterSpeed = sneekySpeedMax;
    }

    // Update is called once per frame
    void Update()
    {
        //Captamos el INPUT por el mapa de acciones
        Vector2 movementInput = playerInput.actions["Move"].ReadValue<Vector2>();

        Vector3 direction = new Vector3(movementInput.x, 0.0f, movementInput.y).normalized;

        /*
        if (playerInput.actions["Dance"].IsPressed() && !isStaticPos)
        {
            Debug.Log("DANCE");
            animator.SetBool("Dance", true);
            isStaticPos = true;
        }

        if (playerInput.actions["Dance"].WasReleasedThisFrame())
        {
            Debug.Log("FINISH DANCE");
            animator.SetBool("Dance", false);
            isStaticPos = false;
        }*/

        //CHARACTER CONTROLLER & MOVIMIENTO DEL JUGADOR

        if (direction.magnitude >= 0.01f && !isStaticPos)
        {
            //Rotación del personaje
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cameraPlayer.eulerAngles.y; //Angulo de rotación respecto a la camara
            float angleSmooth = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnVelocity, turnTime); //Rotación smooth mediante angulo a rotar
            transform.rotation = Quaternion.Euler(0.0f, angleSmooth, 0.0f);

            moveDirection = Quaternion.Euler(0.0f, targetAngle, 0.0f) * Vector3.forward; //Movemos hacia delante según la vista de la camara (targetAngle y vector forward)
            

            if (playerInput.actions["Run"].IsPressed())
            {
                Debug.Log("Running");

                characterSpeed = runSpeedMax;

                velocityInAnimation += acelerationInAnimation * Time.deltaTime;
                velocityInAnimation = Mathf.Clamp(velocityInAnimation, 0.3f, 1.0f);
            }
            else
            {
                Debug.Log("Sneaky");

                characterSpeed = sneekySpeedMax;

                if (velocityInAnimation > 0.3f)
                {
                    velocityInAnimation -= deacelerationInAnimation * Time.deltaTime;
                    velocityInAnimation = Mathf.Clamp(velocityInAnimation, 0.0f, 1f);
                }
                else
                {
                    velocityInAnimation += acelerationInAnimation * Time.deltaTime;
                    velocityInAnimation = Mathf.Clamp(velocityInAnimation, 0.0f, 0.3f);
                }
            }

            //characterController.Move(moveDirection * characterSpeed * Time.deltaTime);

        }
        else //Si no hay movimiento desaceleramos animacion hasta Idle
        {
            velocityInAnimation -= deacelerationInAnimation * Time.deltaTime;
            velocityInAnimation = Mathf.Clamp(velocityInAnimation, 0.0f, 1.0f);
        }

        animator.SetFloat("Speed", velocityInAnimation);


        //Gravedad en personaje

        if (characterController.isGrounded)
        {
            fallVelocity = -gravity * Time.deltaTime;
            moveDirection.y = fallVelocity;
        }
        else
        {
            fallVelocity -= gravity * Time.deltaTime;
            moveDirection.y = fallVelocity;
        }

        //Saltar

        if (characterController.isGrounded)
        {
            isJumping = false;
        }

        if (characterController.isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            isJumping = true;
            fallVelocity = jumpForce;
            moveDirection.y = fallVelocity;
        }

        characterController.Move(moveDirection * characterSpeed * Time.deltaTime);

    }
}
