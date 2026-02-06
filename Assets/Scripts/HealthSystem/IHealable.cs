using System;

public interface IHealable 
{
    bool isAlive { get; }
    void Heal(float amount, object source = null);
}
