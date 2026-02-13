using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour, IHealth, IHealable, IDamageable
{
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float currentHealth;

    public float Current => currentHealth;
    public float Max => maxHealth;
    public bool isAlive => currentHealth > 0;

    public event System.Action<float, float> OnHealthChanged;
    public event System.Action<object> OnDied;

    private bool receivedDamage;

    private void Awake()
    {
        currentHealth = Mathf.Clamp(
            currentHealth <= 0 ? maxHealth : currentHealth, 0, maxHealth
        );

        OnHealthChanged?.Invoke(currentHealth, maxHealth);
        receivedDamage = true;
    }

   public void TakeDamage (float amount, object source = null)
    {
        if (!isAlive) return;
        if (amount <= 0) return;

        currentHealth = Mathf.Max(0f, currentHealth - amount);
        OnHealthChanged?.Invoke(currentHealth, maxHealth);

        if (currentHealth <= 0) OnDied?.Invoke(source);
        
        Health health = GetComponent<Health>();

        if (health.tag == "Player" && receivedDamage)
        {
            receivedDamage = false;
            Score score = GameObject.FindGameObjectWithTag("Player").GetComponent<Score>();
            score.RemovePoints(1);
            StartCoroutine(DamageCooldown(1));
        }

        if (health.tag == "Enemy")
        {
            Score score = GameObject.FindGameObjectWithTag("Player").GetComponent<Score>();
            score.AddPoints(1);
        }
    }

    public void Heal(float amount, object source = null)
    {
        if (!isAlive) return;
        if (amount <= 0f) return;

        currentHealth = Mathf.Min(maxHealth, currentHealth +  amount);
        OnHealthChanged?.Invoke(currentHealth, maxHealth);
    }
    public bool CompareHealth() { if (currentHealth == maxHealth) return true; else return false; }

    IEnumerator DamageCooldown(float sec)
    {
        yield return new WaitForSeconds(sec);
        receivedDamage = true;
    }
    public void ResetToFull()
    {
        currentHealth = maxHealth;
        OnHealthChanged?.Invoke(currentHealth, maxHealth);
    }
}
