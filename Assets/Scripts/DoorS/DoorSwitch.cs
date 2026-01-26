using UnityEngine;

public class DoorSwitch : MonoBehaviour
{

    [Header("Door linked to this switch")]

    public Door door;
    public bool oneTimeUse = true;
    public bool used = false;


    public void Activate()
    {
        Debug.Log($"DoorSwitch.Activate() en: {name}", this);
        if (oneTimeUse && used) {
            Debug.Log("Switch ya usado (oneTimeUse=true)", this);
            return; 
        }

        if (door == null)
        {
            Debug.LogWarning("DoorSwitch sin puerta asignada", this);
            return;
        }
        used = true;
        door.Open();
    }
}
