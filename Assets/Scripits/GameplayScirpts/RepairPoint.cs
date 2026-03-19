using UnityEngine;

public class RepairPoint : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int repairCost = 3;
    public bool isRepaired = false;
    public int TotalRepair = 0;

    public GameObject brokenPart;
    public GameObject repairedPart;


    [Header("Optional Dialogue")]
    [SerializeField] DialougeObject onRepairedDialogue;

    void Start() => repairedPart.SetActive(false);

    public void TryRepair(PlayerInventory inventory)
    {
        if (isRepaired) return;

        if (inventory.SpendParts(repairCost))
        {
            isRepaired = true;
            brokenPart.SetActive(false);
            repairedPart.SetActive(true);
            TotalRepair++;

            Debug.Log("Station system repaired");

            // Restore one shield on the player who performed the repair (if they have PlayerSheilds)
            if (inventory != null)
            {
                var shields = inventory.GetComponent<PlayerSheilds>();
                if (shields != null)
                {
                    shields.RestoreOne();
                    Debug.Log("RepairPoint: restored 1 player shield", this);
                }
            }

            if (onRepairedDialogue != null)
            {
                DialougeManager.Show(onRepairedDialogue);
            }
        }
        else
        {
            Debug.Log("Not enough parts");
        }
    }
}
