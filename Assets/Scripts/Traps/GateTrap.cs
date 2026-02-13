using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class GateTrap : MonoBehaviour
{
    [Header("Gate")]
    public Transform gate;
    public float gateSpeed = 180f;
    public float rotateAmount = -180f;
    [Header("Trigger")]
    public bool oneShot = true;
    [Header("Delay")]
    public float delay = 5f;
    public TextMeshProUGUI countdown;
    private bool active;
    private bool triggered;
    private float rotated;

    [SerializeField] CharacterDeathHandle deathHandler;
    private void Awake()
    {
        if (gate == null) gate = transform;
        countdown.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;


        if (active)
            return;


        if (oneShot && triggered)
            return;

        Score score = GameObject.FindGameObjectWithTag("Player").GetComponent<Score>();
        score.RemovePoints(20);

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
            StartCoroutine(Reload());
            Debug.Log("GateTrap FINISHED");
        }
    }

    public IEnumerator Reload()
    {
        float timeLeft = delay;
        countdown.gameObject.SetActive(true);

        while (timeLeft > 0)
        {
            countdown.text = "You fall in a trap. You will respawn at: " + Mathf.Ceil(timeLeft).ToString();
            yield return new WaitForSeconds(1f);
            timeLeft--;
        }
      deathHandler.RestartGame();
      countdown.gameObject.SetActive(false);
    }
}