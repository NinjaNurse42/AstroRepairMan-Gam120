using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject pausePanel; // drag your pause panel here

    private bool isPaused = false;

    void Start()
    {
        if (pausePanel != null)
            pausePanel.SetActive(false); // hide initially
    }

    void Update()
    {
        // ✅ Detect input even if Time.timeScale = 0
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    private void TogglePause()
    {
        if (pausePanel == null)
            return;

        isPaused = !isPaused; // flip the state

        pausePanel.SetActive(isPaused); // show/hide panel
        pausePanel.transform.SetAsLastSibling(); // make sure it appears on top

        // Freeze or resume the game
        Time.timeScale = isPaused ? 0f : 1f;

        // Optional: freeze player velocity when paused
        Rigidbody2D rb = FindObjectOfType<Rigidbody2D>();
        if (rb != null && isPaused)
        {
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }
    }
}