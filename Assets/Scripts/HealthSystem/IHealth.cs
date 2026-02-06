using System;


public interface IHealth
{
    float Current {  get; }
    float Max { get; }

    event Action <float, float> OnHealthChanged;
    event Action<object> OnDied;
}
