using UnityEngine;
using System.Collections;

public class TrapDamage : MonoBehaviour
{
    [Header("Player References")]
    private Animator anim;
    private Rigidbody2D rb;
    private PlayerOxygen playerOxygen;
    private PlayerInventory inventory;

    [Header("Debug")]
    [SerializeField] bool debugLogCollisions = true;

    [SerializeField] private AudioClip TrapDeathClip;
    [SerializeField] private AudioClip RespawnClip;

    bool isDead = false;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (isDead) return;

        if (collision.CompareTag("Player"))
        {
            anim = collision.GetComponent<Animator>();
            rb = collision.GetComponent<Rigidbody2D>();
            playerOxygen = collision.GetComponent<PlayerOxygen>();
            inventory = collision.GetComponent<PlayerInventory>() ?? collision.GetComponentInParent<PlayerInventory>();

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
            inventory = collision.collider.GetComponent<PlayerInventory>() ?? collision.collider.GetComponentInParent<PlayerInventory>();

            Die();
        }
    }

    public void Die()
    {
        SFXManager.Instance.TrapDeath(TrapDeathClip, transform, 1.0f);
        if (isDead) return;

        isDead = true;

        // Drop player's scrap here (if any) at player's current position
        if (inventory != null)
        {
            Vector3 dropPos = rb != null ? (Vector3)rb.position : (inventory.transform != null ? inventory.transform.position : transform.position);
            inventory.DropAllParts(dropPos);
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
        SFXManager.Instance.Respawn(RespawnClip, transform, 1.0f);
        Vector3 target = CheckPointManager.HasCheckpoint
            ? CheckPointManager.LastCheckpoint
            : Vector3.zero;

        if (rb != null)
        {
            rb.position = target;
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }

        if (playerOxygen != null)
            playerOxygen.ResetOxygen();

        if (anim != null)
            anim.ResetTrigger("Explode");

        isDead = false;

        if (debugLogCollisions)
            Debug.Log($"Respawned player at {target}");
    }
}