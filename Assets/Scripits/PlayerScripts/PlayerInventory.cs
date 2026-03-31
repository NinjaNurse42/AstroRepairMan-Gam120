using UnityEditor.Rendering;
using UnityEditor.VersionControl;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [Header("Inventory")]
    public int parts = 0;
    public int dialogue = 0;
    public int MaximumScrap = 3;

<<<<<<< Updated upstream
    public  void AddParts(int amount)
=======
    [Header("Drop Settings")]
    [SerializeField] private GameObject scrapPrefab; // assign the Scrap prefab (with ScrapPickUp) in inspector
    [SerializeField] private float dropScatterRadius = 1.5f;

    public void AddParts(int amount)
>>>>>>> Stashed changes
    {
        if (amount <= 0) return;

        int before = parts;
        parts = Mathf.Clamp(parts + amount, 0, MaximumScrap);
        int actuallyAdded = parts - before;

        if (actuallyAdded > 0)
        {
            Debug.Log($"Scrap collected. +{actuallyAdded} -> Total: {parts}/{MaximumScrap}", this);
        }
        else
        {
            Debug.Log($"Scrap collection blocked: already at max ({MaximumScrap})", this);
        }
    }

    public bool SpendParts(int amount)
    {
        if (amount <= 0) return true;

        if (parts >= amount)
        {
            parts -= amount;
            ++dialogue;
            Debug.Log($"Spent {amount} parts. Remaining: {parts}", this);
            return true;
        }

        Debug.Log($"Not enough parts to spend ({parts} available, need {amount})", this);
        return false;
    }

    public void Storytime(int story)
    {
        dialogue = story;
        

    }

<<<<<<< Updated upstream
=======
    public void ResetParts()
    {
        parts = 0;
        Debug.Log("PlayerInventory: parts reset to 0", this);
    }

    /// <summary>
    /// Instantiate scrap pickups around the given origin equal to current parts, then zero out parts.
    /// Requires a Scrap prefab assigned to <see cref="scrapPrefab"/> which has the <see cref="ScrapPickUp"/> component.
    /// </summary>
    public void DropAllParts(Vector3 origin)
    {
        if (parts <= 0)
        {
            Debug.Log("PlayerInventory.DropAllParts: no scrap to drop.", this);
            return;
        }

        if (scrapPrefab == null)
        {
            Debug.LogWarning("PlayerInventory.DropAllParts: scrapPrefab not assigned. Parts will be lost.", this);
            ResetParts();
            return;
        }

        int toDrop = parts;

        for (int i = 0; i < toDrop; i++)
        {
            Vector2 offset = Random.insideUnitCircle * dropScatterRadius;
            GameObject go = Instantiate(scrapPrefab, origin + (Vector3)offset, Quaternion.identity);
            ScrapPickUp sp = go.GetComponent<ScrapPickUp>();
            if (sp != null)
            {
                sp.partsAmount = 1; // make each spawned pickup give 1 part
            }

            // Optional: add a small random impulse if the prefab has Rigidbody2D
            Rigidbody2D r = go.GetComponent<Rigidbody2D>();
            if (r != null)
                r.AddForce(Random.insideUnitCircle * 50f);
        }

        ResetParts();
        Debug.Log($"PlayerInventory: dropped {toDrop} scrap pickups at {origin}", this);
    }
>>>>>>> Stashed changes
}
