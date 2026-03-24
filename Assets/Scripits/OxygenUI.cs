using UnityEngine;
using UnityEngine.UI;

public class OxygenUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerOxygen playerOxygen;
    [SerializeField] private Slider oxygenSlider;
    [SerializeField] private Image fillImage;

    [Header("Warning Panel")]
    [SerializeField] private GameObject warningPanel; // drag your warning UI here

    private float lastOxygen = 1f;

    // Threshold in 0-1 percent per second scale
    private readonly float abnormalRateThreshold = 0.004f; // triggers at ~0.4 units/sec

    void Start()
    {
        if (playerOxygen != null)
            lastOxygen = playerOxygen.GetOxygenPercent();

        if (warningPanel != null)
            warningPanel.SetActive(false);
    }

    void Update()
    {
        if (playerOxygen == null) return;

        float currentOxygen = playerOxygen.GetOxygenPercent(); // 0-1
        float deltaOxygen = lastOxygen - currentOxygen;
        float ratePerSecond = Mathf.Max(0f, deltaOxygen / Time.deltaTime); // only care about depletion

        // update slider
        oxygenSlider.value = currentOxygen;

        // update fill color
        if (currentOxygen > 0.5f)
            fillImage.color = Color.cyan;
        else if (currentOxygen > 0.2f)
            fillImage.color = Color.yellow;
        else
            fillImage.color = Color.red;

        // Show panel if rate is above threshold
        if (warningPanel != null)
            warningPanel.SetActive(ratePerSecond > abnormalRateThreshold);

        lastOxygen = currentOxygen;
    }
}