using UnityEngine;

public class ScrapPickUp : MonoBehaviour
{
    [SerializeField] public int partsAmount = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // support PlayerInventory on collider or on a parent (handles nested player setups)
        PlayerInventory inventory = other.GetComponent<PlayerInventory>() ?? other.GetComponentInParent<PlayerInventory>();
        if (inventory == null) return;

        // Prevent double-trigger: disable this pickup's collider/visual immediately
        Collider2D myCol = GetComponent<Collider2D>();
        if (myCol != null) myCol.enabled = false;

        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        if (sprite != null) sprite.enabled = false;

        bool addedFully = inventory.AddParts(partsAmount);
        if (addedFully)
        {
            Destroy(gameObject);
        }
        else
        {
            // Player didn't take the full amount (or was full) — re-enable so pickup can be collected later
            if (myCol != null) myCol.enabled = true;
            if (sprite != null) sprite.enabled = true;

            Debug.Log("ScrapPickUp: player at max scrap or only partially accepted — leaving pickup in world", this);
        }
    }
}