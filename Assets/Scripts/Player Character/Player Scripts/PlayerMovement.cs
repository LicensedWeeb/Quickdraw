using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]

public class PlayerMovement : MonoBehaviour
{
    private Vector2 Input;
    private CharacterController characterController;
    private Vector3 direction;

    [SerializeField] private float speed;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
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


