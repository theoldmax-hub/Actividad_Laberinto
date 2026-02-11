using UnityEngine;

public class Health : MonoBehaviour, IHealth, IHealable, IDamageable
{
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float currentHealth;

    public float Current => currentHealth;
    public float Max => maxHealth;
    public bool isAlive => currentHealth > 0;

    public event System.Action<float, float> OnHealthChanged;
    public event System.Action<object> OnDied;

    private void Awake()
    {
        currentHealth = Mathf.Clamp(
            currentHealth <= 0 ? maxHealth : currentHealth, 0, maxHealth
        );

        OnHealthChanged?.Invoke(currentHealth, maxHealth);
    }

   public void TakeDamage (float amount, object source = null)
    {
        if (!isAlive) return;
        if (amount <= 0) return;
        Score score = GameObject.FindGameObjectWithTag("Player").GetComponent<Score>();
        score.RemovePoints(1);

        currentHealth = Mathf.Max(0f, currentHealth - amount);
        OnHealthChanged?.Invoke(currentHealth, maxHealth);

        if (currentHealth <= 0) OnDied?.Invoke(source);
        
    }

    public void Heal(float amount, object source = null)
    {
        if (!isAlive) return;
        if (amount <= 0f) return;

        currentHealth = Mathf.Min(maxHealth, currentHealth +  amount);
        OnHealthChanged?.Invoke(currentHealth, maxHealth);
    }
    public bool CompareHealth() { if (currentHealth == maxHealth) return true; else return false; }
}
