using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [Header("Main UI")]
    [SerializeField] private GameObject pausePanel;

    [Header("Sub Menus")]
    [SerializeField] private GameObject[] subMenus;
    // drag Controls, Credits, Quit Confirm panels here

    private bool isPaused = false;

    void Start()
    {
        if (pausePanel != null)
            pausePanel.SetActive(false);

        CloseAllSubMenus();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // If any submenu is open → close it FIRST
            if (IsAnySubMenuOpen())
            {
                CloseAllSubMenus();
                return;
            }

            // Otherwise toggle pause
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    void PauseGame()
    {
        if (pausePanel == null) return;

        isPaused = true;

        pausePanel.SetActive(true);
        pausePanel.transform.SetAsLastSibling();

        Time.timeScale = 0f;

        Rigidbody2D rb = FindObjectOfType<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }
    }

    void ResumeGame()
    {
        if (pausePanel == null) return;

        isPaused = false;

        pausePanel.SetActive(false);

        // 🔥 ALSO close any leftover submenus
        CloseAllSubMenus();

        Time.timeScale = 1f;
    }

    void CloseAllSubMenus()
    {
        foreach (GameObject menu in subMenus)
        {
            if (menu != null)
                menu.SetActive(false);
        }
    }

    bool IsAnySubMenuOpen()
    {
        foreach (GameObject menu in subMenus)
        {
            if (menu != null && menu.activeSelf)
                return true;
        }
        return false;
    }
}