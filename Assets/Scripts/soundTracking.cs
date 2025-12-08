//Script para que los sonidos del ambiente siguen al jugador dentro del box collider
using UnityEngine;

public class crickets : MonoBehaviour
{
    public Collider Area;
    public GameObject Player;
    public Vector3 closestPoint;
    
    
    void Update()
    {
       closestPoint = Area.ClosestPoint(Player.transform.position);

        transform.position = closestPoint;
    }
}
