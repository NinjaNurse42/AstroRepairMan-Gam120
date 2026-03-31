using UnityEngine;

public class ScrapPickUp : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] public int partsAmount = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerInventory inventory = other.GetComponent<PlayerInventory>();

        if (inventory != null)
        {
            inventory.AddParts(1);
            Destroy(gameObject);
        }
    }
}


