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

    [Header("Death Dialogue")]
    [SerializeField] string projectileDeathMessage = "Hit by projectile!";
    [SerializeField] string impactDeathMessage = "Crashed at high speed!";

    [Header("Debug")]
    [SerializeField] bool debugLogCollisions = true;

    // ✅ GLOBAL death reason (accessible from ANY script)
    public static string LastDeathReason;

    bool isDead = false;

    // ✅ Store velocity BEFORE impact
    private Vector2 lastVelocity;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        if (!CheckPointManager.HasCheckpoint)
            CheckPointManager.SetCheckpoint(transform.position);
    }

    void Update()
    {
        // ✅ Track velocity every frame BEFORE collision happens
        if (rb != null)
            lastVelocity = rb.linearVelocity;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDead) return;
        if (collision.gameObject == null) return;

        // ✅ Projectile check (collision)
        var proj = collision.gameObject.GetComponent<Projectile>();
        if (proj != null)
        {
            if (debugLogCollisions)
                Debug.Log("Hit by projectile (collision) -> dying", this);

            LastDeathReason = projectileDeathMessage;

            Destroy(proj.gameObject);
            Die();
            return;
        }

        // Layer check
        if ((damageLayers.value & (1 << collision.gameObject.layer)) == 0)
        {
            if (debugLogCollisions)
                Debug.Log("Collision ignored (layer not in damageLayers)", this);
            return;
        }

        // ✅ Use PRE-IMPACT velocity instead of slowed velocity
        float speed = lastVelocity.magnitude;

        if (debugLogCollisions)
            Debug.Log($"Pre-impact speed: {speed:F2}", this);

        if (speed >= deathImpactSpeed)
        {
            if (debugLogCollisions)
                Debug.Log("Fatal impact detected!", this);

            LastDeathReason = impactDeathMessage;

            Die();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (isDead) return;
        if (other == null || other.gameObject == null) return;

        // ✅ Projectile check (trigger)
        var proj = other.GetComponent<Projectile>();
        if (proj != null)
        {
            if (debugLogCollisions)
                Debug.Log("Hit by projectile (trigger) -> dying", this);

            LastDeathReason = projectileDeathMessage;

            Destroy(proj.gameObject);
            Die();
            return;
        }
    }

    // PUBLIC so other scripts can call it
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