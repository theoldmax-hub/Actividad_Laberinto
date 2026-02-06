using UnityEngine;

public class KeyDestroyOnOverlap : DestroyOnOverlap
{
    public GameObject keyInHandPrefab;

    protected override void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        Score score = other.GetComponent<Score>();
        score.points = score.points + 10;

        var inv = other.GetComponentInParent<PlayerInventory>();
        if (inv != null)
        {
            inv.GiveKey(keyInHandPrefab);
            
            base.OnTriggerEnter(other);
        }
    }
}
