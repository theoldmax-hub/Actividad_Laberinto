using UnityEngine;

public class GateTrap : MonoBehaviour
{
    [Header("Gate")]
    public Transform gate;
    public float gateSpeed = 180f;
    public float rotateAmount = -180f;
    [Header("Trigger")]
    public bool oneShot = true;
    private bool active;
    private bool triggered;
    private float rotated;
    private void Awake()
    {
        if (gate == null) gate = transform;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;


        if (active)
            return;


        if (oneShot && triggered)
            return;

        // Activar
        active = true;
        triggered = true;
        rotated = 0f;

        Debug.Log("GateTrap ACTIVATED");
    }

    private void Update()
    {
        if (!active)
            return;

        float total = Mathf.Abs(rotateAmount);
        float step = gateSpeed * Time.deltaTime;


        if (rotated + step > total)
            step = total - rotated;


        float dir = Mathf.Sign(rotateAmount);


        gate.Rotate(Vector3.up, step * dir, Space.World);

        rotated += step;


        if (rotated >= total - 0.0001f)
        {
            active = false;
            Debug.Log("GateTrap FINISHED");
        }
    }
}