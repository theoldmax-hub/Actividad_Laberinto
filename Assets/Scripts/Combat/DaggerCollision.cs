using UnityEngine;

public class DaggerCollision : MonoBehaviour
{
    [SerializeField] CombatSystemScript PlayerInformation;
    [SerializeField] GameObject Player;
    [SerializeField] float KnockbackForce = 10f;
    [SerializeField] Animator animatora;
    Animator animator;
    private void Start()
    {
        animator = animatora.GetComponent<Animator>();
    }
    private void OnTriggerEnter(Collider other)
    {
        /*    if (other.transform.parent.CompareTag("Enemy") && animator.GetBool("Attack"))
            {


                int EnemyHealth = other.GetComponent<CombatSystemScript>().HealthPoints;
                EnemyHealth -= PlayerInformation.AttackPower;
                other.GetComponent<CombatSystemScript>().HealthPoints = EnemyHealth;

                Debug.Log("Ouch!");
                Rigidbody knockback = other.GetComponent<Rigidbody>();
                if (TryGetDamageable(other, out IDamageable damageable))
                damageable.TakeDamage((float)PlayerInformation.AttackPower,Player);
                if(knockback != null)
                {
                    knockback.AddForce(Player.transform.forward * KnockbackForce, ForceMode.Impulse);
                }

        }
        */
        if (TryGetDamageable(other, out IDamageable damageable) && animator.GetBool("Attack"))
        {
            Debug.Log("Ouch!");
            Rigidbody knockback = other.GetComponent<Rigidbody>();
            damageable.TakeDamage((float)PlayerInformation.AttackPower, Player);
            if (knockback != null)
            {
                knockback.AddForce(Player.transform.forward * KnockbackForce, ForceMode.Impulse);
            }
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