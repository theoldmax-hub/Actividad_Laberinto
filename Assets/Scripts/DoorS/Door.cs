using UnityEngine;

public class Door : MonoBehaviour
{
    public AudioSource doorSound;

    public Transform doorTransform;

    [Header("Rotation")]

    public float openYAngle = 195f;
    public float speed = 2f;

    Score score;

    private Quaternion closedRotation;
    private Quaternion openRotation;

    public bool isOpen {  get; private set; }

    private void Awake()
    {
        if (doorTransform == null) doorTransform = transform;

        closedRotation = doorTransform.localRotation;

        Vector3 openEuler = closedRotation.eulerAngles;
        openEuler.y = openYAngle;
        openRotation = Quaternion.Euler(openEuler);
    }
    private void Update()
    {
        Quaternion target = isOpen ? openRotation : closedRotation;

        doorTransform.localRotation = Quaternion.Lerp(doorTransform.localRotation, target, speed * Time.deltaTime);
    }

    public void Open()
    {
        Debug.Log("Door.Open() llamado en " + name);
        isOpen = true;
        score = GameObject.FindGameObjectWithTag("Player").GetComponent<Score>();
        score.AddPoints(1);
        doorSound.Play();
    }
    public void Close() => isOpen = false;
}
