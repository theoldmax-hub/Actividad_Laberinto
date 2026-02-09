using UnityEngine;

public class DaggerCollision : MonoBehaviour
{
    [SerializeField] CombatSystemScript PlayerInformation;
    [SerializeField] GameObject Player;
    [SerializeField] float KnockbackForce = 10f;
    [SerializeField] Animator animatora;
    Animator animator;
    public bool canDamage = true;
    private void Start()
    {
        animator = animatora.GetComponent<Animator>();
    }
 
    private void OnTriggerStay(Collider other)
    {
        if (TryGetDamageable(other, out IDamageable damageable) && animator.GetBool("Attack") && canDamage && other.gameObject.CompareTag("Enemy"))
        {
                Debug.Log("Ouch!");
                canDamage = false;
                Rigidbody knockback = other.GetComponent<Rigidbody>();
                damageable.TakeDamage((float)PlayerInformation.AttackPower, Player);
                if (knockback != null)
                {
                    knockback.AddForce(Player.transform.forward * KnockbackForce, ForceMode.Impulse);
                }

        }

    }

    private void Update()
    {
        if (!animator.GetBool("Attack"))
        {
            canDamage = true;
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
}