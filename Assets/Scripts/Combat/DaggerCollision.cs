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
        if (other.CompareTag("Enemy") && animator.GetBool("Attack"))
        {
            Debug.Log("Ouch!");
            int EnemyHealth = other.GetComponent<CombatSystemScript>().HealthPoints;
            EnemyHealth -= PlayerInformation.AttackPower;
            other.GetComponent<CombatSystemScript>().HealthPoints = EnemyHealth;
            Rigidbody knockback = other.GetComponent<Rigidbody>();
            if(knockback != null)
            {
                knockback.AddForce(Player.transform.forward * KnockbackForce, ForceMode.Impulse);
            }

        }
    }
}