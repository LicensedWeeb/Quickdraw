using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]

public class PlayerMovement : MonoBehaviour
{
    //other scripts
    private CameraManager cameraManager;

    [Header("Movement")]
    private Vector2 Input;
    private CharacterController characterController;
    private Vector3 direction;

    [SerializeField] private float speed;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        cameraManager = GameObject.Find("CameraManager").GetComponent<CameraManager>();

        cameraManager.players.Add(gameObject);
    }

    private void Update()
    {
        characterController.Move(direction * speed * Time.deltaTime);
    }


    public void Move(InputAction.CallbackContext context)
    {
        Input = context.ReadValue <Vector2>();
        direction = new Vector3(Input.x, y: 0.0f, Input.y);

    }


}


