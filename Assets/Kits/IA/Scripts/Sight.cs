using UnityEngine;
using UnityEngine.Audio;

public class Sight : MonoBehaviour
{
    [SerializeField] float radius = 10f;
    [SerializeField] float checksPerSecond = 5;

    [SerializeField] LayerMask layerMask = Physics.DefaultRaycastLayers;

    Transform player;
    float lastCheckTime = 0f;


    AudioSource audioSource;
    bool playerWasInSight = false;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        if ((Time.time - lastCheckTime) > (1f/ checksPerSecond))  {

            Debug.Log("Checking sight");
            lastCheckTime = Time.time;

            // Hago el checkeo
            player = null;

            Collider [] colliders = Physics.OverlapSphere(transform.position, radius, layerMask);


            foreach (Collider c in colliders) {

                if (c.CompareTag("Player"))
                {
                    
                    Vector3 direction = c.transform.position - transform.position;
                    if (Physics.Raycast(transform.position, direction, out RaycastHit hit))
                    {
                        if (hit.collider == c)
                        {
                            player = c.transform;
                            Debug.Log("Player found", player);
                        }
                    }
                    
                }
                
            }

            //Detecta si el jugador está dentro de su area
            if (player != null && !playerWasInSight)
            {
                audioSource.Play();
                Debug.Log("Player found sound played");
            }

            playerWasInSight = (player != null);
        }
    }


    public Transform GetPlayerInSight() { return player; }
}
