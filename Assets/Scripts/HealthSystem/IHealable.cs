using System;

public interface IHealable 
{
    bool IsAlive { get; }
    void Heal(float amount, object source = null);
}
