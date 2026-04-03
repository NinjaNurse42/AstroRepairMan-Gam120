using UnityEditor.Rendering;
using UnityEditor.VersionControl;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public int parts = 0;
    public int dialogue = 0;

    [Header("Drop Settings")]
    [SerializeField] private GameObject scrapPickupPrefab;

    /// <summary>
    /// Add parts to the inventory. Returns true when at least one part was added.
    /// </summary>
    public bool AddParts(int amount)
    {
        if (amount <= 0) return false;

        parts += amount;
        Debug.Log($"Scrap collected. +{amount} -> Total: {parts}", this);
        return true;
    }

    /// <summary>
    /// Drops all currently held parts into the world at spawnOrigin.
    /// Instantiates one `scrapPickupPrefab` per part and clears the inventory.
    /// If no prefab is assigned, clears parts and logs a warning.
    /// Returns number of parts dropped.
    /// </summary>
    public int DropAllParts(Vector3 spawnOrigin)
    {
        int toDrop = parts;
        if (toDrop <= 0) return 0;

        if (scrapPickupPrefab == null)
        {
            Debug.LogWarning("PlayerInventory: scrapPickupPrefab not assigned. Clearing parts without spawning pickups.", this);
            parts = 0;
            return toDrop;
        }

        for (int i = 0; i < toDrop; i++)
        {
            Vector3 offset = new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), 0f);
            GameObject go = Instantiate(scrapPickupPrefab, spawnOrigin + offset, Quaternion.identity);

            ScrapPickUp sp = go.GetComponent<ScrapPickUp>();
            if (sp != null)
                sp.partsAmount = 1;
        }

        parts = 0;
        Debug.Log($"Dropped {toDrop} scrap at {spawnOrigin}", this);
        return toDrop;
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
    
    public void Storytime(int story)
    {
        dialogue = story;
    }
}
