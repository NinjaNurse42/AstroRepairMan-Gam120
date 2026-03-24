using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonAction : MonoBehaviour
{
    public enum ActionType
    {
        Resume,
        Quit,
        LoadScene
    }

    [Header("Choose what this button does")]
    public ActionType action = ActionType.Resume;

    [Header("If LoadScene is selected, put the scene name here")]
    public string sceneName;

    [Header("Optional: pause panel to hide on Resume")]
    public GameObject pausePanel;

    /// <summary>
    /// Call this from the button's OnClick() in the inspector
    /// </summary>
    public void PerformAction()
    {
        switch (action)
        {
            case ActionType.Resume:
                // Unpause time
                Time.timeScale = 1f;

                // Hide pause panel if assigned
                if (pausePanel != null)
                    pausePanel.SetActive(false);
                break;

            case ActionType.Quit:
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#else
                    Application.Quit();
#endif
                break;

            case ActionType.LoadScene:
                if (!string.IsNullOrEmpty(sceneName))
                {
                    Time.timeScale = 1f; // make sure timescale is normal
                    SceneManager.LoadScene(sceneName);
                }
                else
                {
                    Debug.LogWarning("ButtonAction: sceneName is empty!");
                }
                break;
        }
    }
}