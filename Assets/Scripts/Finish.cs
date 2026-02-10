using TMPro;
using UnityEngine;

public class Finish : MonoBehaviour
{
    public GameObject Victory;
    public AudioSource finishSound;
    private void Start()
    {
        finishSound.enabled = false;
        Victory.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        finishSound.enabled = true;
        Victory.SetActive(true);
        Score score = GameObject.FindGameObjectWithTag("Player").GetComponent<Score>();
        score.AddPoints(1);
    }
}
