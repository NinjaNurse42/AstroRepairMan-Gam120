using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private RepairPoint currentRepair;
    private PlayerInventory inventory;

    void Start()
    {
        inventory = GetComponent<PlayerInventory>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && currentRepair != null)
        {
            currentRepair.TryRepair(inventory);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        RepairPoint repair = collision.GetComponent<RepairPoint>();
        if (repair != null)
        {
            currentRepair = repair;
        }
    }
}