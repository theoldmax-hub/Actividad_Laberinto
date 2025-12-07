using UnityEngine;

public class footsteps : MonoBehaviour
{
    public AudioSource footstepsSound;

    private void Update()
    {
        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
        {
            footstepsSound.enabled = true;
        }
        else
        {
            footstepsSound.enabled = false;
        }
    }
}
