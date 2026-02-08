//Script para coger el item del laberinto
using UnityEngine;

public class DestroyOnOverlap : MonoBehaviour
{
    public AudioSource soundEffect;
    public GameObject RawImagetoShow;
    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Score score = other.GetComponent<Score>();
            score.points = score.points + 1;

            if (RawImagetoShow != null)
            {
                RawImagetoShow.SetActive(true);
                
            }
            if (soundEffect != null)
            {
                soundEffect.Play();
            }
            Destroy(gameObject);
        }
    }
}
