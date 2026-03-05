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

            Die();
        }
    }

    // PUBLIC so other scripts like PlayerOxygen can call it
    public void Die()
    {
        if (isDead) return;

        isDead = true;

        anim.SetTrigger("Explode");

        StartCoroutine(RespawnDelay());
    }

    IEnumerator RespawnDelay()
    {
        yield return new WaitForSeconds(1f);

        anim.SetTrigger("Restore");

        yield return new WaitForSeconds(0.5f);

        RestorePlayer();
    }

    public void RestorePlayer()
    {
        Vector3 target = CheckPointManager.HasCheckpoint
            ? CheckPointManager.LastCheckpoint
            : Vector3.zero;

        rb.position = target;
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;

        transform.position = target;

        if (playerOxygen != null)
            playerOxygen.ResetOxygen();

        anim.ResetTrigger("Explode");

        isDead = false;

        if (debugLogCollisions)
            Debug.Log($"Respawned at {target}", this);
    }

    public void ForceRespawn()
    {
        Die();
    }
}