using UnityEngine;
using UnityEngine.SceneManagement;

public class SawTrap : MonoBehaviour
{
    [Header("Damage")]
    public float damage = 10f;
    
    [Header("Rotation")]
    public float rotationSpeed = 360f;

    [Header("Movement")]
    public float moveDistance = 2.3f;
    public float moveSpeed = 1f;

    private Vector3 startPosition;

    private void Start()
    {

        startPosition = transform.localPosition;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (TryGetDamageable(other, out IDamageable damageable))
        {
            damageable.TakeDamage(damage,source: gameObject);  
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (TryGetDamageable(other,out IDamageable damageable))
        {
            damageable.TakeDamage(damage * Time.deltaTime,source: gameObject);
        }
    }

    private void Update()
    {
        RotateSaw();
        MoveSaw();
    }

    private void RotateSaw()
    {
        transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime, Space.Self);
    }
    private void MoveSaw()
    {
        float xOffset = Mathf.PingPong(Time.time * moveSpeed, moveDistance);

        transform.localPosition = new Vector3(startPosition.x + xOffset, startPosition.y, startPosition.z);
    }

    private bool TryGetDamageable(Collider col, out IDamageable damageable)
    {
        foreach (var mb in col.GetComponentsInParent<MonoBehaviour>())
        {
            if (mb is IDamageable d)
            {
                damageable = d;
                return true;
            }

        }
        damageable = null;
       return false;
    }

}