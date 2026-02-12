using UnityEngine;

public class PlayerDamage : MonoBehaviour
{

    [SerializeField] float deathImpactSpeed = 12f;
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
        if (debugLogCollisions)
            Debug.Log($"OnCollisionEnter2D with {collision.gameObject.name} (layer {collision.gameObject.layer})", this);

        // layer filter
        if ((damageLayers.value & (1 << collision.gameObject.layer)) == 0)
        {
            if (debugLogCollisions)
                Debug.Log("PlayerDamage: collision ignored by layer mask", this);
            return;
        }

        float impactSpeed = 0f;
        if (collision.contactCount > 0)
        {
            foreach (var contact in collision.contacts)
            {
                // relativeVelocity is the velocity of this object relative to the other
                float normalSpeed = -Vector2.Dot(collision.relativeVelocity, contact.normal);
                if (normalSpeed > impactSpeed) impactSpeed = normalSpeed;
            }
        }
        else
        {
            impactSpeed = collision.relativeVelocity.magnitude;
        }

        if (debugLogCollisions)
            Debug.Log($"PlayerDamage: computed impactSpeed={impactSpeed:F2}", this);

        if (impactSpeed >= deathImpactSpeed)
        {
            if (debugLogCollisions)
                Debug.Log($"PlayerDamage: fatal impact {impactSpeed:F2} >= {deathImpactSpeed:F2} with {collision.gameObject.name}", this);
            RespawnAtCheckpoint();
        }
    }


   

    void RespawnAtCheckpoint()
    {
        if (!CheckPointManager.HasCheckpoint)
        {
            Debug.LogWarning("PlayerDamage: No checkpoint set. Respawning at origin (0,0).", this);
            Teleport(Vector3.zero);
        }
        else
        {
            Teleport(CheckPointManager.LastCheckpoint);
        }
    }

    // Teleport using Rigidbody2D API and clear velocities to avoid immediate re-collision motion.
    void Teleport(Vector3 target)
    {
        if (rb != null)
        {
            // Set the physics position first to avoid tunneling/overlap issues
            rb.position = target;
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }

        // Keep transform in sync in case other code reads it directly
        transform.position = target;

        // (Optional) reset rotation if desired:
        // transform.rotation = Quaternion.identity;

        if (debugLogCollisions)
            Debug.Log($"PlayerDamage: teleported to checkpoint {target}", this);
    }

    // Public helper to force a respawn from other systems
    public void ForceRespawn() => RespawnAtCheckpoint();   
}



