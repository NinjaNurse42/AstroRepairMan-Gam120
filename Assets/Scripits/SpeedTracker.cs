using UnityEngine;

public class SpeedTracker : MonoBehaviour
{
    [Header("Logging")]
    public bool EnableLogging = true;
    [Tooltip("Seconds between log entries")]
    public float LogInterval = 0.5f;
    [Tooltip("Only log when speed is >= this value. Set to 0 to always log.")]
    public float MinSpeedToLog = 0f;
    [Tooltip("Include full velocity vector in the log")]
    public bool LogVelocityVector = false;

    Rigidbody2D rb;
    float timer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
            Debug.LogWarning("SpeedTracker: no Rigidbody2D found on the same GameObject. Attach to the player or add a Rigidbody2D.", this);

        timer = 0f;
    }

    void FixedUpdate()
    {
        if (!EnableLogging || rb == null) return;

        timer += Time.fixedDeltaTime;
        if (timer < LogInterval) return;
        timer = 0f;

        float speed = rb.linearVelocity.magnitude;
        if (speed < MinSpeedToLog) return;

        if (LogVelocityVector)
            Debug.Log($"SpeedTracker: speed={speed:F2} units/s velocity={rb.linearVelocity}", this);
        else
            Debug.Log($"SpeedTracker: speed={speed:F2} units/s", this);
    }
}
