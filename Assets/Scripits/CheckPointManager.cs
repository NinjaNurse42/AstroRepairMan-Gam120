using UnityEngine;

public class CheckPointManager: MonoBehaviour
{
    public static Vector3 LastCheckpoint { get; private set; }
    public static bool HasCheckpoint { get; private set; }

    public static void SetCheckpoint(Vector3 position)
    {
        LastCheckpoint = position;
        HasCheckpoint = true;
        Debug.Log($"CheckpointManager: checkpoint set to {position}", null);
    }

}
