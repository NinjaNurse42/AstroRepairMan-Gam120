using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public int parts = 0;

    public  void AddParts(int amount)
    {
        parts += amount;
        Debug.Log("Scrap collected. Total: " + parts);
    }

    public bool SpendParts(int amount)
    {
        if (parts >= amount)
        {
            parts -= amount;
            return true;
        }
        return false;
    }
    
}
