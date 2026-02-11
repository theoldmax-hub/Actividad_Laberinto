using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

[RequireComponent(typeof(CharacterController))]
public class NI_PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 3f;

    private float gravity = -9.81f;
    public float jumpHeight = 0.5f;

    public bool cameraRelative = true;

    private CharacterController characterController;

    private Vector2 moveInput;

    private float verticalVelocity;

    [Header("Look")]
    public float mouseSensitivity = 0.1f;

    public Transform cameraPivot;

    public float minPitch = -80f;
    public float maxPitch = 80f;

    private Vector2 lookInput;
    private float pitch;


    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void OnJump(InputValue value)
    {
        if (!value.isPressed) return;
        
        if (characterController.isGrounded)
        {
            verticalVelocity = Mathf.Sqrt(2f * jumpHeight * -gravity);
        }

    }

    private void OnLook(InputValue value)
    {
        lookInput = value.Get<Vector2>();
       
    }

    void Update()
    {
        
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        ApplyGroundingAndGravity();
        MoveCharacter();
        ApplyLook();
    }

    private void ApplyGroundingAndGravity()
    {
        if (characterController.isGrounded && verticalVelocity < 0) verticalVelocity = -2f;
        
        verticalVelocity += gravity * Time.deltaTime;
    }

    private void MoveCharacter()
    {
        Vector3 move = new Vector3(moveInput.x, 0f, moveInput.y);

        if (move.sqrMagnitude > 1f) move.Normalize();

        if(cameraRelative && Camera.main != null)
        {
            Transform cam = Camera.main.transform;

            Vector3 camForward = cam.forward;
            Vector3 camRight = cam.right;

            camForward.y = 0f;
            camRight.y = 0f;

            camForward.Normalize();
            camRight.Normalize();

            move = camRight * move.x + camForward * move.z;
        }

        Vector3 velocity = move * moveSpeed;
        velocity.y = verticalVelocity;

        characterController.Move(velocity *  Time.deltaTime);
    }
    private void ApplyLook()
    {
        float yaw = lookInput.x * mouseSensitivity;

        transform.Rotate(0f, yaw, 0f);

        if (cameraPivot != null)
        {
            pitch -= lookInput.y * mouseSensitivity;
            pitch = Mathf.Clamp(pitch, minPitch, maxPitch);
            cameraPivot.localRotation = Quaternion.Euler(pitch, 0f, 0f);
        }
    }
    public float GetVerticalVelocity()
    {
        return verticalVelocity;
    }

    public bool GetIsGrounded()
    {
        return characterController.isGrounded;
    }
}