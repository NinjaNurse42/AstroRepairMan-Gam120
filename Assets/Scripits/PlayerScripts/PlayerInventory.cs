using UnityEditor.Rendering;
using UnityEditor.VersionControl;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public int parts = 0;
    public int dialogue = 0;

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
            ++dialogue;
            return true;
        }
        return false;
    }
    
    public void Storytime(int story)
    {
        dialogue = story;
        

    }

}
