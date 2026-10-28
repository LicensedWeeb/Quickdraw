using System.Threading;
using Unity.Hierarchy;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]

public class PlayerMovement : MonoBehaviour
{
    //other scripts
    private CameraManager cameraManager;

    [Header("Bullshit")]
    private Vector2 Input;
    private CharacterController characterController;
    private Vector3 direction;
    private Vector3 velocity;
    private float verticalVelocity;
    private bool isSprinting;
    private bool jumpPressed;

    [Header("PlayerMovementSettings")]
    [SerializeField] private float minSpeed = 0f;
    [SerializeField] private float maxSpeed = 10f;
    [SerializeField] private float acceleration = 5f;
    [SerializeField] private float deceleration = 2f;
    [SerializeField] private float jumpHeight = 0f;
    [SerializeField] private float gravity = -9.81f;

    [SerializeField] private float speed;




    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        cameraManager = GameObject.Find("CameraManager").GetComponent<CameraManager>();

        cameraManager.AddPlayer(gameObject);
    }

    //Performed by Unity Input System
    public void onMove(InputAction.CallbackContext context)
    {
        Input = context.ReadValue<Vector2>();
    }

    public void onSprint(InputAction.CallbackContext context)
    {
        isSprinting = context.performed;
    }

    public void onJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            jumpPressed = true;
        }
    }
    private void Update()
    {
        characterController.Move(direction * speed * Time.deltaTime);
        handleMovement();
    }


    public void Move(InputAction.CallbackContext context)
    {
        Input = context.ReadValue <Vector2>();
        direction = new Vector3(Input.x, y: 0.0f, Input.y);

    }

    private void handleMovement()
    {
        float targetSpeed = isSprinting ? minSpeed : maxSpeed;
        Vector3 inputDirection = (transform.forward * Input.y + transform.right * Input.x);

        //Accelerartion/Deceleration

        Vector3 targetVelocity = inputDirection * targetSpeed;
        velocity = Vector3.MoveTowards(velocity, targetVelocity, (inputDirection.magnitude > 0 ? acceleration : deceleration) * Time.deltaTime);

        //apply gravity

        if (characterController.isGrounded)
        {
            verticalVelocity = -2f; //small force to keep player grounded
            if (jumpPressed)
            {
                verticalVelocity = jumpHeight;
                jumpPressed = false;
            }
        }
        else
        {
            verticalVelocity += gravity * Time.deltaTime;
        }

        //Combine horizontal and vertical movement

        Vector3 move = velocity + Vector3.up * verticalVelocity;
        characterController.Move(move * Time.deltaTime);
    }


}


