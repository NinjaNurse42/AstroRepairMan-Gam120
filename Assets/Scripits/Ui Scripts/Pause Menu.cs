using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject pausePanel; // Drag your pause menu UI here

    private bool isPaused = false;

    void Start()
    {
        if (pausePanel != null)
            pausePanel.SetActive(false);
    }

    void Update()
    {
        // Toggle pause on Escape key
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    private void PauseGame()
    {
        isPaused = true;

        // Show the UI
        if (pausePanel != null)
            pausePanel.SetActive(true);

        // Stop all in-game time
        Time.timeScale = 0f;

        // Optional: freeze input/momentum by zeroing Rigidbody velocity
        FreezePlayer();
    }

    private void ResumeGame()
    {
        isPaused = false;

        if (pausePanel != null)
            pausePanel.SetActive(false);

        Time.timeScale = 1f;
    }

    private void FreezePlayer()
    {
        // Find player and set velocity to zero
        Rigidbody2D rb2d = FindObjectOfType<Rigidbody2D>();
        if (rb2d != null)
        {
            rb2d.linearVelocity = Vector2.zero;
            rb2d.angularVelocity = 0f;
        }

        // Optional: also reset input states in your player script if needed
        PlayerMovement pm = FindObjectOfType<PlayerMovement>();
        if (pm != null)
        {
            pm.ResetInput(); // You can implement ResetInput() in your PlayerMovement script
        }
    }
}