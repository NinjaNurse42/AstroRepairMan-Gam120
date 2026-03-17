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

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        if (!CheckPointManager.HasCheckpoint)
            CheckPointManager.SetCheckpoint(transform.position);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDead) return;
        if (collision.gameObject == null) return;

        // If collided with a projectile, die immediately
        var proj = collision.gameObject.GetComponent<Projectile>();
        if (proj != null)
        {
            if (debugLogCollisions)
                Debug.Log("Hit by projectile (collision) -> dying", this);

            // Optionally destroy the projectile; if projectile handles its own destruction this is safe as well
            Destroy(proj.gameObject);
            Die();
            return;
        }

        // Existing impact-based death handling
        if ((damageLayers.value & (1 << collision.gameObject.layer)) == 0)
        {
            if (debugLogCollisions)
                Debug.Log("Collision ignored (layer not in damageLayers)", this);
            return;
        }

        // Use correct Rigidbody2D API (velocity)
        float speed = rb != null ? rb.linearVelocity.magnitude : 0f;

        if (debugLogCollisions)
            Debug.Log($"Impact speed: {speed:F2}", this);

        if (speed >= deathImpactSpeed)
        {
            if (debugLogCollisions)
                Debug.Log("Fatal impact detected!", this);

            Die();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (isDead) return;
        if (other == null || other.gameObject == null) return;

        // If entered trigger by projectile, die immediately
        var proj = other.GetComponent<Projectile>();
        if (proj != null)
        {
            if (debugLogCollisions)
                Debug.Log("Hit by projectile (trigger) -> dying", this);

            Destroy(proj.gameObject);
            Die();
            return;
        }

        // Other trigger-based logic could go here...
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

        isDead = false;

        if (debugLogCollisions)
            Debug.Log($"Respawned at {target}", this);
    }

    public void ForceRespawn() => Die();
}