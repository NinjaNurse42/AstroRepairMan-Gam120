using UnityEngine;

public class RepairPoint : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int repairCost = 5;
    public bool isRepaired = false;

    public GameObject brokenPart;
    public GameObject repairedPart;

    void Start()
    {
        repairedPart.SetActive(false);
    }

    public void TryRepair(PlayerInventory inventory)
    {
        if (isRepaired) return;

        if (inventory.SpendParts(repairCost))
        {
            isRepaired = true;
            brokenPart.SetActive(false);
            repairedPart.SetActive(true);

            Debug.Log("Station system repaired");
        }
        else
        {
            Debug.Log("Not enough parts");
        }
    }
}
