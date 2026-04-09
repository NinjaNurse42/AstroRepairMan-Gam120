using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class WinCondition : MonoBehaviour
{
    [Header("Fade Canvas (black full screen UI)")]
    public Canvas fadeCanvas;

    public float fadeDuration = 1.5f;

    private CanvasGroup canvasGroup;
    private bool hasWon = false;

    void Start()
    {
        if (fadeCanvas != null)
        {
            fadeCanvas.gameObject.SetActive(false);

            canvasGroup = fadeCanvas.GetComponent<CanvasGroup>();
            if (canvasGroup == null)
                canvasGroup = fadeCanvas.gameObject.AddComponent<CanvasGroup>();

            canvasGroup.alpha = 0f;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (hasWon) return;

        if (!other.CompareTag("Player")) return;

        hasWon = true;
        StartCoroutine(WinSequence());
    }

    IEnumerator WinSequence()
    {
        fadeCanvas.gameObject.SetActive(true);

        yield return StartCoroutine(FadeToBlack());

        SceneManager.LoadScene("WinScreen");
    }

    IEnumerator FadeToBlack()
    {
        float t = 0f;

        canvasGroup.alpha = 0f;

        while (t < fadeDuration)
        {
            t += Time.unscaledDeltaTime;

            canvasGroup.alpha = Mathf.Lerp(0f, 1f, t / fadeDuration);

            yield return null;
        }

        canvasGroup.alpha = 1f;
    }
}