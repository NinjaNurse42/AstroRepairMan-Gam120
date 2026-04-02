using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [Header("Inventory Settings")]
    [SerializeField] int maxParts = 3;
    [Tooltip("Prefab for a single scrap pickup (should have a ScrapPickUp component and a collider)")]
    [SerializeField] GameObject scrapPickupPrefab;
    [Tooltip("Radius to scatter dropped scrap around the player")]
    [SerializeField] float dropScatterRadius = 0.5f;

       public int parts = 0;
    public int dialogue = 0;

    /// <summary>
    /// Try to add up to <paramref name="amount"/> parts.
    /// Returns the number of parts actually added (0..amount).
    /// </summary>
    public int AddParts(int amount)
    {
        if (amount <= 0) return 0;

        int space = maxParts - parts;
        if (space <= 0)
        {
            Debug.Log($"Inventory full (max {maxParts}) - cannot add {amount}", this);
            return 0;
        }

        int toAdd = Mathf.Min(amount, space);
        parts += toAdd;
        Debug.Log($"Scrap collected. +{toAdd} -> Total: {parts}/{maxParts}", this);

        return toAdd;
    }

    public bool SpendParts(int amount)
    {
        if (parts >= amount)
        {
            parts -= amount;
            ++dialogue;
            return true;
        }
        return false;
    }

    /// <summary>
    /// Drop all currently held parts into the world as individual scrap pickups.
    /// If no prefab is assigned the inventory is simply cleared.
    /// Returns number of dropped parts.
    /// </summary>
    public int DropAllParts()
    {
        int toDrop = parts;
        if (toDrop <= 0) return 0;

        if (scrapPickupPrefab != null)
        {
            Vector3 origin = transform.position;
            for (int i = 0; i < toDrop; ++i)
            {
                Vector2 offset = Random.insideUnitCircle * dropScatterRadius;
                Vector3 spawnPos = origin + new Vector3(offset.x, offset.y, 0f);
                GameObject go = Instantiate(scrapPickupPrefab, spawnPos, Quaternion.identity);
                // Ensure the spawned pickup represents a single part (ScrapPickUp.partsAmount defaults to 1)
                ScrapPickUp sp = go.GetComponent<ScrapPickUp>();
                if (sp != null)
                    sp.partsAmount = 1;
            }
        }
        else
        {
            Debug.LogWarning("No scrapPickupPrefab assigned on PlayerInventory - cleared parts without spawning pickups", this);
        }

        parts = 0;
        Debug.Log($"Dropped {toDrop} scrap pickups", this);
        return toDrop;
    }
}

