using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    public RepairPoint repairPoint; 
    private Animator anime;
    private bool opened = false;

    void Start()
    {
        anime = GetComponent<Animator>();
    }

    void Update()
    {
        if (repairPoint != null && repairPoint.TotalRepair >= 1 && !opened)
        {
            anime.SetTrigger("OpenDoor1"); 
            opened = true; 
        }
    }
}