using UnityEngine;

public class CombatSystemScriptEnemy : MonoBehaviour
{
    // To-do: Detect if script is player or enemy, attack button, health and damage MAYBE: Death and animations
    private bool isPlayer = false;
    private PlayerControls Controls;
    [Header("General Configuration")]
    public int HealthPoints = 10;
    public int AttackPower = 1; 
    [Header("Enemy Configuration")]
    [SerializeField] GameObject TouchAttackRadius;
    void Start()
    {
    }

    void Update()
    {
    }
}
