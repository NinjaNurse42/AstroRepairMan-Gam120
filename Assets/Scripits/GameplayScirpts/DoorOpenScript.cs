using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    public RepairPoint repairPoint; 
    private Animator anim;
    private bool opened = false;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (repairPoint != null && repairPoint.TotalRepair >= 1 && !opened)
        {
            anim.SetTrigger("OpenDoor1"); 
            opened = true; 
        }
    }
}