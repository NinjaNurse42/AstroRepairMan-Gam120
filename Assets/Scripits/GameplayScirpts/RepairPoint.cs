using UnityEngine;
using System.Collections.Generic;

public class RepairPoint : MonoBehaviour
{
    public int repairCost = 3;
    public bool isRepaired = false;
    public int TotalRepair = 0;

    public GameObject brokenPart;
    public GameObject repairedPart;

    [SerializeField] private AudioClip RepairClip;
    [SerializeField] private AudioClip DenyClip;

    [Header("Optional Dialogue")]
    [SerializeField] DialougeObject onRepairedDialogue;

    [Header("Objects to Hide on Repair")]
    [SerializeField] private List<GameObject> hideOnRepair = new List<GameObject>();

    void Start()
    {
        repairedPart.SetActive(false);
    }

    public void TryRepair(PlayerInventory inventory)
    {
        if (isRepaired) return;

        if (inventory.SpendParts(repairCost))
        {
            SFXManager.Instance.ScrapPickup(RepairClip, transform, 1.0f);

            isRepaired = true;
            brokenPart.SetActive(false);
            repairedPart.SetActive(true);

            TotalRepair++;

            // Hide all assigned objects
            foreach (GameObject obj in hideOnRepair)
            {
                if (obj != null)
                    obj.SetActive(false);
            }

            Debug.Log("Station system repaired");

            if (onRepairedDialogue != null)
            {
                DialougeManager.Show(onRepairedDialogue);
            }
        }
        else
        {
            SFXManager.Instance.Deny(DenyClip, transform, 1.0f);
            Debug.Log("Not enough parts");
        }
    }
}