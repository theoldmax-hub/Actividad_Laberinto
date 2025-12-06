using UnityEngine;

public class MapMeshMovement : MonoBehaviour
{
    public float rotaionSpeed = 30F;
    private float initialRotationY = 0;
    public float runningTime;
    public float amplitude = 0.3f;
    public float sin;
    private Vector3 initialPosition;
    private Vector3 newPosition;
    
    void Start()
    {
      initialPosition = transform.position;  
    }

    // Update is called once per frame
    void Update()
    {
        runningTime += Time.deltaTime;
        sin = amplitude * Mathf.Sin(runningTime);
        newPosition = new Vector3(0f, sin, 0f);
        transform.position = initialPosition + newPosition;

        transform.Rotate(0f, initialRotationY + rotaionSpeed * Time.deltaTime, 0f);
    }
}
