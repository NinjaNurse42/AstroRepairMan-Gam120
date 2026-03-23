using UnityEngine;
using System.Collections;

public class PlayerDamage : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;
    private PlayerSheilds shields;

    [Header("Death Settings")]
    [SerializeField] float deathImpactSpeed = 6f;
    [SerializeField] LayerMask damageLayers = ~0;
    [SerializeField] PlayerOxygen playerOxygen;

    [Header("Death Dialogue")]
    [SerializeField] string projectileDeathMessage = "Hit by projectile!";
    [SerializeField] string impactDeathMessage = "Crashed at high speed!";
    [SerializeField] TextboxUI textboxUI; // drag your TextboxUI here for death dialogue

    [Header("Debug")]
    [SerializeField] bool debugLogCollisions = true;

    // ✅ GLOBAL death reason (accessible from any script)
    public static string LastDeathReason;

    bool isDead = false;

    // ✅ Store velocity BEFORE impact
    private Vector2 lastVelocity;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        shields = GetComponent<PlayerSheilds>();

        if (!CheckPointManager.HasCheckpoint)
            CheckPointManager.SetCheckpoint(transform.position);
    }

    void Update()
    {
        // ✅ Track velocity every frame BEFORE collision happens
        if (rb != null)
            lastVelocity = rb.linearVelocity; // linearVelocity or velocity works depending on your Rigidbody type
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDead || collision.gameObject == null) return;

        // ✅ Projectile check (collision)
        var proj = collision.gameObject.GetComponent<Projectile>();
        if (proj != null)
        {
            if (debugLogCollisions)
                Debug.Log("Hit by projectile (collision) -> checking shields", this);

            bool absorbed = shields != null && shields.TryAbsorbDamage(1);

            LastDeathReason = projectileDeathMessage;

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
        if (isDead || other == null || other.gameObject == null) return;

        // ✅ Projectile check (trigger)
        var proj = other.GetComponent<Projectile>();
        if (proj != null)
        {
            if (debugLogCollisions)
                Debug.Log("Hit by projectile (trigger) -> checking shields", this);

            bool absorbed = shields != null && shields.TryAbsorbDamage(1);

            LastDeathReason = projectileDeathMessage;

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
        }
    }

    // PUBLIC so other scripts can call it
    public void Die()
    {
        if (isDead) return;

        isDead = true;

        if (anim != null)
            anim.SetTrigger("Explode");

        // ✅ Trigger death dialogue if TextboxUI assigned
        if (textboxUI != null)
            textboxUI.OnPlayerDeath();

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