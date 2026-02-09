using System.Collections;
using UnityEngine;

public class AttackEnemyRange : MonoBehaviour
{
    GameObject EnemyParent;
    CombatSystemScript EnemyInformation;
    [SerializeField] float KnockbackForce = 10f;
    private bool canAttack = true;
    private void Start()
    {
        EnemyParent = transform.parent.gameObject;
        EnemyInformation = EnemyParent.GetComponent<CombatSystemScript>();
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && TryGetDamageable(other, out IDamageable damageable) && canAttack)
        {
            canAttack = false;
            Rigidbody knockback = other.GetComponent<Rigidbody>();
            damageable.TakeDamage((float)EnemyInformation.AttackPower, EnemyParent);
            if (knockback != null)
            {
                knockback.AddForce(EnemyParent.transform.forward * KnockbackForce, ForceMode.Impulse);
            }
            StartCoroutine(AttackCooldown(1));
        }
        
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
    IEnumerator AttackCooldown(float Seconds)
    {
        yield return new WaitForSeconds(Seconds);
        canAttack = true;
    }
}
