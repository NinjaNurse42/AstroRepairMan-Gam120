using UnityEngine;


public class OxygenZone : MonoBehaviour
{
    void Awake()
    {
        // Make sure the collider is a trigger for 2D physics
        var col2 = GetComponent<Collider2D>();
        if (col2 != null)
            col2.isTrigger = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        HandleEnterExit(other.gameObject, true);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        HandleEnterExit(other.gameObject, false);
    }

    void HandleEnterExit(GameObject other, bool inZone)
    {
        // Optional: require the player to be tagged "Player"
        if (!other.CompareTag("Player")) return;

        // Find PlayerOxygen on the object or a parent (handles nested player structure)
        var po = other.GetComponent<PlayerOxygen>() ?? other.GetComponentInParent<PlayerOxygen>();
        if (po != null)
        {
            po.SetInOxygenZone(inZone);
            Debug.Log($"OxygenZone: SetInOxygenZone({inZone}) for {other.name}", this);
        }
    }

    void OnDisable()
    {
        // Best-effort: clear flag if zone is disabled while player is inside.
        var player = GameObject.FindWithTag("Player");
        if (player == null) return;
        var po = player.GetComponent<PlayerOxygen>() ?? player.GetComponentInParent<PlayerOxygen>();
        if (po != null) po.SetInOxygenZone(false);
    }
}