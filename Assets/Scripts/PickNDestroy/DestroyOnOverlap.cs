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
            if(RawImagetoShow != null)
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
