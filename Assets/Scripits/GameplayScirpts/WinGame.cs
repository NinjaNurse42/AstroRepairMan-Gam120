using UnityEngine;
using UnityEngine.SceneManagement;

public class WinCondition : MonoBehaviour
{
    public int repairsToWin = 13;

    void Update()
    {
        RepairPoint[] points = FindObjectsOfType<RepairPoint>(true); // <-- IMPORTANT

        int repairedCount = 0;

        foreach (RepairPoint point in points)
        {
            if (point.isRepaired)
            {
                repairedCount++;
            }
        }

        Debug.Log("Repaired Count: " + repairedCount);

        if (repairedCount >= repairsToWin)
        {
            SceneManager.LoadScene("WinScreen");
        }
    }
}