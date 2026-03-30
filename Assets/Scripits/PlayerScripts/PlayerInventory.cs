
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public int parts = 0;
    public int dialogue = 0;

    [Header("Drop Settings")]
    [SerializeField] private GameObject scrapPrefab; // assign the Scrap prefab (with ScrapPickUp) in inspector
    [SerializeField] private float dropScatterRadius = 1.5f;

    public  void AddParts(int amount)
    {
        parts += amount;
        Debug.Log("Scrap collected. Total: " + parts);
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
            parts = 0;
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
        }

        parts = 0;
        Debug.Log($"PlayerInventory: dropped {toDrop} scrap pickups at {origin}", this);
    }
}
