//Script para coger el mapa del laberinto y que lo muestre en el canvas
using UnityEngine;

public class DestroyOnOverlap : MonoBehaviour
{
    public AudioSource SoundEffect;
    public GameObject RawImagetoShow;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(RawImagetoShow != null)
            {
                RawImagetoShow.SetActive(true);
                
            }

            Destroy(gameObject);
            SoundEffect.enabled = true;
        }
    }
}
