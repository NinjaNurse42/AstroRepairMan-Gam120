using UnityEngine;

public class ScrapPickUp : MonoBehaviour
{
    [SerializeField] public int partsAmount = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // support PlayerInventory on the collider or a parent (handles nested player setups)
        PlayerInventory inventory = other.GetComponent<PlayerInventory>() ?? other.GetComponentInParent<PlayerInventory>();

        if (inventory == null)
            return;

        // Only consume the pickup when the player is below their maximum scrap
        if (inventory.parts < inventory.MaximumScrap)
        {
            inventory.AddParts(partsAmount);
            Destroy(gameObject);
        }
        else
        {
            // Optional: debug so you can see the event in the console while testing
            Debug.Log("ScrapPickUp: player at max scrap, leaving pickup in world", this);
        }
    }
}


