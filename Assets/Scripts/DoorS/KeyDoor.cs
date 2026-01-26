using UnityEngine;

public class KeyDoor : MonoBehaviour
{
    public Door door;
    public bool consumeKey = true;
    public void tryOpen(PlayerInventory inv)
    {
        if (door == null || inv == null) return;

        if (!inv.hasKey)
        {
            Debug.Log("No tienes llave");
            return;
        }
        door.Open();
        if (consumeKey) inv.ConsumeKey();
    }
}
