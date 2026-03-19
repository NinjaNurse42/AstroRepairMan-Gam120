using UnityEngine;
using System.Collections;

public class PlayerDamage : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;

    [Header("Death Settings")]
    [SerializeField] float deathImpactSpeed = 6f;
    [SerializeField] LayerMask damageLayers = ~0;
    [SerializeField] PlayerOxygen playerOxygen;

    [Header("Debug")]
    [SerializeField] bool debugLogCollisions = true;

    bool isDead = false;

    // cached shield component
    PlayerSheilds shields;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        shields = GetComponent<PlayerSheilds>();

        if (!CheckPointManager.HasCheckpoint)
            CheckPointManager.SetCheckpoint(transform.position);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDead) return;
        if (collision.gameObject == null) return;

        // If collided with a projectile, attempt to let shields absorb it first
        var proj = collision.gameObject.GetComponent<Projectile>();
        if (proj != null)
        {
            if (debugLogCollisions)
                Debug.Log("Hit by projectile (collision)", this);

            bool absorbed = shields != null && shields.TryAbsorbDamage(1);

            // destroy projectile on hit
            Destroy(proj.gameObject);

            if (absorbed)
                return;

            Die();
            return;
        }

        // Existing impact-based death handling (speed-based)
        if ((damageLayers.value & (1 << collision.gameObject.layer)) == 0)
        {
            if (debugLogCollisions)
                Debug.Log("Collision ignored (layer not in damageLayers)", this);
            return;
        }

        float speed = rb != null ? rb.linearVelocity.magnitude : 0f;

        if (debugLogCollisions)
            Debug.Log($"Impact speed: {speed:F2}", this);

        if (speed >= deathImpactSpeed)
        {
            if (debugLogCollisions)
                Debug.Log("High-speed impact detected", this);

            // Try to absorb with shields first
            bool absorbed = shields != null && shields.TryAbsorbDamage(1);
            if (absorbed)
                return;

            Die();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (isDead) return;
        if (other == null || other.gameObject == null) return;

        // Handle projectile configured as trigger
        var proj = other.GetComponent<Projectile>();
        if (proj != null)
        {
            if (debugLogCollisions)
                Debug.Log("Hit by projectile (trigger)", this);

            bool absorbed = shields != null && shields.TryAbsorbDamage(1);

            // destroy projectile on hit
            Destroy(proj.gameObject);

            if (absorbed)
                return;

            Die();
            return;
        }

        // Other trigger logic (e.g. checkpoints) not handled here
    }

    // PUBLIC so other scripts like PlayerOxygen can call it
    public void Die()
    {
        if (isDead) return;

        isDead = true;

        if (anim != null)
            anim.SetTrigger("Explode");

        StartCoroutine(RespawnDelay());
    }

    IEnumerator RespawnDelay()
    {
        yield return new WaitForSeconds(1f);

        if (anim != null)
            anim.SetTrigger("Restore");

        yield return new WaitForSeconds(0.5f);

        RestorePlayer();
    }

    public void RestorePlayer()
    {
        Vector3 target = CheckPointManager.HasCheckpoint
            ? CheckPointManager.LastCheckpoint
            : Vector3.zero;

        if (rb != null)
        {
            rb.position = target;
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }

        transform.position = target;

        if (playerOxygen != null)
            playerOxygen.ResetOxygen();

        if (anim != null)
            anim.ResetTrigger("Explode");

        // Optional: restore shields on respawn
        if (shields != null)
            shields.RestoreAll();

        isDead = false;

        if (debugLogCollisions)
            Debug.Log($"Respawned at {target}", this);
    }

    public void ForceRespawn()
    {
        Die();
    }
}