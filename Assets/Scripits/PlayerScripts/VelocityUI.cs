using UnityEngine;
using TMPro;

public class VelocityTextUI : MonoBehaviour
{
    [Header("Required")]
    [SerializeField] private Rigidbody2D rb;
    private TextMeshProUGUI text;

    [Header("Optional Warning UI")]
    [SerializeField] private GameObject warningUI; // drag your warning text or panel here

    [Header("Settings")]
    [SerializeField] private float warningThreshold = 20f;

    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();

        if (warningUI != null)
            warningUI.SetActive(false); // hide at start
    }

    void Update()
    {
        float speed = rb != null ? rb.linearVelocity.magnitude : 0f;
        text.text = speed.ToString("F2");

        if (warningUI != null)
        {
            warningUI.SetActive(speed > warningThreshold);
        }
    }
}