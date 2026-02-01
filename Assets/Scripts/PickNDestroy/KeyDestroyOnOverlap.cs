using UnityEngine;

public class KeyDestroyOnOverlap : DestroyOnOverlap
{
    public GameObject keyInHandPrefab;

    protected override void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        var inv = other.GetComponentInParent<PlayerInventory>();
        if (inv != null)
        {

                inv.GiveKey(keyInHandPrefab);
            
            base.OnTriggerEnter(other);
        }
    }
}
