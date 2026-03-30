    using UnityEngine;
using System.Collections;

public class TrapDamage : MonoBehaviour
{
    [Header("Player References")]
    private Animator anim;
    private Rigidbody2D rb;
    private PlayerOxygen playerOxygen;
    private PlayerInventory inventory;
    private Transform playerTransform;

    [Header("Debug")]
    [SerializeField] bool debugLogCollisions = true;

    bool isDead = false;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (isDead) return;

        if (collision.CompareTag("Player"))
        {
            anim = collision.GetComponent<Animator>();
            rb = collision.GetComponent<Rigidbody2D>();
            playerOxygen = collision.GetComponent<PlayerOxygen>();
            inventory = collision.GetComponent<PlayerInventory>();
            playerTransform = collision.transform;

            Die();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDead) return;

        if (collision.collider.CompareTag("Player"))
        {
            anim = collision.collider.GetComponent<Animator>();
            rb = collision.collider.GetComponent<Rigidbody2D>();
            playerOxygen = collision.collider.GetComponent<PlayerOxygen>();
            inventory = collision.collider.GetComponent<PlayerInventory>();
            playerTransform = collision.collider.transform;

            Die();
        }
    }

    public void Die()
    {
        if (isDead) return;

        isDead = true;

        // Drop all scrap from the player (if inventory found)
        if (inventory != null)
        {
            Vector3 dropOrigin = playerTransform != null ? playerTransform.position : transform.position;
            if (debugLogCollisions)
                Debug.Log($"TrapDamage.Die: dropping {inventory.parts} parts at {dropOrigin}", this);

            inventory.DropAllParts(dropOrigin);
        }
        else if (debugLogCollisions)
        {
            Debug.LogWarning("TrapDamage.Die: PlayerInventory not found on player - no scrap dropped.", this);
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

    void RestorePlayer()
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

        // Ensure player's transform is placed at the checkpoint as well
        if (playerTransform != null)
            playerTransform.position = target;

        if (playerOxygen != null)
            playerOxygen.ResetOxygen();

        if (anim != null)
            anim.ResetTrigger("Explode");

        isDead = false;

        if (debugLogCollisions)
            Debug.Log($"Respawned player at {target}");
    }
}