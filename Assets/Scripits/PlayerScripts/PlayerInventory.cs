using UnityEditor.Rendering;
using UnityEditor.VersionControl;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public int parts = 0;
    public int dialogue = 0;

    /// <summary>
    /// Add parts to the inventory. Returns true when at least one part was added.
    /// This prevents calling code from assuming the pickup was consumed when the player is full.
    /// </summary>
    public bool AddParts(int amount)
    {
        if (amount <= 0) return false;

        parts += amount;
        Debug.Log($"Scrap collected. +{amount} -> Total: {parts}", this);
        return true;
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
