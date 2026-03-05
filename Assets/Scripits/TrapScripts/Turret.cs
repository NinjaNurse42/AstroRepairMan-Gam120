using UnityEngine;


public class Turret : MonoBehaviour
{
    [Header("Targeting")]
    [SerializeField] string targetTag = "Player";
    [SerializeField] LayerMask targetLayer = ~0;
    [SerializeField] float detectionRadius = 8f;
    [SerializeField] LayerMask obstructionMask = 0; // layers that block line of sight (0 = none)

    [Header("Rotation")]
    [SerializeField] Transform turretHead; // pivot that will rotate (if null, falls back to this.transform)
    [SerializeField] float rotationSpeed = 360f; // degrees per second
    [SerializeField] float aimToleranceDegrees = 6f; // allowed angle difference to consider "aimed"

    [Header("Firing")]
    [SerializeField] Transform firePoint;
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileSpeed = 12f;
    [SerializeField] float projectileLifeTime = 4f;
    [SerializeField] float fireRate = 1f; // shots per second
    [SerializeField] bool onlyFireWhenInSight = true; // require unobstructed line of sight

    [Header("Debug")]
    [SerializeField] bool showGizmos = true;

    float nextFireTime;
    Transform currentTarget;

    void OnValidate()
    {
        if (detectionRadius < 0f) detectionRadius = 0f;
        if (rotationSpeed < 0f) rotationSpeed = 0f;
        if (aimToleranceDegrees < 0f) aimToleranceDegrees = 0f;
        if (projectileSpeed < 0f) projectileSpeed = 0f;
        if (projectileLifeTime < 0f) projectileLifeTime = 0.1f;
        if (fireRate < 0f) fireRate = 0f;
    }

    void Start()
    {
        if (turretHead == null)
            turretHead = transform;

        if (firePoint == null)
            firePoint = turretHead;

        // If user left targetLayer as default (~0) but uses tag, we still use the tag search.
    }

    void Update()
    {
        // Find closest target within radius (prefer layer filtering; fallback to tag search)
        currentTarget = FindClosestTarget();

        if (currentTarget == null)
            return;

        Vector2 toTarget = (currentTarget.position - turretHead.position);
        float desiredAngle = Mathf.Atan2(toTarget.y, toTarget.x) * Mathf.Rad2Deg - 90f; // assuming turret's up points forward
        float currentZ = turretHead.eulerAngles.z;
        float angle = Mathf.MoveTowardsAngle(currentZ, desiredAngle, rotationSpeed * Time.deltaTime);
        turretHead.eulerAngles = new Vector3(0f, 0f, angle);

        // Check if within aim tolerance
        float angleDifference = Mathf.DeltaAngle(angle, desiredAngle);
        bool aimOk = Mathf.Abs(angleDifference) <= aimToleranceDegrees;

        // Optional line of sight check
        if (onlyFireWhenInSight && aimOk)
        {
            Vector2 start = firePoint.position;
            Vector2 end = currentTarget.position;
            RaycastHit2D hit = Physics2D.Linecast(start, end, obstructionMask);
            if (hit.collider != null)
            {
                // Obstructed
                aimOk = false;
            }
        }

        // Fire if aimed and cooldown allows
        if (aimOk && Time.time >= nextFireTime && projectilePrefab != null)
        {
            Shoot();
            nextFireTime = Time.time + (fireRate > 0f ? 1f / fireRate : Mathf.Infinity);
        }
    }

    Transform FindClosestTarget()
    {
        // Use OverlapCircleAll with layer mask if set (non-zero), otherwise use tag search.
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, detectionRadius, targetLayer);
        Transform best = null;
        float bestDistSqr = float.MaxValue;

        // If layer mask is everything (default ~0) but user set a tag, filter by tag too.
        bool usingTag = !string.IsNullOrEmpty(targetTag);

        for (int i = 0; i < hits.Length; i++)
        {
            Collider2D c = hits[i];
            if (c == null) continue;

            if (usingTag && c.CompareTag(targetTag) == false) continue;

            float d = (c.transform.position - transform.position).sqrMagnitude;
            if (d < bestDistSqr)
            {
                bestDistSqr = d;
                best = c.transform;
            }
        }

        // If no result from layer query, optionally try a tag-based search inside radius.
        if (best == null && usingTag)
        {
            GameObject[] tagged = GameObject.FindGameObjectsWithTag(targetTag);
            float radiusSqr = detectionRadius * detectionRadius;
            for (int i = 0; i < tagged.Length; i++)
            {
                Transform t = tagged[i].transform;
                float d = (t.position - transform.position).sqrMagnitude;
                if (d <= radiusSqr && d < bestDistSqr)
                {
                    bestDistSqr = d;
                    best = t;
                }
            }
        }

        return best;
    }

    void Shoot()
    {
        GameObject go = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = go.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            // Move along turretHead's up vector (top-down convention)
            Vector2 velocity = turretHead.up * projectileSpeed;
            rb.linearVelocity = velocity;
        }

        // If the projectile prefab doesn't manage its own lifetime, destroy after set time
        Destroy(go, projectileLifeTime);
    }

    void OnDrawGizmosSelected()
    {
        if (!showGizmos)
            return;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);

        if (firePoint != null)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(firePoint.position, firePoint.position + (turretHead != null ? turretHead.up : transform.up) * 1.2f);
            Gizmos.DrawWireSphere(firePoint.position, 0.08f);
        }
    }
}
