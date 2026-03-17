using UnityEngine;

public class RotateAndPulse : MonoBehaviour
{
    [Header("Rotation")]
    public float rotationSpeed = 1f;

    [Header("Pulsate")]
    public float pulseAmplitude = 0.1f; // how much it grows/shrinks
    public float pulseFrequency = 2f;   // how fast it pulses

    private Vector3 originalScale;

    private void Start()
    {
        originalScale = transform.localScale;
    }

    private void Update()
    {
        // Rotate the object
        transform.Rotate(0f, 0f, rotationSpeed);

        // Pulsate (scale up and down)
        float scaleFactor = 1f + Mathf.Sin(Time.time * pulseFrequency) * pulseAmplitude;
        transform.localScale = originalScale * scaleFactor;
    }
}