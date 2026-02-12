using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class CharacterDeathHandle : MonoBehaviour
{
    [Header("References&Options")]
    public Health playerHealth;
    public GameObject deathPanel;

    public PlayerInput playerInputToDisable;
    public bool pauseOnDeath = false;
    private bool dead;
    

    private void Awake()
    {
        if (playerHealth == null) playerHealth = GetComponent<Health>();
        if (playerInputToDisable == null) playerInputToDisable = GetComponent<PlayerInput>(); 
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
        Debug.Log("Restart pressed");
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
