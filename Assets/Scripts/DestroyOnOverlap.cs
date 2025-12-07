using UnityEngine;

public class DestroyOnOverlap : MonoBehaviour
{
    public AudioSource Paper;
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
            Paper.enabled = true;
        }
    }
}
