using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class CharacterDeathHandle : MonoBehaviour
{
    [Header("References&Options")]
    public Health playerHealth;
    public GameObject deathPanel;
    public Transform respawnPoint;

    public PlayerInput playerInputToDisable;
    public bool pauseOnDeath = false;
    private bool dead;
    

    private void Awake()
    {
        if (playerHealth == null) playerHealth = GetComponent<Health>();
        if (playerInputToDisable == null) playerInputToDisable = GetComponent<PlayerInput>();
        if (deathPanel != null) deathPanel.SetActive(false);
    }

    private void OnEnable()
    {
        if (playerHealth != null) playerHealth.OnDied += HandleDied;
    }

    private void OnDisable()
    {
        if (playerHealth != null) playerHealth.OnDied -= HandleDied;
    }

    private void HandleDied(object source)
    {
        if (dead) return;
        dead = true;

        var movement = GetComponent<NI_PlayerMovement>();
        if (movement != null) movement.allowCursorLock = false;

        if (deathPanel != null) deathPanel.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if(playerInputToDisable != null) playerInputToDisable.enabled = false;

        if (pauseOnDeath) Time.timeScale = 0f;

        
    }

    public void RestartGame()
    {
        if (respawnPoint == null) return;

        if (deathPanel != null) deathPanel.SetActive(false);

        var movement = GetComponent<NI_PlayerMovement>();
        if (movement != null) movement.allowCursorLock = true;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (playerInputToDisable != null) playerInputToDisable.enabled = true;

        var cc = GetComponent<CharacterController>();
        if (cc != null) cc.enabled = false;

        transform.position = respawnPoint.position;
        transform.rotation = respawnPoint.rotation;

        if (cc != null) cc.enabled = true;

        if (playerHealth != null) playerHealth.ResetToFull();

        dead = false;
        Time.timeScale = 1f;
    }

    public void QuitGame()
    {
        Time.timeScale = 1f;

        // corta input gameplay para que no se disparen callbacks al cerrar
        if (playerInputToDisable != null)
            playerInputToDisable.enabled = false;

        // desactiva scripts que reaccionen al input
        var combat = GetComponent<CombatSystemScript>();
        if (combat != null) combat.enabled = false;

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else

Application.Quit();

#endif
    }
}
