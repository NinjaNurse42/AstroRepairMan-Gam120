using UnityEngine;
using System.Collections;

public class PlayerDamage : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;
    private PlayerSheilds shields;
    private PlayerInventory inventory;

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
        inventory = GetComponent<PlayerInventory>();

        if (!CheckPointManager.HasCheckpoint)
            CheckPointManager.SetCheckpoint(transform.position);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDead) return;
        if (collision.gameObject == null) return;

        var proj = collision.gameObject.GetComponent<Projectile>();
        if (proj != null)
        {
            if (debugLogCollisions)
                Debug.Log("Hit by projectile (collision) -> checking shields", this);

            bool absorbed = shields != null && shields.TryAbsorbDamage(1);

            Destroy(proj.gameObject);



            if (debugLogCollisions)
                Debug.Log("No shields available -> dying", this);

            Die();
            return;
        }

        if ((damageLayers.value & (1 << collision.gameObject.layer)) == 0)
        {
            if (debugLogCollisions)
                Debug.Log("Collision ignored (layer not in damageLayers)", this);
            return;
        }

        // Use Rigidbody2D.velocity
        float speed = rb != null ? rb.linearVelocity.magnitude : 0f;

        if (debugLogCollisions)
            Debug.Log($"Impact speed: {speed:F2}", this);

        if (speed >= deathImpactSpeed)
        {
            if (debugLogCollisions)
                Debug.Log("Fatal impact detected -> checking shields", this);

            bool absorbed = shields != null && shields.TryAbsorbDamage(1);

            if (absorbed)
            {
                if (debugLogCollisions)
                    Debug.Log("Impact absorbed by shields", this);
                return;
            }

            if (debugLogCollisions)
                Debug.Log("No shields available -> dying", this);

            Die();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (isDead) return;
        if (other == null || other.gameObject == null) return;

        var proj = other.GetComponent<Projectile>();
        if (proj != null)
        {
            if (debugLogCollisions)
                Debug.Log("Hit by projectile (trigger) -> checking shields", this);

            bool absorbed = shields != null && shields.TryAbsorbDamage(1);

            Destroy(proj.gameObject);

            if (absorbed)
            {
                if (debugLogCollisions)
                    Debug.Log("Projectile absorbed by shields", this);
                return;
            }

            if (debugLogCollisions)
                Debug.Log("No shields available -> dying", this);

            Die();
            return;
        }
    }

    // PUBLIC so other scripts like PlayerOxygen can call it
    public void Die()
    {
        if (isDead) return;

        isDead = true;

        // Diagnostic: ensure inventory reference and parts count are visible in log
        if (inventory == null)
        {
            Debug.LogWarning("PlayerDamage.Die: PlayerInventory component not found on same GameObject.", this);
        }
        else
        {
            Debug.Log($"PlayerDamage.Die: dropping {inventory.parts} parts", this);
            inventory.DropAllParts(transform.position);
        }

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

        if (shields != null)
            shields.RestoreAll();

        if (anim != null)
            anim.ResetTrigger("Explode");

        isDead = false;

        if (debugLogCollisions)
            Debug.Log($"Respawned at {target}", this);
    }

    public void ForceRespawn() => Die();
}