using UnityEngine;
using UnityEngine.SceneManagement;

public class SawTrap : MonoBehaviour
{
    [Header("Rotation")]
    public float rotationSpeed = 360f;

    [Header("Movement")]
    public float moveDistance = 2.3f;
    public float moveSpeed = 1f;

    private Vector3 startPosition;

    private void Start()
    {

        startPosition = transform.localPosition;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            PlayerInventoryRework.CurrentInventory1 = 0;
            PlayerInventoryRework.CurrentInventory2 = 0;
            PlayerInventoryRework.CurrentInventory3 = 0;
        }
    }
    private void Update()
    {
        RotateSaw();
        MoveSaw();
    }

    private void RotateSaw()
    {
        transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime, Space.Self);
    }
    private void MoveSaw()
    {
        float xOffset = Mathf.PingPong(Time.time * moveSpeed, moveDistance);

        transform.localPosition = new Vector3(startPosition.x + xOffset, startPosition.y, startPosition.z);
    }
}