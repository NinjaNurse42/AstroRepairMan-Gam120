using UnityEngine;

public class PlayerOxygen : MonoBehaviour
{
     float maxOxygen = 100f;
    [SerializeField] float depletionRateOutside = 0.5f; // oxygen lost per second when not in zone
    [SerializeField] float regenRateInside = 15f; // oxygen restored per second inside zone
    [SerializeField] float suffocationDamagePerSecond = 20f;

    [Header("Debug")]
    [SerializeField] bool enableDebugLogs = true;
    [SerializeField] float debugLogInterval = 1f;

    float oxygenLevel;
    [SerializeField] bool isInOxygenZone;

    float debugTimer;

    void OnValidate()
    {
        if (maxOxygen <= 0f) maxOxygen = 100f;
        if (depletionRateOutside < 0f) depletionRateOutside = 0f;
        if (regenRateInside < 0f) regenRateInside = 0f;
        if (debugLogInterval <= 0f) debugLogInterval = 1f;
    }

    void Start()
    {
        oxygenLevel = maxOxygen;
        debugTimer = 0f;
        if (enableDebugLogs)
            Debug.Log($"PlayerOxygen.Start: initialized with max oxygen: {maxOxygen}", this);
    }

    void Update()
    {
        float dt = Time.deltaTime;

        // Guard: if time is frozen, don't pretend to update
        if (dt <= 0f)
        {
            if (enableDebugLogs)
                Debug.Log("PlayerOxygen.Update: deltaTime is zero - Time.timeScale may be 0", this);
            return;
        }

        float before = oxygenLevel;

        if (isInOxygenZone)
        {
            oxygenLevel = Mathf.Min(maxOxygen, oxygenLevel + regenRateInside * dt);
        }
        else
        {
            oxygenLevel = Mathf.Max(0f, oxygenLevel - depletionRateOutside * dt);
        }

        // Optionally apply suffocation damage when out of oxygen (left intentional empty here)
        if (oxygenLevel <= 0f)
        {
           
        }

        // Debug logging once per debugLogInterval seconds (not every frame)
        if (enableDebugLogs)
        {
            debugTimer += dt;
            if (debugTimer >= debugLogInterval)
            {
                debugTimer = 0f;
                float change = oxygenLevel - before;
                Debug.Log($"PlayerOxygen.Update: oxygen={oxygenLevel:F2} change={change:F3} inZone={isInOxygenZone}", this);
            }
        }
    }

    // Called by zone triggers (OxygenZone) or any other code
    public void SetInOxygenZone(bool inZone)
    {
        isInOxygenZone = inZone;
        if (enableDebugLogs)
            Debug.Log($"PlayerOxygen.SetInOxygenZone: now inZone={inZone}", this);
    }

    // small helper you can call from Inspector (via button) or other scripts to toggle
    public void ToggleOxygenZone()
    {
        SetInOxygenZone(!isInOxygenZone);
    }

    public float GetOxygenPercent()
    {
        return Mathf.Clamp01(oxygenLevel / maxOxygen);
    }

    public float GetOxygenLevel() => oxygenLevel;
}
