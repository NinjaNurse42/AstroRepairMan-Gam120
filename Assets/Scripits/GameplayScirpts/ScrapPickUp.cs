using UnityEngine;

public class ScrapPickUp : MonoBehaviour
{
    [SerializeField] public int partsAmount = 1;
    bool isProcessing = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isProcessing) return;

        // support PlayerInventory on collider or on a parent (handles nested player setups)
        PlayerInventory inventory = other.GetComponent<PlayerInventory>() ?? other.GetComponentInParent<PlayerInventory>();
        if (inventory == null) return;

        isProcessing = true;

        // Prevent double-trigger: disable this pickup's collider/visual immediately
        Collider2D myCol = GetComponent<Collider2D>();
        if (myCol != null) myCol.enabled = false;

        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        if (sprite != null) sprite.enabled = false;

        // AddParts now returns how many parts were actually added (0..partsAmount)
        int added = inventory.AddParts(partsAmount);

        if (added >= partsAmount)
        {
            // fully picked up
            Destroy(gameObject);
            return;
        }

        if (added > 0)
        {
            // partially accepted; reduce remaining amount on this pickup
            partsAmount -= added;
            Debug.Log($"ScrapPickUp: partially picked up {added}, remaining {partsAmount}", this);
        }
        else
        {
            // nothing accepted (inventory full)
            Debug.Log("ScrapPickUp: player at max scrap or cannot accept parts right now", this);
        }

        // Re-enable collider/visual so the remaining pickup can be collected later
        if (myCol != null) myCol.enabled = true;
        if (sprite != null) sprite.enabled = true;

        isProcessing = false;
    }
}


