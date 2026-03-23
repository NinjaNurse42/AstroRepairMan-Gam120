using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class ShieldChangedEvent : UnityEvent<int, int> { }

public class PlayerSheilds : MonoBehaviour
{
    [Header("Shield Settings")]
    [SerializeField] int maxShields = 3;
    [SerializeField] int startingShields = 3;

    [Header("Events")]
    [SerializeField] ShieldChangedEvent onShieldsChanged;

    [Header("Debug")]
    [SerializeField] bool debugLogs = true;

    int currentShields;

    void OnValidate()
    {
        if (maxShields < 0) maxShields = 0;
        startingShields = Mathf.Clamp(startingShields, 0, maxShields);
    }

    void Awake()
    {
        currentShields = Mathf.Clamp(startingShields, 0, maxShields);
        onShieldsChanged?.Invoke(currentShields, maxShields);
    }

    /// <summary>
    /// Attempt to absorb damage with shields.
    /// Returns true if shields absorbed the damage (player should NOT die).
    /// Returns false when there are no shields left (damage should be applied to player).
    /// </summary>
    public bool TryAbsorbDamage(int amount = 1)
    {
        if (amount <= 0)
            return true; // nothing to absorb

        if (currentShields <= 0)
            return false; // no shields to absorb

        int used = Mathf.Min(currentShields, amount);
        currentShields -= used;

        onShieldsChanged?.Invoke(currentShields, maxShields);

        if (debugLogs)
            Debug.Log($"PlayerSheilds: absorbed {used} damage. Shields now {currentShields}/{maxShields}", this);

        return true;
    }

    /// <summary>
    /// Restore a single shield (used after a successful repair).
    /// </summary>
    public void RestoreOne()
    {
        if (currentShields >= maxShields)
        {
            if (debugLogs) Debug.Log("PlayerSheilds: already at max, nothing to restore", this);
            return;
        }

        currentShields = Mathf.Min(maxShields, currentShields + 1);
        onShieldsChanged?.Invoke(currentShields, maxShields);

        if (debugLogs)
            Debug.Log($"PlayerSheilds: restored 1 shield -> {currentShields}/{maxShields}", this);
    }

    public void RestoreAll()
    {
        currentShields = maxShields;
        onShieldsChanged?.Invoke(currentShields, maxShields);

        if (debugLogs)
            Debug.Log("PlayerSheilds: restored to max", this);
    }

    public bool IsDepleted => currentShields <= 0;
    public int Current => currentShields;
    public int Max => maxShields;
}
