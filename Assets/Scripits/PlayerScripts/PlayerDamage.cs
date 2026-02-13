using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    [Header("Death Settings")]
    [SerializeField] float deathImpactSpeed = 6f;   // Adjust this to tune sensitivity
    [SerializeField] LayerMask damageLayers = ~0;

    [Header("Debug")]
    [SerializeField] bool debugLogCollisions = true;

    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (!CheckPointManager.HasCheckpoint)
            CheckPointManager.SetCheckpoint(transform.position);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Ignore layers not included in damageLayers
        if ((damageLayers.value & (1 << collision.gameObject.layer)) == 0)
        {
            if (debugLogCollisions)
                Debug.Log("Collision ignored (layer not in damageLayers)", this);
            return;
        }

        float speed = rb.linearVelocity.magnitude;

        if (debugLogCollisions)
            Debug.Log($"Impact speed: {speed:F2}", this);

        if (speed >= deathImpactSpeed)
        {
            if (debugLogCollisions)
                Debug.Log("Fatal impact detected!", this);

            RespawnAtCheckpoint();
        }
    }

    void RespawnAtCheckpoint()
    {
        Vector3 target = CheckPointManager.HasCheckpoint
            ? CheckPointManager.LastCheckpoint
            : Vector3.zero;

        Teleport(target);
    }

    void Teleport(Vector3 target)
    {
        rb.position = target;
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;

        transform.position = target;

        if (debugLogCollisions)
            Debug.Log($"Respawned at {target}", this);
    }

    public void ForceRespawn()
    {
        RespawnAtCheckpoint();
    }
}


