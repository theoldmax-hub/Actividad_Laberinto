using System;
public interface IDamageable
{
    bool isAlive {  get; }
    void TakeDamage(float amount, object source = null);
}
