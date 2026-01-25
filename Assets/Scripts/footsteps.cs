//Script para el sonido de andar
using UnityEngine;
using UnityEngine.InputSystem;

public class footsteps : MonoBehaviour
{
    private Vector2 moveInput;
    public AudioSource footstepsSound;

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }
    
    private void Update()
    {
        bool moving = moveInput.sqrMagnitude > 0.01f;

        if (moving)
        {
            if (!footstepsSound.isPlaying)
                footstepsSound.Play();
        }
        else
        {
            if (footstepsSound.isPlaying)
                footstepsSound.Stop();
        }
    }
}
