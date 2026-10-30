using System;
using System.Collections;
using System.Threading;
using Unity.Hierarchy;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

[RequireComponent(typeof(CharacterController))]

public class PlayerMovement : MonoBehaviour
{
    //other scripts
    private CameraManager cameraManager;

    [Header("Bullshit")]
    private Vector2 Input;
    private CharacterController characterController;
    private Vector3 direction;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float gravityMultiplier = 2f;
    [SerializeField] private float velocity = 0f;
    [SerializeField] private float jumpPower = 2f;
    private int numberofJumps = 0;
    [SerializeField] private int maxJumps = 2;

    [SerializeField] private float speed = 5f;

    [SerializeField] private Movement movement;


    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        cameraManager = GameObject.Find("CameraManager").GetComponent<CameraManager>();

        cameraManager.AddPlayer(gameObject);
    }
    private void Update()
    {
        
        applyGravity();
        applyMovement();
    }


    public void Move(InputAction.CallbackContext context)
    {
        Input = context.ReadValue <Vector2>();
        direction = new Vector3(Input.x, y: 0.0f, Input.y);

    }

    private void applyMovement()
    {
        var targetSpeed = movement.isSprinting ? movement.speed * movement.multiplier : movement.speed;
        movement.currentSpeed = Mathf.MoveTowards(movement.currentSpeed, targetSpeed, movement.acceleration * Time.deltaTime);
        
        characterController.Move(direction * movement.currentSpeed * Time.deltaTime);
    }

    private void applyGravity()
    {
        if(isGrounded() && velocity < 0f)
        {
            velocity = -1f;
        }
        else
        {

            velocity += gravity * gravityMultiplier * Time.deltaTime;
        }
        direction.y = velocity;

    }

    public void jump(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        if (!isGrounded() && numberofJumps >= maxJumps) return;
        if (numberofJumps == 0)
        {
            StartCoroutine(resetJumps());
        }

        numberofJumps++;
      
        velocity = jumpPower;
       
    }

    public void sprint(InputAction.CallbackContext context)
    {
        movement.isSprinting = context.started || context.performed;
    }

    private IEnumerator resetJumps()
    {
        yield return new WaitUntil(() => !isGrounded());
        yield return new WaitUntil(() => isGrounded());

        numberofJumps = 0;
    }

    private bool isGrounded() => characterController.isGrounded;

    [Serializable]
    public struct Movement
    {
        public float speed;
        public float multiplier;
        public float acceleration;

        public bool isSprinting;
        public float currentSpeed;

    }

}


