using UnityEngine;

public class MapMeshMovement : MonoBehaviour
{
    public float constantRotation = 0.5f;
    private float y = 0;
    public float runningTime;
    public float amplitude = 0.008f;
    public float sin;
    private Vector3 newPosition;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        runningTime += Time.deltaTime;
        sin = amplitude * Mathf.Sin(runningTime);
        newPosition = new Vector3(0f, sin, 0f);
        transform.position += newPosition;

        transform.Rotate(0f, y + constantRotation, 0f);
    }
}
