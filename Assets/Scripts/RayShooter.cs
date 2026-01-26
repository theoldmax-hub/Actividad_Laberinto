using NUnit;
using UnityEngine;
using UnityEngine.InputSystem;
public class OpenDoors : MonoBehaviour
{
    [Header("Raycast")]
    public float range = 2f;
    public LayerMask hitMask = ~0;
    public Transform rayOrigin;

    private PlayerInventory inv;

    private void Awake()
    {
        inv = GetComponent<PlayerInventory>();
        if (inv == null) inv = GetComponentInParent<PlayerInventory>();
    }
    void OnUse(InputValue value)
    {
        if (value.isPressed == false) return;
                
        Transform origin = rayOrigin != null ? rayOrigin : transform;
       
        //Debug.DrawRay(origin.position, origin.forward * range, Color.red, 1f);
        
        if (Physics.Raycast(origin.position, origin.forward,out RaycastHit hit, range, hitMask, QueryTriggerInteraction.Ignore))
        {
            if (hit.collider.GetComponentInParent<KeyDoor>() is KeyDoor keyDoor)
            {
                keyDoor.tryOpen(inv);
                return;
            }

            if (hit.collider.TryGetComponent<DoorSwitch>(out var sw))
            {
                Debug.Log("DoorSwitch en el MISMO objeto");
                sw.Activate();
            }
            else if (hit.collider.GetComponentInParent<DoorSwitch>() is DoorSwitch swParent)
            {
                Debug.Log("DoorSwitch en un PADRE");
                swParent.Activate();
            }
            else
            {
                Debug.Log("No hay DoorSwitch en hit ni en padres");
            }
        }
    }
}
