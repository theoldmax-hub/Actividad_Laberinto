using UnityEngine;

public class crickets : MonoBehaviour
{
    public Collider Area;
    public GameObject Player;
    public Vector3 closestPoint;
    // Update is called once per frame
    void Update()
    {
       closestPoint = Area.ClosestPoint(Player.transform.position);

        transform.position = closestPoint;
    }
}
