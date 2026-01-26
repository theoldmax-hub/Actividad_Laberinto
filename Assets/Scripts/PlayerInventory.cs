using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public Transform handPoint;
    public GameObject heldKey;

    public bool hasKey { get; private set; }

    public void GiveKey(GameObject keyPrefab)
    {
        if (hasKey) return;

        hasKey = true;

        if (handPoint != null && keyPrefab != null)
        {
            heldKey = Instantiate(keyPrefab, handPoint);
            heldKey.transform.localPosition = Vector3.zero;
            heldKey.transform.localRotation = Quaternion.identity;
        }

    }
    public void ConsumeKey()
    {
        hasKey = false;

        if (heldKey != null)
            Destroy(heldKey);

        heldKey = null;
    }
}
