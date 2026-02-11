using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;

    public float walkValue = 0.5f;
    public float runValue = 1f;
    

    private CharacterController characterController;
    private NI_PlayerMovement movement;

    void Awake()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        movement = GetComponent<NI_PlayerMovement>();
    }

    void Update()
    {
        float move = 0f;

        if (Keyboard.current.wKey.isPressed ||
            Keyboard.current.aKey.isPressed ||
            Keyboard.current.sKey.isPressed ||
            Keyboard.current.dKey.isPressed)
        {
            move = walkValue;


            if (Keyboard.current.leftShiftKey.isPressed)
            {
                move = runValue;
            }
        }

        animator.SetFloat("Forward", move);

        animator.SetFloat("verticalVelocity", movement.GetVerticalVelocity());
        animator.SetBool("IsGrounded", movement.GetIsGrounded());

    }
}


