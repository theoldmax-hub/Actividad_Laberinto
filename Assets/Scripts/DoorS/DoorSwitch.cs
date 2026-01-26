using UnityEngine;

public class DoorSwitch : MonoBehaviour
{
    [Header("Stick / Lever")]
    public Transform stickPivot;
    public float stickTargetZ = -80f;
    public float stickSpeed = 6f;
    private Quaternion stickStartRot;
    private Quaternion stickTargetRot;
    private bool rotateStick;
    [Header("Door linked to this switch")]
    
    public Door door;
    public bool oneTimeUse = true;
    public bool used = false;

    private void Awake()
    {
        if(stickPivot != null)
        {
            stickStartRot = stickPivot.localRotation;

            Vector3 euler = stickStartRot.eulerAngles;
            euler.z = stickTargetZ;
            stickTargetRot = Quaternion.Euler(euler);
        }
    }

    private void Update()
    {
        if (!rotateStick || stickPivot == null) return;

        stickPivot.localRotation = Quaternion.Lerp(stickPivot.localRotation, stickTargetRot, stickSpeed * Time.deltaTime);
    }
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
        if (stickPivot != null)
            rotateStick = true;
    }
}
