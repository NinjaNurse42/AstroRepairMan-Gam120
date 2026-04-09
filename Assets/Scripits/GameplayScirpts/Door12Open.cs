using UnityEngine;

public class DoorOpen1 : MonoBehaviour
{
    [Header("All Repair Points (drag 12 here)")]
    public RepairPoint[] repairPoints;

    private Animator anime;
    private bool opened = false;

    void Start()
    {
        anime = GetComponent<Animator>();
    }

    void Update()
    {
        if (opened) return;

        if (AllRepairsComplete())
        {
            anime.SetTrigger("OpenDoor1");
            opened = true;
        }
    }

    bool AllRepairsComplete()
    {
        foreach (RepairPoint rp in repairPoints)
        {
            if (rp == null || rp.TotalRepair < 1)
                return false;
        }

        return true;
    }
}