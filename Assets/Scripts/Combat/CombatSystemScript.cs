using UnityEngine;
using UnityEngine.InputSystem;

public class CombatSystemScript : MonoBehaviour
{
    // To-do: Detect if script is player or enemy, attack button, health and damage MAYBE: Death and animations
    private bool isPlayer = false;
    private PlayerControls Controls;
    [Header("General Configuration")]
    public int HealthPoints = 10;
    public int AttackPower = 1; 
    [Header("Player Configuration")]
    [SerializeField] PlayerInventoryRework Inventario;
    [SerializeField] GameObject Hand;
    [Header("Enemy Configuration")]
    [SerializeField] GameObject TouchAttackRadius;
    private Animator weaponAnim;
    void Start()
    {
        if(GetComponent<CharacterController>() != null)
        {
            isPlayer = true;
            Controls = new PlayerControls();
            Controls.Enable();
            weaponAnim = Hand.GetComponent<Animator>();
        }
    }

    void Update()
    {
        
        if(isPlayer == true && Inventario.CheckHand() == 1)
        {
            
            Controls.GamePlay.Attack.performed += ctx => Attack();
        }
        
    }
    private void Attack()
    {
        Debug.Log("Condition Passed");
        weaponAnim.SetBool("Attack", true);
    }
}
