using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] string playerTag = "Player";

    void Awake()
    {
        var col = GetComponent<Collider2D>();
        if (col != null)
            col.isTrigger = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other == null) return;
        if (!other.CompareTag(playerTag)) return;

        CheckPointManager.SetCheckpoint(transform.position);
    }
}
