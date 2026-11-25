using UnityEngine;

public class PlayerAnimationInput : MonoBehaviour
{
    PlayerMovement playerMovement;
    Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerMovement != null && animator != null)
        {
            animator.SetBool("isGrounded", playerMovement.isGrounded());
        }
    }
}
