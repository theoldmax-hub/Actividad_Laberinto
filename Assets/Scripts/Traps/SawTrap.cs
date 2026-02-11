using System.Collections.Generic;
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

    [Header("ParticleSystem")]
    public ParticleSystem bloodFX;

    private HashSet<IDamageable> inside = new HashSet<IDamageable>();

    private void Start()
    {
        if (bloodFX != null) bloodFX.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        
        startPosition = transform.localPosition;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (TryGetDamageable(other, out IDamageable damageable))
        {
            damageable.TakeDamage(damage, source: gameObject);

            if (inside.Add(damageable) && bloodFX != null) {
                Debug.Log("damageable ADDED");
                bloodFX.Play();
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (TryGetDamageable(other,out IDamageable damageable))
        {

            //if (bloodFX != null) bloodFX.Play();
            
            damageable.TakeDamage(damage * Time.deltaTime,source: gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (TryGetDamageable(other, out IDamageable damageable))
        {
            inside.Remove(damageable);

            if (inside.Count == 0 && bloodFX != null) { 
            bloodFX.Stop(true, ParticleSystemStopBehavior.StopEmitting);
            }
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